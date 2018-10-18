# RatesExchangeApi-Core

[![NuGet](https://img.shields.io/badge/nuget-1.0.1-blue.svg)](https://www.nuget.org/packages/RatesExchangeApi)

A .NET Standard Library for using the [Rates Exchange API](https://www.ratesexchange.eu/).

## Installation

[NuGet](https://www.nuget.org/packages/RatesExchangeApi/): `Install-Package RatesExchangeApi`

## Usage

The main class is [`RatesExchangeApiService`](https://github.com/voutsasva/RatesExchangeApi-Core/blob/master/RatesExchangeApi/RatesExchangeApiService.cs). When using it you will need provide your API key after [signing up](https://www.ratesexchange.eu/Account/Register) for an account.

An example console app is included. In order to use it replace the `[YOUR_API_KEY]` with your own API key from rates exchange api.

### Quick Start

```c#
using RatesExchangeApi;
using RatesExchangeApi.Models;

...

var client = new RatesExchangeApiService("[YOUR_API_KEY]");

var result = await client.CheckIfApiIsOnline();

var rates = await client.GetLatestRates("USD");

...

```

## Tests

NUnit is used for some simple integration tests with the actual web service. To run the tests, a valid API key must be added. Replace the `[YOUR_API_KEY]` in `ApiTests.cs` with your own API key from rates exchange api.
You can see an example usage in the [api tests](https://github.com/voutsasva/RatesExchangeApi-Core/blob/master/RatesExchangeApi.Tests/ApiTests.cs).

## Author
Vassilis Voutsas, voutsasva@gmail.com

## License
ECB Currency Converter is released under the MIT license. See [LICENSE](https://github.com/voutsasva/RatesExchangeApi-Core/blob/master/LICENSE) for more information.
