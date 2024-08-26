using AgentRest.Data;
using AgentRest.Model;
using AgentRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
        public ActionResult GetAll()
        {
            return Ok(new List<AgentModel>(_context.AgentSet));
        }

        [HttpPost("create")]
        public ActionResult <AgentModel> Create(AgentModel model)
        {
            int id = _context.AgentSet.Any() ? _context.AgentSet.Max(x => x.Id) + 1 : 1;
            model.Id = id;
            _context.AgentSet.Add(model);
            return Created();
        }

        [HttpPut("update{id}")]
        public ActionResult <AgentModel> Update(int id, AgentModel newAgent)
        {
            var ById = _context.AgentSet.Any() ? _context.AgentSet.FirstOrDefault(a => a.Id == id) : null;
            ById.YPostion = newAgent.YPostion;
            ById.Missions = newAgent.Missions;
            ById.Status = newAgent.Status;
            ById.XPostion = newAgent.XPostion;
            return Ok(_context.AgentSet);

        }

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
