using System;

namespace Domain.Exceptions;

public class SendEmailException : Exception
{
    public SendEmailException(string message) : base(message)
    {

    }
}
