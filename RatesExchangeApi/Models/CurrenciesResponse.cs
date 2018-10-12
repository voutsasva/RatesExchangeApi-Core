using System.Runtime.Serialization;

namespace RatesExchangeApi.Models
{
    /// <summary>
    /// Currencies Response
    /// </summary>
    [DataContract]
    public class CurrenciesResponse
    {
        /// <summary>
        /// Currency ISO Symbol
        /// </summary>
        [DataMember(Name = "symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Currency description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
