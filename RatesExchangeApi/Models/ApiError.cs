using System.Runtime.Serialization;

namespace RatesExchangeApi.Models
{
    [DataContract]
    public class ApiError
    {
        [DataMember(Name = "Code")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "Message")]
        public string Error { get; set; }
    }
}
