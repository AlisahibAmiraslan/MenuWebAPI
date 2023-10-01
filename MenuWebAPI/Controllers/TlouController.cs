using MenuWebAPI.DTOs;
using MenuWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TlouController : ControllerBase
    {
        private readonly DataContext _context;

        public TlouController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacter()
        {
            return Ok(await _context.Characters.Include(c => c.Backpack).Include(c => c.Weapons).Include(c => c.Factions).ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var character = await _context.Characters
                .Include(b => b.Backpack)
                .Include(w => w.Weapons)
                .Include(f => f.Factions)
                .FirstOrDefaultAsync(c=> c.Id == id);

            return Ok(character);
        }

        [HttpPost]

        public async Task<ActionResult<List<Character>>>CreateCharacter(CharacterCreateDto request)
        {
            var newCaracter = new Character
            {
                Name = request.Name,
            };

            var backpack = new Backpack
            {
                Description = request.Backpack.Description,
                Character = newCaracter
            };

            var weapons = request.Weapons.Select(w => new Weapon {Name = w.Name, Character = newCaracter}).ToList();

            var factions = request.Faction.Select(f=> new Faction { Name = f.Name, Characters = new List<Character> { newCaracter } }).ToList();

            newCaracter.Backpack = backpack;
            newCaracter.Weapons = weapons;
            newCaracter.Factions = factions; 
            
            _context.Characters.Add(newCaracter);
            
            await _context.SaveChangesAsync();

            return Ok( await _context.Characters.Include(c=>c.Backpack).Include(c=>c.Weapons).Include(c=>c.Factions).ToListAsync() );
        }
    }
}
