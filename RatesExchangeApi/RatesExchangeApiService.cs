using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RatesExchangeApi.Models;

namespace RatesExchangeApi
{
    public class RatesExchangeApiService
    {

        /// <summary>
        /// The API key to use in all requests.
        /// </summary>
        private readonly string _apiKey;

        private const string ApiBaseUrl = "https://api.ratesexchange.eu";
        private const string CheckIfApiIsOnLineUrl = "{0}/client/checkapi";
        private const string GetLatestRatesUrl = "{0}/client/latest?apikey={1}&base_currency={2}";
        private const string GetLatestDetailsRatesUrl = "{0}/client/latestdetails?apikey={1}&base_currency={2}";
        private const string GetHistoryRatesUrl = "{0}/client/history?apiKey={1}&base_currency={2}&date={3}";
        private const string GetHistoryDetailsRatesUrl = "{0}/client/historydetails?apiKey={1}&base_currency={2}&date={3}";
        private const string GetHistoryRatesForCurrencyUrl = "{0}/client/historydates?apiKey={1}&currency={2}&from_date={3}";
        private const string ConvertCurrencyUrl = "{0}/client/convert?apiKey={1}&from={2}&amount={3}&date={4}";
        private const string ConvertCurrencyDetailsUrl = "{0}/client/convertdetails?apiKey={1}&from={2}&amount={3}&date={4}";
        private const string GetCurrenciesUrl = "{0}/client/currencies?apiKey={1}";

        public RatesExchangeApiService(string key)
        {
            _apiKey = key;
        }

        public Task<BoolResponse> CheckIfApiIsOnline()
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, CheckIfApiIsOnLineUrl, ApiBaseUrl);
            return GetResponseFromUrlAsync<BoolResponse>(requestUrl);
        }

        public async Task<RatesResponse> GetLatestRates(string baseCurrency)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetLatestRatesUrl, ApiBaseUrl, _apiKey, baseCurrency);
            var resp = await GetResponseFromUrlAsync<RatesResponse>(requestUrl).ConfigureAwait(false);
            return resp;
        }

        public Task<RatesDetailsResponse> GetLatestDetailsRates(string baseCurrency)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetLatestDetailsRatesUrl, ApiBaseUrl, _apiKey, baseCurrency);
            return GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        public Task<RatesResponse> GetHistoryRates(string baseCurrency, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, date);
            return GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        public Task<RatesDetailsResponse> GetHistoryDetailsRates(string baseCurrency, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryDetailsRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, date);
            return GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        public Task<RatesHistoryResponse> GetHistoryRatesForCurrency(string baseCurrency, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryRatesForCurrencyUrl, ApiBaseUrl, _apiKey, baseCurrency, date);
            return GetResponseFromUrlAsync<RatesHistoryResponse>(requestUrl);
        }

        public Task<RatesResponse> ConvertCurrency(string fromCurrency, string amount, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, ConvertCurrencyUrl, ApiBaseUrl, _apiKey, fromCurrency, amount, date);
            return GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        public Task<RatesDetailsResponse> ConvertCurrencyDetails(string fromCurrency, string amount, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, ConvertCurrencyDetailsUrl, ApiBaseUrl, _apiKey, fromCurrency, amount, date);
            return GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        public Task<List<CurrenciesResponse>> GetCurrencies()
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetCurrenciesUrl, ApiBaseUrl, _apiKey);
            return GetResponseFromUrlAsync<List<CurrenciesResponse>>(requestUrl);
        }





        #region Private methods

        private void ThrowExceptionIfApiKeyInvalid()
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("No API key was given.");
            }
        }

        private static async Task<T> GetResponseFromUrlAsync<T>(string requestUrl) where T : class
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

        #endregion
    }













    public class BaseException : Exception
    {
        public BaseException(string message) : base(message) { }
        public BaseException(string message, Exception inner) : base(message, inner) { }

        public string Code { get; set; }
        public string StatusCode { get; set; }
    }

    public class ValidationException : BaseException
    {
        public ValidationException(string code, string message) : base(message)
        {
            Code = code;
            StatusCode = HttpStatusCode.BadRequest.ToString();
        }
    }
}
