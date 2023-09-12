using MenuWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class FooterMenuController : ControllerBase
    {
        private readonly DataContext _context;

        public FooterMenuController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<FooterMenu>>> GetFooterMenu() 
        {
            return Ok(await _context.FooterMenus.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FooterMenu>> GetFooterMenu(int id)
        {
            var menu = await _context.FooterMenus.FindAsync(id);

            if(menu == null)
                return NotFound();

            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult<List<FooterMenu>>> CreateFooterMenu(FooterMenu menu)
        {
            _context.FooterMenus.Add(menu);
            await _context.SaveChangesAsync();

            return Ok(menu);
        }

        [HttpPut]
        public async Task<ActionResult<List<FooterMenu>>> UpdateFooterMenu(FooterMenu updateMenu)
        {
            var menu = await _context.FooterMenus.FindAsync(updateMenu.Id);

            if(menu == null)
                return NotFound();

            menu.Name = updateMenu.Name;
            menu.Url = updateMenu.Url;

            await _context.SaveChangesAsync();

            return Ok(updateMenu);
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<List<FooterMenu>>> DeleteFooterMenu(int id)
        {
            var menu = await _context.FooterMenus.FindAsync(id);
            
            if(menu == null)
                return NotFound();

            _context.FooterMenus.Remove(menu);
            await _context.SaveChangesAsync();
            return Ok(menu);
        }
    }
}
