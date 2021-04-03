using Newtonsoft.Json;

namespace Evolutional.Project.CrossCutting.Configuration.AppModels
{
    public class SqlConnection
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
    }
}
