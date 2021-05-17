using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PokemonDomain.Models;
using System.Web.Http;
using System.Collections.Generic;

namespace PokemonDomain.Extensions
{
    /// <summary>
    /// Extension method for HttpClient Class to use generic objects.
    /// </summary>
    public static class HttpClientExtensions
    {
        private static readonly MediaTypeWithQualityHeaderValue JsonMediaType = new MediaTypeWithQualityHeaderValue("application/json");
        private static readonly JsonMediaTypeFormatter JsonFormatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            },
        };

        public static Task<ClientResponse<TResponse>> GetAsync<TResponse>(this HttpClient httpClient, string path)
        {
            AddAcceptHeader(httpClient);
            return Process<TResponse>(httpClient.GetAsync(path));
        }

        public static Task<ClientResponse<TResponse>> PostAsync<TRequest, TResponse>(this HttpClient httpClient, string path, TRequest request)
        {
            return Process<TResponse>(httpClient.PostAsync(path, request, JsonFormatter));
        }

        private static async Task<ClientResponse<TResponse>> Process<TResponse>(Task<HttpResponseMessage> requestTask)
        {
            var response = await requestTask;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseHeaders = response.Headers.ToDictionary(header => header.Key, header => string.Join("", header.Value));

                return new ClientResponse<TResponse>
                {
                    Content = JsonConvert.DeserializeObject<TResponse>(content),
                    Header = responseHeaders,
                };
            }
            catch (Exception)
            {
                throw new ApiException(response);
            }
        }

        private static void AddAcceptHeader(HttpClient httpClient)
        {
            if (!httpClient.DefaultRequestHeaders.Accept.Contains(JsonMediaType))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(JsonMediaType);
            }
        }
    }
}