namespace Evolutional.Project.Domain.Interfaces.Tools
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string login);
    }
}