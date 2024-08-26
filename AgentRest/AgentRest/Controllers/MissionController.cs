using AgentRest.Data;
using AgentRest.Model;
using AgentRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionController(
        IServiceProvider serviceProvider,
        ApplicationDbContext _context,
        IHttpClientFactory clientFactory
        ) : ControllerBase
    {
        private IAgentService _agentService => serviceProvider.GetRequiredService<IAgentService>();
        private ITargetService _targetService => serviceProvider.GetRequiredService<ITargetService>();
        private IMissionService _missionService => serviceProvider.GetRequiredService<IMissionService>();

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(new List<MissionModel>(_context.MissionSet));
        }

        [HttpPost("create")]
        public ActionResult<MissionModel> Create(MissionModel model)
        {
            int id = _context.AgentSet.Any() ? _context.AgentSet.Max(x => x.Id) + 1 : 1;
            model.Id = id;
            _context.MissionSet.Add(model);
            return Created();
        }

        [HttpPut("update{id}")]
        public ActionResult<MissionModel> Update(int id, MissionModel newMission)
        {
            var ById = _context.MissionSet.FirstOrDefault(a => a.Id == id);
/*            if (ById == null) { return BadRequest($"the {id} is not exist");
*/
            ById.Status = newMission.Status;
            ById.AgentId = newMission.AgentId;
            ById.Agent = newMission.Agent;
            ById.Target = newMission.Target;
            ById.TargetId = newMission.TargetId;
            ById.Id = newMission.Id;
            ById.ActionTime = newMission.ActionTime;
            return Ok(_context.MissionSet);

        }
    }
}
