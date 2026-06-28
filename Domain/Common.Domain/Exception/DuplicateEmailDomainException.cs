namespace Domain.Common.Domain.Exception;

public class DuplicateEmailDomainException : BaseDomainException
{
    public DuplicateEmailDomainException() : base("Email تکراری است")
    {
    }

    public DuplicateEmailDomainException(string message) : base(message)
    {
    }
}
