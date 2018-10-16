using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RatesExchangeApi.Models;

namespace RatesExchangeApi
{
    internal static class HttpHandler
    {
        internal static async Task<T> GetResponseFromUrlAsync<T>(string requestUrl) where T : class
        {
            var compressionHandler = GetCompressionHandler();
            using (var client = new HttpClient(compressionHandler))
            {
                var response = await client.GetAsync(requestUrl).ConfigureAwait(false);
                return await ParseForecastFromResponse<T>(response).ConfigureAwait(false);
            }
        }

        private static HttpClientHandler GetCompressionHandler()
        {
            var compressionHandler = new HttpClientHandler();
            if (compressionHandler.SupportsAutomaticDecompression)
            {
                compressionHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            return compressionHandler;
        }

        private static async Task<T> ParseForecastFromResponse<T>(HttpResponseMessage response) where T : class
        {
            var responseStream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ApiError>(responseStream);
                throw new ValidationException(error.ErrorCode, error.Error);
            }
            var resp = JsonConvert.DeserializeObject<T>(responseStream);
            return resp;
        }

    }
}
