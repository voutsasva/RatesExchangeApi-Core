using System;
using System.Threading.Tasks;
using RatesExchangeApi;
using Newtonsoft.Json;

namespace ExampleApp
{
    internal static class Program
    {
        private const string ApiKey = "[YOUR_API_KEY]";

        private static void Main()
        {
            try
            {
                var client = new RatesExchangeApiService(ApiKey);
                CheckIfApiIsOnline(client).Wait();
                GetLatestRates(client, "EUR").Wait(); 
                ConvertCurrency(client, "USD", "100", "2018-06-25", "JPY,KRW,CHF,AUD,GBP,EUR").Wait();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
            Console.ReadKey();
        }

        private static async Task CheckIfApiIsOnline(RatesExchangeApiService client)
        {
            Console.WriteLine("-- Check if API is online");
            var parsed = JsonConvert.SerializeObject(await client.CheckIfApiIsOnline(), Formatting.Indented);
            Console.WriteLine(parsed);
        }

        private static async Task GetLatestRates(RatesExchangeApiService client, string baseCurrency)
        {
            Console.WriteLine("-- Get latest rates from ECB");
            var parsed = JsonConvert.SerializeObject(await client.GetLatestRates(baseCurrency), Formatting.Indented);
            Console.WriteLine(parsed);
        }

        private static async Task ConvertCurrency(RatesExchangeApiService client, string fromCurrency, string amount, string date, string currencies)
        {
            Console.WriteLine($"-- Convert {amount} {fromCurrency} to {currencies}.");
            var parsed = JsonConvert.SerializeObject(await client.ConvertCurrency(fromCurrency, amount, date, currencies), Formatting.Indented);
            Console.WriteLine(parsed);
        }
    }
}
