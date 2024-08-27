using AgentRest.Data;
using AgentRest.Dto;
using AgentRest.Model;
using AgentRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public ActionResult GetAll(targetsDto target)
        {

            return Ok(new List<TargetModel>(_context.TargetSet));
            /*            return Ok (_context.TargetSet);
            */
        }

        /*[HttpPost("create")]
        public ActionResult<TargetModel> Create(TargetModel model)
        {
            int id = _context.TargetSet.Any() ? _context.TargetSet.Max(x => x.Id) + 1 : 1;
            model.Id = id;
            _context.TargetSet.Add(model);
            return Created();
        }*/

        [HttpPost("create")]
        public async Task<ActionResult> NewAgent(targetsDto target)
        {
            TargetModel model = new ();
           
            model.photoUrl = target.photoUrl;
            model.XPostion = target.xposition;
            model.YPostion = target.yposition;
            model.Name = target.name;
            _context.TargetSet.AddAsync(model);
            await _context.SaveChangesAsync();
                      
            return Created();
        }

        [HttpPut("update{name}")]
        public async Task<ActionResult> Update(string name, targetsDto targets)
        {
            TargetModel model = new();
            if (model.Name == name)
            {
                model.XPostion = targets.xposition;
                model.YPostion = targets.yposition;
                _context.TargetSet.UpdateRange(model);
                await _context.SaveChangesAsync();

            }
            return Ok();
        }
        


        /*[HttpPut("update{id}")]
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

        }*/

        public static TargetModel GetMove(TargetModel currentLocation, string direction)
        {
            Dictionary<string, Func<TargetModel, (int x, int y)>> map = new()
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
