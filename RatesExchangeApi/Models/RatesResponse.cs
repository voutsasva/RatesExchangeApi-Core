using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RatesExchangeApi.Models
{
    /// <summary>
    /// ECB Rates
    /// </summary>
    [DataContract]
    public class RatesResponse
    {
        /// <summary>
        /// Base currency (ISO)
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
        public IDictionary<string, decimal> Rates { get; set; }
    }
}
