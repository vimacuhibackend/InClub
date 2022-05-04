using System;

namespace Inclub.Domain.Exceptions
{
    public class InclubDomainException : Exception
    {
        public InclubDomainException()
        { }

        public InclubDomainException(string message)
            : base(message)
        { }

        public InclubDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
