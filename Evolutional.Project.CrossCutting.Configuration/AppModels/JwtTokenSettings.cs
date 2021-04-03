using Newtonsoft.Json;

namespace Evolutional.Project.CrossCutting.Configuration.AppModels
{
    public class JwtTokenSettings
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("expiration")]
        public int Expiration { get; set; }

        [JsonProperty("secretKey")]
        public string SecretKey { get; set; }
    }
}
