using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonDomainTests.Helpers
{
    public class TestMessageHandler : HttpMessageHandler
    {
        private readonly IDictionary<string, HttpResponseMessage> _messages;

        public TestMessageHandler(IDictionary<string, HttpResponseMessage> messages)
        {
            _messages = messages;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            if (_messages.ContainsKey(request.RequestUri.ToString()))
            {
                response = _messages[request.RequestUri.ToString()] ?? new HttpResponseMessage(HttpStatusCode.NotFound);
                response.RequestMessage = request;
            }
            return Task.FromResult(response);
        }

    }
}
