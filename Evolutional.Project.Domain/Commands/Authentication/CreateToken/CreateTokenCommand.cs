using MediatR;
using Newtonsoft.Json;

namespace Evolutional.Project.Domain.Commands.Authentication.CreateToken
{
    public sealed class CreateTokenCommand : IRequest<CreateTokenCommandResponse>
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
