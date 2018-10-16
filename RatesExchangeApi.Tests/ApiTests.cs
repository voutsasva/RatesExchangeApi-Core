using System.Threading.Tasks;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace RatesExchangeApi.Tests
{
    [TestFixture]
    public class ApiTests
    {
        private const string ApiKey = "[YOUR_API_KEY]";
        private const string BaseCurrency = "EUR";
        private const string OtherCurrency = "USD";
        private const string HistoryDateForRates = "2018-06-01";

        [Fact]
        public void NullApiKeyThrowsException()
        {
            var client = new RatesExchangeApiService(null);
            Assert.That(async () => await client.GetLatestRates(BaseCurrency), Throws.InvalidOperationException);
        }

        [Fact]
        public void EmptyApiKeyThrowsException()
        {
            var client = new RatesExchangeApiService(string.Empty);
            Assert.That(async () => await client.GetLatestRates(BaseCurrency), Throws.InvalidOperationException);
        }

        [Fact]
        public async Task ValidApiKeyRetrievesData()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await RatesExchangeApiService.CheckIfApiIsOnline();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.Not.Null);
        }

        [Fact]
        public async Task CanGetLatestRates()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.GetLatestRates(BaseCurrency);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanGetLatestDetailsRates()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.GetLatestDetailsRates(BaseCurrency);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanGetHistoryRates()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.GetHistoryRates(OtherCurrency, HistoryDateForRates);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanGetHistoryDetailsRates()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.GetHistoryDetailsRates(OtherCurrency, HistoryDateForRates);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanGetHistoryRatesForDate()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.GetHistoryRatesForCurrency(OtherCurrency, HistoryDateForRates);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanConvertCurrency()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.ConvertCurrency(BaseCurrency, "100", HistoryDateForRates);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanConvertCurrencyDetails()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.ConvertCurrencyDetails(BaseCurrency, "100", HistoryDateForRates);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rates, Is.Not.Null);
        }

        [Fact]
        public async Task CanGetCurrencies()
        {
            var client = new RatesExchangeApiService(ApiKey);
            var result = await client.GetCurrencies();
            Assert.That(result, Is.Not.Null);
        }
    }
}
