using AgentRest.Model;

namespace AgentRest.Service
{
    public interface ITargetService
    {
        TargetModel CreateTarget(TargetModel agent);
        TargetModel UpdateAgent(int id, TargetModel agent, TargetModel newAgent);
        List<TargetModel> GetAgent(TargetModel agent);


    }
}

