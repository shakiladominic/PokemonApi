using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace PokemonDomain
{
    [Serializable]
    public class ApiException : Exception
    {

        public override string Message { get; }
        public int HttpStatusCode { get; }

        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public ApiException(HttpResponseMessage message)
        {
            Message = message.ReasonPhrase;
            HttpStatusCode = (int)message.StatusCode;
        }

    }
}
