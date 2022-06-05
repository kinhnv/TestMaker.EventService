using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.EventService.Infrastructure.Entities;

namespace TestMaker.EventService.Api.Controllers.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class MigrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MigrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _context.Database.Migrate();

            return Ok();
        }
    }
}
