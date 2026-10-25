using Domain.Common.Domain;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.UserAggregate;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Password Password { get; private set; }
    public string? AvatarName { get; private set; }
    public Gender Gender { get; private set; }

    public List<UserRole> Roles { get; private set; } = new();
    public List<UserAddress> UserAddresses { get; private set; } = new();
    public List<Wallet> Wallets { get; private set; } = new();
    public List<UserToken> Tokens { get; private set; } = new();

    private User()
    {
    }

    public User(string name, string family, string phoneNumber, string email,
        Password password, Gender gender, IUserDomainService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = new PhoneNumber(phoneNumber);
        Email = email;
        Password = password;
        Gender = gender;
    }

    public void Edit(string name, string family, string phoneNumber, string email,
       Gender gender, IUserDomainService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = new PhoneNumber(phoneNumber);
        Email = email;
        Gender = gender;
    }

    public static User RegisterUser(string phoneNumber, Password password, IUserDomainService domainService)
    {
        return new User(string.Empty, string.Empty, phoneNumber, email: string.Empty, password, Gender.None, domainService);
    }

    public void SetAvatar(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
            imageName = "avatar.png";

        AvatarName = imageName;
    }

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        var addAddress = UserAddresses.Any(a => a.PostalCode == address.PostalCode &&
            a.PostalAddress == address.PostalAddress);

        if (addAddress)
            throw new DuplicateAddressDomainException("این آدرس قبلاً ثبت شده است.");

        UserAddresses.Add(address);
    }

    public void DeleteAddress(long addressId)
    {
        var address = UserAddresses.FirstOrDefault(x => x.Id == addressId);
        if (address == null)
        {
            throw new NullOrEmptyDomainDataException("آدرس وجود ندارد ");
        }
        UserAddresses.Remove(address);
    }

    public void EditAddress(UserAddress address)
    {
        var oldAddress = UserAddresses.FirstOrDefault(x => x.Id == address.Id);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("آدرس وجود ندارد ");
        }
        UserAddresses.Remove(oldAddress);
        address.UserId = Id;
        UserAddresses.Add(address);
    }

    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    public void SetRole(List<UserRole> roles)
    {
        if (Id == 0)
            throw new InvalidDomainDataException("!کاربر ثبت نشده");

        roles.ForEach(f => f.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activeTokenCount = Tokens.Count(t => t.RefreshTokenExpireDate > DateTime.Now);
        if (activeTokenCount >= 12)
            throw new InvalidDomainDataException("امکان ورود همزمان با بیش از ۱۲ دستگاه وجود ندارد.");

        Tokens.Add(new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device)
        {
            UserId = Id
        });
    }

    public void RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(t => t.Id == tokenId);
        if (token == null)
            throw new NullOrEmptyDomainDataException("توکن یافت نشد");

        Tokens.Remove(token);
    }

    public void Guard(string phoneNumber, string email, IUserDomainService domainUserService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

        if (!string.IsNullOrWhiteSpace(email))
        {
            if (email.IsValidEmail() == false)
                throw new InvalidDomainDataException(" ایمیل  نامعتبر است");

            if (email != Email && domainUserService.EmailIsExist(email))
                throw new DuplicateEmailDomainException("ایمیل تکراری است");
        }

        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("تعداد کاراکتر شماره باید 11 تا باشد.");

        if (phoneNumber != PhoneNumber?.Value && domainUserService.PhoneNumberIsExist(phoneNumber))
            throw new DuplicatePhoneNumberDomainException("شماره موبایل تکراری است");
    }
}
