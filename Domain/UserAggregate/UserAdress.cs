using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate;

public class UserAddress:BaseEntity
{
    public long UserId { get; internal set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string Province { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string NationalCode { get; private set; }
    public bool ISActive { get; private set; }

    public UserAddress(string name, string family, string phoneNumber, string? email,
                    string province, string city, string postalCode, string postalAddress,
                    string nationalCode)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Province = province;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        NationalCode = nationalCode;
        ISActive = false;
    }
    public void Edit(string province, string city, string postalCode, string postalAddress,
           string phoneNumber, string name, string family, string nationalCode)
    {
        Guard(province, city, postalCode, postalAddress,
             phoneNumber, name, family, nationalCode);

        Province = province;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }

    public void SetActive()
    {
        ISActive = true;
    }

    public void Guard(string province, string city, string postalCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode)
    {
        NullOrEmptyDomainDataException.CheckString(province, nameof(province));
        NullOrEmptyDomainDataException.CheckString(city, nameof(city));
        NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
        NullOrEmptyDomainDataException.CheckString(postalAddress, nameof(postalAddress));
        NullOrEmptyDomainDataException.CheckString(name, nameof(name));
        NullOrEmptyDomainDataException.CheckString(family, nameof(family));
        NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

        if (IranianNationalIdChecker.IsValid(nationalCode) == false)
            throw new InvalidDomainDataException("کدملی نامعتبر است");
    }





}
