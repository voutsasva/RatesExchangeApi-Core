using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RatesExchangeApi.Models
{
    /// <summary>
    /// ECB Rates details
    /// </summary>
    [DataContract]
    public class RatesDetailsResponse
    {
        /// <summary>
        /// Base currency
        /// </summary>
        [DataMember(Name = "base")]
        public string Base { get; set; }

        /// <summary>
        /// Rates date
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }

        /// <summary>
        /// Rates
        /// </summary>
        [DataMember(Name = "rates")]
        public List<Rate> Rates { get; set; }
    }

    /// <summary>
    /// Rate
    /// </summary>
    [DataContract]
    public class Rate
    {
        /// <summary>
        /// Currency ISO Symbol
        /// </summary>
        [DataMember(Name = "symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Currency description
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Rate value
        /// </summary>
        [DataMember(Name = "value")]
        public decimal Value { get; set; }
    }
}
