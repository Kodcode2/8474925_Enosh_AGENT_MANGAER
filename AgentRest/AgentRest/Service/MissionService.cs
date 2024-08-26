using AgentRest.Data;

namespace AgentRest.Service
{
    public class MissionService(
        ApplicationDbContext context,
        IServiceProvider serviceProvider,
        IHttpClientFactory clientFactory
        ) : IMissionService

    {
        private ITargetService _targetService => serviceProvider.GetRequiredService<ITargetService>();
        private IAgentService _agentService => serviceProvider.GetRequiredService<IAgentService>();

    }
}
