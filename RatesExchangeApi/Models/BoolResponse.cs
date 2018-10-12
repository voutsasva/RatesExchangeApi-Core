using System.Runtime.Serialization;

namespace RatesExchangeApi.Models
{
    /// <summary>
    /// Bool response
    /// </summary>
    [DataContract]
    public class BoolResponse
    {
        /// <summary>
        /// Result is True or False
        /// </summary>
        [DataMember(Name = "result")]
        public bool Result { get; set; }
    }
}
