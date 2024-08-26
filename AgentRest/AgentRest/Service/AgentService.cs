using AgentRest.Data;
using AgentRest.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AgentRest.Service
{
    public class AgentService(
        IServiceProvider serviceProvider,
        ApplicationDbContext context,
        IHttpClientFactory clientFactory
        ) : IAgentService
    {
        private ITargetService _targetService => serviceProvider.GetRequiredService<ITargetService>();
        private IMissionService _missionService => serviceProvider.GetRequiredService<IMissionService>();



        public AgentModel CreateAgent(AgentModel agent)
        {
            int agentId = context.AgentSet.Any() ? context.AgentSet.Max(a => a.Id )+ 1 : 1;
            agentId = agent.Id;

            if (agentId == null) { throw new ArgumentNullException("the agent is null"); }
         
            context.Add(agent);
            return agent;
        }

     

        public List<AgentModel> GetAgent(AgentModel agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent is null");
            }
            return context.AgentSet.ToList();
            
        }

        public AgentModel UpdateAgent(AgentModel agent, AgentModel newAgent)
        {
            var byId = agent.Id;
            if(byId == null) { return newAgent; }
            throw new NotImplementedException();
        }

        /*public static LocationModel? GetMove(LocationModel currentLocation, string direction)
        {
            Dictionary<string, Func<LocationModel, (int x, int y)>> map = new()
            {
                {  "e", (location) => (0, 1) },
                {  "w", (location) => (0, -1) },
                {  "s", (location) => (1, 1) },
                {  "n", (location) => (-1, 1) },
                {  "nw", (location) => (-1, -1) },
                {  "ne", (location) => (-1, 1) },
                {  "sw", (location) => (1, -1) },
                {  "se", (location) => (1, 1) },
                {  "wn", (location) => (-1, -1) },
                {  "en", (location) => (-1, 1) },
                {  "ws", (location) => (1, -1) },
                {  "es", (location) => (1, 1) },
            };

            var (x, y) = map[direction](currentLocation);

            return IsLocationValid(x, y) ? new LocationModel() { X = x, Y = y } : null;*/



            /*public AgentModel UpdateAgent(AgentModel agent, AgentModel newAgent)
            {
                if (agent == null) { throw new ArgumentNullException("the id not exist"); }
                agent.Status = newAgent.Status;
                agent.NickName = newAgent.NickName;
                agent.Image = newAgent.Image;
                return agent;

            }
    */

        }
}
