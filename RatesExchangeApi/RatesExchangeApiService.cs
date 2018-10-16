using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RatesExchangeApi.Models;

namespace RatesExchangeApi
{
    /// <summary>
    /// The Rates Exchange Api Service. Returns ECB rates,
    /// and provides API usage information.
    /// </summary>
    public class RatesExchangeApiService
    {

        /// <summary>
        /// The API key to use in all requests.
        /// </summary>
        private readonly string _apiKey;

        #region Consts

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
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RatesExchangeApiService"/> class.
        /// </summary>
        /// <param name="key">
        /// The API key to use.
        /// </param>
        public RatesExchangeApiService(string key)
        {
            _apiKey = key;
        }

        /// <summary>
        /// Asynchronously checks if API is online or not
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="BoolResponse"/> with the requested data.
        /// </returns>
        public async Task<BoolResponse> CheckIfApiIsOnline()
        {
            var requestUrl = string.Format(CultureInfo.InvariantCulture, CheckIfApiIsOnLineUrl, ApiBaseUrl);
            return await HttpHandler.GetResponseFromUrlAsync<BoolResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously retrieves the latest rates
        /// </summary>
        /// <param name="baseCurrency">
        /// Base currency (ISO format)
        /// </param>
        /// <param name="currencies">
        /// List of currencies (ISO format) separated by a comma (optional)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesResponse> GetLatestRates(string baseCurrency, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetLatestRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, currencies);
            return await HttpHandler.GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously retrieves the latest rates
        /// </summary>
        /// <param name="baseCurrency">
        /// Base currency (ISO format)
        /// </param>
        /// <param name="currencies">
        /// List of currencies (ISO format) separated by a comma (optional)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesDetailsResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesDetailsResponse> GetLatestDetailsRates(string baseCurrency, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetLatestDetailsRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, currencies);
            return await HttpHandler.GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously retrieves rates for a specific date
        /// </summary>
        /// <param name="baseCurrency">
        /// Base currency (ISO format)
        /// </param>
        /// <param name="currencies">
        /// List of currencies (ISO format) separated by a comma (optional)
        /// </param>
        /// <param name="date">
        /// Rates date (format: YYYY-MM-DD)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesResponse> GetHistoryRates(string baseCurrency, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, date, currencies);
            return await HttpHandler.GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously retrieves rates for a specific date
        /// </summary>
        /// <param name="baseCurrency">
        /// Base currency (ISO format)
        /// </param>
        /// <param name="currencies">
        /// List of currencies (ISO format) separated by a comma (optional)
        /// </param>
        /// <param name="date">
        /// Rates date (format: YYYY-MM-DD)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesDetailsResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesDetailsResponse> GetHistoryDetailsRates(string baseCurrency, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryDetailsRatesUrl, ApiBaseUrl, _apiKey, baseCurrency, date, currencies);
            return await HttpHandler.GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously retrieves rates for a specific currency <see cref="baseCurrency"/>
        /// </summary>
        /// <param name="baseCurrency">
        /// Base currency (ISO format)
        /// </param>
        /// <param name="date">
        /// Rates date (format: YYYY-MM-DD)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesHistoryResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesHistoryResponse> GetHistoryRatesForCurrency(string baseCurrency, string date)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetHistoryRatesForCurrencyUrl, ApiBaseUrl, _apiKey, baseCurrency, date);
            return await HttpHandler.GetResponseFromUrlAsync<RatesHistoryResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously converts from one currency <see cref="fromCurrency"/> to many <see cref="currencies"/> for a specific date.
        /// </summary>
        /// <param name="fromCurrency">
        /// Currency to convert (ISO format)
        /// </param>
        /// <param name="currencies">
        /// List of currencies (ISO format) separated by a comma (optional)
        /// </param>
        /// <param name="date">
        /// Rates date (format: YYYY-MM-DD)
        /// </param>
        /// <param name="amount">
        /// Amount to convert (ex: 100,50)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesResponse> ConvertCurrency(string fromCurrency, string amount, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, ConvertCurrencyUrl, ApiBaseUrl, _apiKey, fromCurrency, amount, date, currencies);
            return await HttpHandler.GetResponseFromUrlAsync<RatesResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously converts from one currency <see cref="fromCurrency"/> to many <see cref="currencies"/> for a specific date.
        /// </summary>
        /// <param name="fromCurrency">
        /// Currency to convert (ISO format)
        /// </param>
        /// <param name="currencies">
        /// List of currencies (ISO format) separated by a comma (optional)
        /// </param>
        /// <param name="date">
        /// Rates date (format: YYYY-MM-DD)
        /// </param>
        /// <param name="amount">
        /// Amount to convert (ex: 100,50)
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="RatesDetailsResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<RatesDetailsResponse> ConvertCurrencyDetails(string fromCurrency, string amount, string date, string currencies = null)
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, ConvertCurrencyDetailsUrl, ApiBaseUrl, _apiKey, fromCurrency, amount, date, currencies);
            return await HttpHandler.GetResponseFromUrlAsync<RatesDetailsResponse>(requestUrl);
        }

        /// <summary>
        /// Asynchronously retrieves all available currencies
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> for a <see cref="CurrenciesResponse"/> with the requested data, or null if the data was corrupted.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown when the service returned anything other than a 200 (Status OK) code.
        /// </exception>
        public async Task<List<CurrenciesResponse>> GetCurrencies()
        {
            ThrowExceptionIfApiKeyInvalid();
            var requestUrl = string.Format(CultureInfo.InvariantCulture, GetCurrenciesUrl, ApiBaseUrl, _apiKey);
            return await HttpHandler.GetResponseFromUrlAsync<List<CurrenciesResponse>>(requestUrl);
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
