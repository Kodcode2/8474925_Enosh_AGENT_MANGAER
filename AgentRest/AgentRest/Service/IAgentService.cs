using AgentRest.Model;
using System.Collections.Generic;

namespace AgentRest.Service
{
    public interface IAgentService
    {
        AgentModel CreateAgent(AgentModel agent);
        AgentModel UpdateAgent(AgentModel agent, AgentModel newAgent);
        List<AgentModel> GetAgent(AgentModel agent);


    }
}
