using System;
using System.Collections.Generic;
using System.Net;

namespace Application.Exceptions;

public class RestException : Exception
{
    private RestException() => _errors = new List<ErrorMessage>();
    public RestException(HttpStatusCode code, ErrorMessage error = null) : this()
    {
        Code = code;
        _errors.Add(error);
    }

    public RestException(HttpStatusCode code, IEnumerable<ErrorMessage> errors = null,bool? singleMessage=null) : this()
    {
        Code = code;
        _errors.AddRange(errors);
        SingleMessage = singleMessage;
    }

    public RestException(HttpStatusCode code, string messageCode, params string[] parameters) : this()
    {
        Code = code;
        _errors.Add(new ErrorMessage(messageCode, parameters));
    }

    public RestException(HttpStatusCode code, bool translate, string messageCode, params string[] parameters) : this()
    {
        Code = code;
        _errors.Add(new ErrorMessage(translate,messageCode, parameters));
    }

    private readonly List<ErrorMessage> _errors;
    public IReadOnlyCollection<ErrorMessage> Errors => _errors;

    public HttpStatusCode Code { get; }
    public bool? SingleMessage { get; }
}
