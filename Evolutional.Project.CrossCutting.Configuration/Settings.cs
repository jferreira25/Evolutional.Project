using Newtonsoft.Json;

namespace Evolutional.Project.CrossCutting.Configuration
{
    public class Settings
    {
        [JsonProperty(PropertyName = "applicationName")]
        public string ApplicationName { get; set; }
    }
}
