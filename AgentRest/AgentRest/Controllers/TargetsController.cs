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
    public class TargetsController(
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
            return Ok(new List<TargetModel>(_context.TargetSet));
        }

        [HttpPost("create")]
        public ActionResult<TargetModel> Create(TargetModel model)
        {
            int id = _context.TargetSet.Any() ? _context.TargetSet.Max(x => x.Id) + 1 : 1;
            model.Id = id;
            _context.TargetSet.Add(model);
            return Created();
        }

        [HttpPut("update{id}")]
        public ActionResult<TargetModel> Update(int id, TargetModel newTarget)
        {
            var ById = _context.TargetSet.Any() ? _context.TargetSet.FirstOrDefault(a => a.Id == id) : null;
            ById.Id = newTarget.Id;
            ById.Role = newTarget.Role;
            ById.XPostion = newTarget.XPostion;
            ById.YPostion = newTarget.YPostion;
            ById.Status = newTarget.Status;
            ById.Name = newTarget.Name;
            
            return Ok(_context.TargetSet);

        }

        /*[HttpPost]
        public ActionResult Post([FromBody] object body )
        {
            int a = 1;
            return Ok();
        }

        [HttpGet]
        public ActionResult Get([FromBody] object body)
        {
            int Id = 2;
            return Ok(new List<TargetModel>());
        }

        [HttpPut("{id}/move")]
        public ActionResult Move([FromBody] object body)
        {
            int a = 1;
            return Ok();
        }*/
    }
}
