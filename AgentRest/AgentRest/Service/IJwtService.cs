namespace AgentRest.Service
{
    public interface IJwtService
    {
        string CreateJwtToken(string name);
    }
}
