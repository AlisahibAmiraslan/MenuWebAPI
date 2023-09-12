using MenuWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly DataContext _context;

        public AboutController(DataContext context)
        {
            _context = context;
        }

        //[DisableCors]   // cors işlemini passiv yapmak için, tüm işlemleri yapıla bilir ama disable olan yer çalışmaz
        [HttpGet]

        public async Task<ActionResult<List<About>>> GetAbout()
        {
            return Ok(await _context.Abouts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<About>> GetAbout(int id)
        {
            var about = await _context.Abouts.FindAsync(id);

            return Ok(about);   
        }

        [HttpPost]
        public async Task<ActionResult<List<About>>>CreateAbout(About about)
        {
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();

            return Ok(about);
        }
    }
}
