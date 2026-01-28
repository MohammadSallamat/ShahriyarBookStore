using Application.CommonApplication;
using Application.UsersApp.AddAdress;
using Domain.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersApp.EditAddress;

public class EditUserAddressCommand : IBaseCommand
{
    public EditUserAddressCommand(string Province, string city, string postalCode, string postalAddress, PhoneNumber phoneNumber, string name, string family, string nationalCode, long userId, long AddressId)
    {
        Province = Province;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
        UserId = userId;
        AdressId = AddressId;

    }

    public long UserId { get; set; }
    public long AdressId { get; private set; }
    public string Province { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }

}
