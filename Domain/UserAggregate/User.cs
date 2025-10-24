using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
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
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string PassWord { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<UserAddress> UserAdresses { get; private set; }
    public List<Wallet> Wallets { get; private set; }

    public User(string name, string family, string phoneNumber, string email,
        string passWord, Gender gender, IDomainUserService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        PassWord = passWord;
        Gender = gender;
    }

    public void Edit (string name, string family, string phoneNumber, string email,
        string passWord, Gender gender, IDomainUserService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        PassWord = passWord;
        Gender = gender;
    }

    public static User RegisterUser(string email, string phoneNumber, string password, IDomainUserService domainService)
    {

        return new User("", "", phoneNumber, email, password, Gender.None, domainService);

    }
    public void AddAdress(UserAddress adress)
    {
        adress.UserId = Id;
        UserAdresses.Add(adress);
    }

    public void DeleteAdress(long adressId)
    {
        var adress=UserAdresses.FirstOrDefault(x=>x.Id == adressId);
        if (adress == null)
        {
            throw new NullOrEmptyDomainDataException("کاربر وجود ندارد ");
        }
        UserAdresses.Remove(adress);
    }
    public void EditAdress(UserAddress adress)
    {
        var oldAddress = UserAdresses.FirstOrDefault(x => x.Id == adress.Id);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("کاربر وجود ندارد ");
        }
        UserAdresses.Remove(oldAddress);
        UserAdresses.Add(adress);
    }
    public void ChargeWallet(Wallet wallet) 
    {
        Wallets.Add(wallet);
    }

    public void SetRole(List<UserRole> roles)
    {
        roles.ForEach(f=>f.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void Guard(string phoneNumber, string email,IDomainUserService domainUserService)
    {
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));


        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

        if (email.IsValidEmail() == false)
            throw new InvalidDomainDataException(" ایمیل  نامعتبر است");

        if (phoneNumber != PhoneNumber && domainUserService.PhoneNumberIsExist(phoneNumber))
            throw new InvalidDomainDataException("شماره موبایل تکراری است");

        if (email != Email && domainUserService.EmailIsExist(email))
            throw new InvalidDomainDataException("ایمیل تکراری است");

    }
}
