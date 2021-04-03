using Evolutional.Project.CrossCutting.Configuration.AppModels;
using Newtonsoft.Json;

namespace Evolutional.Project.CrossCutting.Configuration
{
    public class AppSettings: Settings
    {
        public static AppSettings Settings => AppFileConfiguration<AppSettings>.Settings;

        [JsonProperty("Sqlconnections")]
        public SqlConnection Sqlconnections { get; set; }
        [JsonProperty("jwtTokenSettings")]
        public JwtTokenSettings JwtTokenSettings { get; set; }
    }
}
