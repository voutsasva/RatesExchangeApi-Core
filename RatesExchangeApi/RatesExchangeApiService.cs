using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
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
        private const string GetLatestRatesUrl = "{0}/client/latest?apikey={1}&base_currency={2}&currencies={3}";
        private const string GetLatestDetailsRatesUrl = "{0}/client/latestdetails?apikey={1}&base_currency={2}&currencies={3}";
        private const string GetHistoryRatesUrl = "{0}/client/history?apiKey={1}&base_currency={2}&date={3}&currencies={4}";
        private const string GetHistoryDetailsRatesUrl = "{0}/client/historydetails?apiKey={1}&base_currency={2}&date={3}&currencies={4}";
        private const string GetHistoryRatesForCurrencyUrl = "{0}/client/historydates?apiKey={1}&currency={2}&from_date={3}";
        private const string ConvertCurrencyUrl = "{0}/client/convert?apiKey={1}&from={2}&amount={3}&date={4}&currencies={5}";
        private const string ConvertCurrencyDetailsUrl = "{0}/client/convertdetails?apiKey={1}&from={2}&amount={3}&date={4}&currencies={5}";
        private const string GetCurrenciesUrl = "{0}/client/currencies?apiKey={1}";

        public RatesExchangeApiService(string key)
        {
            _apiKey = key;
        }

        public async Task<BoolResponse> CheckIfApiIsOnline()
        {
            var requestUrl = string.Format(CultureInfo.InvariantCulture, CheckIfApiIsOnLineUrl, ApiBaseUrl);
            var resp = await HttpHandler.GetResponseFromUrlAsync<BoolResponse>(requestUrl).ConfigureAwait(false);
            return resp;
        }

        public async Task<RatesResponse> GetLatestRates(string baseCurrency, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetLatestRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, currencies);
            var resp = await HttpHandler.GetResponseFromUrlAsync<RatesResponse>(requestUrl).ConfigureAwait(false);
            return resp;
        }

        public Task<RatesDetailsResponse> GetLatestDetailsRates(string baseCurrency, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetLatestDetailsRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, currencies);
            return HttpHandler.GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        public Task<RatesResponse> GetHistoryRates(string baseCurrency, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, date, currencies);
            return HttpHandler.GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        public Task<RatesDetailsResponse> GetHistoryDetailsRates(string baseCurrency, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryDetailsRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, date, currencies);
            return HttpHandler.GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        public Task<RatesHistoryResponse> GetHistoryRatesForCurrency(string baseCurrency, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryRatesForCurrencyUrl, ApiBaseUrl, _apiKey, baseCurrency, date);
            return HttpHandler.GetResponseFromUrlAsync<RatesHistoryResponse>(requestUrl);
        }

        public Task<RatesResponse> ConvertCurrency(string fromCurrency, string amount, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, ConvertCurrencyUrl, ApiBaseUrl, _apiKey, fromCurrency, amount, date, currencies);
            return HttpHandler.GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        public Task<RatesDetailsResponse> ConvertCurrencyDetails(string fromCurrency, string amount, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, ConvertCurrencyDetailsUrl, ApiBaseUrl, _apiKey, fromCurrency, amount, date, currencies);
            return HttpHandler.GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        public Task<List<CurrenciesResponse>> GetCurrencies()
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetCurrenciesUrl, ApiBaseUrl, _apiKey);
            return HttpHandler.GetResponseFromUrlAsync<List<CurrenciesResponse>>(requestUrl);
        }





        #region Private methods

        private void ThrowExceptionIfApiKeyInvalid()
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("No API key was given.");
            }
        }

        #endregion
    }
}
