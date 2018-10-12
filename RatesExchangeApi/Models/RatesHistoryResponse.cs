using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RatesExchangeApi.Models
{
    /// <summary>
    /// History Rates
    /// </summary>
    [DataContract]
    public class RatesHistoryResponse
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

        /// <summary>
        /// Rates
        /// </summary>
        [DataMember(Name = "rates")]
        public List<HistoryRate> Rates { get; set; }
    }

    /// <summary>
    /// History Rate
    /// </summary>
    [DataContract]
    public class HistoryRate
    {
        /// <summary>
        /// Date rate
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }

        /// <summary>
        /// Rate value
        /// </summary>
        [DataMember(Name = "value")]
        public decimal Value { get; set; }
    }
}
