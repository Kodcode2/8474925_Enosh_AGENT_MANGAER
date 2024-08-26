using AgentRest.Data;
using AgentRest.Model;

namespace AgentRest.Service
{
    public class TargetService(
        IServiceProvider serviceProvider,
        ApplicationDbContext _dbContext,
        IHttpClientFactory clientFactory
        ) : ITargetService
    {
        private IAgentService _agentService = serviceProvider.GetRequiredService<IAgentService>();
        private IMissionService _missionService = serviceProvider.GetRequiredService<IMissionService>();
        public TargetModel CreateTarget(TargetModel agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("the target is null");
            }
            _dbContext.Add(agent);
            return agent;
        }

        public List<TargetModel> GetAgent(TargetModel agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent is null");
            }
            return _dbContext.TargetSet.ToList();

        }


       

        public TargetModel UpdateAgent(int id, TargetModel agent, TargetModel newAgent)
        {
            var byId = agent.Id;
            if (byId == null) { return newAgent; }
            newAgent.Status = agent.Status;
            newAgent.YPostion = agent.YPostion;
            newAgent.XPostion = agent.XPostion;
            _dbContext.SaveChanges();
            return newAgent;

        }

       
    }
}
