using Newtonsoft.Json;

namespace Evolutional.Project.Domain.Commands.Authentication.CreateToken
{
    public sealed class CreateTokenCommandResponse
    {
        public CreateTokenCommandResponse(string token)
        {
            Token = token;
        }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
