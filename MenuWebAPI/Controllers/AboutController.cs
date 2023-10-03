using MenuWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    /* eğer Authorize controllerin başına koyarsak aşağıdakı gibi,
     o zaman tüm metodları giriş yapılmadan işlem yapılamaz, eğer biz tüm controller'a 
    authorize verdikse ama bazı metodların çalışmasını istiyorsak giriş izni olmadan o zaman
    Her controller'ın yanına "AllowAnonymous" koyarız
     */
    [Authorize]  // tümü controller için çalışsın authorize kuralı
    public class AboutController : ControllerBase
    {
        private readonly DataContext _context;

        public AboutController(DataContext context)
        {
            _context = context;
        }

        //[DisableCors]   // cors işlemini passiv yapmak için, tüm işlemleri yapıla bilir ama disable olan yer çalışmaz

    /*  eğer bir controller'de iki tane "POST" methodu varsa o zaman böyle yazıla bilir,
    bunun maksadı "GET" methodu browserde url yazdığımız zaman json bilgiler gözükmesin.
    */
        [HttpPost("GetAbout"), AllowAnonymous] // bu sadece bu method giriş yapılmadan kullanılsın demektir
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
    /* Authorize, eğer bir methodungiriş olmadan çalışmasını istemiyorsak o zaman kullanılmalıdı
     şuan about contreoller post methodu giriş yapılmazsa çalışmaz
    [HttpPost,Authorize] -- bu sadece bu method için çalışsın kural */
      [HttpPost]
  public async Task<ActionResult<List<About>>>CreateAbout(About about)
  {
      _context.Abouts.Add(about);
      await _context.SaveChangesAsync();

      return Ok(about);
  }
}
}
