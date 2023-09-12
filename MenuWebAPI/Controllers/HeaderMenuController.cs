using MenuWebAPI.Data;
using MenuWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderMenuController : ControllerBase
    {
        
        private readonly DataContext _context;


        public HeaderMenuController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<HeaderMenu>>> GetHeaderMenu()
        {
            return Ok(await _context.HeaderMenus.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HeaderMenu>> GetHeaderMenu(int id)
        {
            var menu = await _context.HeaderMenus.FindAsync(id);

            if(menu == null)
                return NotFound();

            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult<List<HeaderMenu>>> CreateHeaderMenu(HeaderMenu menu)
        {
            _context.HeaderMenus.Add(menu);
            await _context.SaveChangesAsync();

            return Ok(menu);
        }

        [HttpPut()]
        public async Task<ActionResult<List<HeaderMenu>>> UpdateHeaderMenu(HeaderMenu updateMenu)
        {
            var menu = await _context.HeaderMenus.FindAsync(updateMenu.Id);
           

            if (menu == null)
                return NotFound();

            
            menu.Name = updateMenu.Name;
            menu.Description = updateMenu.Description;
            menu.Url = updateMenu.Url;

            await _context.SaveChangesAsync();

            return Ok(menu);
        }

        [HttpDelete("id")]

        public async Task <ActionResult<List<HeaderMenu>>> DeleteHeaderMenu(int id)
        {
            var menu = await _context.HeaderMenus.FindAsync(id);

            if ( menu == null)
                return NotFound();  

            _context.HeaderMenus.Remove(menu);  
            await _context.SaveChangesAsync();

            return Ok(menu);
        }

    }
}
