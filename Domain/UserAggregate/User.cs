using Domain.Common.Domain;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public PhoneNumber PhoneNumber{ get; private set; }
    public string Email { get; private set; }
    public Password Password { get; private set; }
    public string AvatarName {  get; private set; }
    public Gender Gender { get; private set; }

    public List<UserRole> Roles { get; private set; }
    public List<UserAddress> UserAddresses { get; private set; }
    public List<Wallet> Wallets { get; private set; }

    public User(string name, string family, PhoneNumber phoneNumber, string email,
        Password password, Gender gender, IDomainUserService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
    }

    public void Edit (string name, string family, PhoneNumber phoneNumber, string email,
       Gender gender, IDomainUserService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public static User RegisterUser(PhoneNumber phoneNumber, Password password, IDomainUserService domainService)
    {

        return new User("", "", phoneNumber, email: null, password, Gender.None, domainService);

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
        var addAddress = UserAddresses.Any(a=> a.PostalCode == address.PostalCode &&
            a.PostalAddress == address.PostalAddress);

        if(addAddress) 
            throw new DuplicateAddressDomainException("این آدرس قبلاً ثبت شده است.");

        UserAddresses.Add(address);
    }

    public void DeleteAddress(long addressId)
    {
        var address=UserAddresses.FirstOrDefault(x=>x.Id == addressId);
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
        UserAddresses.Add(address);
    }
    public void ChargeWallet(Wallet wallet) 
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    public void SetRole(List<UserRole> roles)
    {
        roles.ForEach(f=>f.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }
    
    public void Guard(PhoneNumber phoneNumber, string email,IDomainUserService domainUserService)
    {
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));



        if (email.IsValidEmail() == false)
            throw new InvalidDomainDataException(" ایمیل  نامعتبر است");

        if (phoneNumber != PhoneNumber && domainUserService.PhoneNumberIsExist(phoneNumber))
            throw new InvalidDomainDataException("شماره موبایل تکراری است");

        if (email != Email && domainUserService.EmailIsExist(email))
            throw new InvalidDomainDataException("ایمیل تکراری است");

    }
}
