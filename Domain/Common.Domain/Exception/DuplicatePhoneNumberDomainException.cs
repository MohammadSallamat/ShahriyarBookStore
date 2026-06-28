namespace Domain.Common.Domain.Exception;

public class DuplicatePhoneNumberDomainException : BaseDomainException
{
    public DuplicatePhoneNumberDomainException() : base("PhoneNumber تکراری است")
    {
    }

    public DuplicatePhoneNumberDomainException(string message) : base(message)
    {
    }
}