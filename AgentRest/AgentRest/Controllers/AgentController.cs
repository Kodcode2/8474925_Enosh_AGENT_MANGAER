using AgentRest.Data;
using AgentRest.Model;
using AgentRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using AgentRest.Dto;

namespace AgentRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController(
        IServiceProvider serviceProvider,
        ApplicationDbContext _context,
        IHttpClientFactory clientFactory
        ) : ControllerBase
    {
        private IAgentService _agentService => serviceProvider.GetRequiredService<IAgentService>();
        private ITargetService _targetService => serviceProvider.GetRequiredService<ITargetService>();
        private IMissionService _missionService => serviceProvider.GetRequiredService<IMissionService>();

        /*private readonly List<AgentModel> Agents = [
            new() {Id = 1 , Image = "dcdcd", NickName = "2323", Status = AgentStatus.Active, XPostion = 2, YPostion = 4  }
            ];*/


        /*[HttpGet]
        public ActionResult Get([FromBody] object body)
        {
            int Id = 2;
            return Ok(new List<TargetModel>());
        }*/
        [HttpGet]
        public ActionResult GetAll(agentsDto agent)
        {
            return Ok(new List<AgentModel>(_context.AgentSet));
        }


        [HttpPost("create")]
        public async Task <ActionResult> NewAgent(agentsDto agent)
        {
            AgentModel model = new ();
            
            model.NickName = agent.nickname;
            model.photoUrl = agent.photoUrl;
            model.XPostion = agent.xposition;
            model.YPostion = agent.yposition;
            _context.AgentSet.AddAsync(model);
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPut("update{name}")]
        public async Task< ActionResult> UpdateMoveAgent(string name, agentsDto targets)
        {
            AgentModel model = new();
            if (model.NickName == name)
            {
                model.XPostion = targets.xposition;
                model.YPostion = targets.yposition;
                _context.AgentSet.UpdateRange(model);
                await _context.SaveChangesAsync();

            }
            return Ok();

        }

        public static AgentModel GetMove(AgentModel currentLocation, string direction)
        {
            Dictionary<string, Func<AgentModel, (int x, int y)>> map = new()
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
            return currentLocation;
        }





        /*[HttpPost("create")]
        public ActionResult <AgentModel> Create(AgentModel model)
        {
            int id = _context.AgentSet.Any() ? _context.AgentSet.Max(x => x.Id) + 1 : 1;
            model.Id = id;
            _context.AgentSet.Add(model);
            return Created();
        }*/

        /* [HttpPut("update{id}")]
         public ActionResult <AgentModel> Update(int id, AgentModel newAgent)
         {
             var ById = _context.AgentSet.Any() ? _context.AgentSet.FirstOrDefault(a => a.Id == id) : null;
             ById.YPostion = newAgent.YPostion;
             ById.Missions = newAgent.Missions;
             ById.Status = newAgent.Status;
             ById.XPostion = newAgent.XPostion;
             return Ok(_context.AgentSet);

         }*/

        /*[HttpPost("create")]
        public ActionResult <agentsDto> NewAgent ( AgentModel model)
        {
            int id = _context.AgentSet.Any() ? _context.AgentSet.Max(a => a.Id) + 1 : 1;
            model.Id= id;
            _context.AgentSet.Add(model);
            return Created();
        }*/

        /* [HttpPut("{id}/move")]
         public ActionResult Move([FromBody] object body)
         {
             int a = 1;
             return Ok();
         }*/



        /* [HttpPost("create")]
         public ActionResult Create(AgentModel agent)
         {
             if (agent == null) { return BadRequest("the agent is null"); }

             try
             {
                 agentService.CreateAgent(agent);
                 return Ok(agent);
             }
             catch (Exception ex) 
             {
                 return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
             }          

         }*/
        /*[HttpPut("Update{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Update(int id, AgentModel newAgent)
        {
            return Ok();
        }*/

    }
}
