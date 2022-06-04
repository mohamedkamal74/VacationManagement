using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VacationManagement.ApiControllers
{
    [Route("api/VacationPlans")]
    [ApiController]
    public class VacationPlansController : ControllerBase
    {
        private readonly VacationDbContext _context;

        public VacationPlansController(VacationDbContext context)
        {
           _context = context;
        }
        // GET: api/<VacationTypesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VacationTypesController>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                return Ok(_context.Employees.Where(v => v.Name.Contains(name)).ToList());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST api/<VacationTypesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VacationTypesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationTypesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
