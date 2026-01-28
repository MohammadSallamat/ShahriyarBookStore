using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Application.UsersApp.AddAdress;

public class AddAddressUserCommand : IBaseCommand
{
    public AddAddressUserCommand(long userId, string name, string family, PhoneNumber phoneNumber,
                    string province, string city, string postalCode, string postalAddress,
                    string nationalCode)
    {

        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        NationalCode = nationalCode;
    }
    public long UserId { get; set; }
    public string Province { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
}
