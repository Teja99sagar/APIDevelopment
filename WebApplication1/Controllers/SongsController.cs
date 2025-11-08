using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             return Ok(await _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song= await _dbContext.Songs.FindAsync(id);

            if(song==null)
            {
                return NotFound("Record not Found");
            }
            return Ok(song);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> testGetSong(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound("Record not Found");
            }
            return Ok(song);
        }



        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songObj)
        {
            var song=await _dbContext.Songs.FindAsync(id);
            if(song==null)
            {
                return NotFound("Record not Found");
            }
            else
            {
                song.title = songObj.title;
                song.Language = songObj.Language;
                song.Duration = songObj.Duration;
                await _dbContext.SaveChangesAsync();
                return Ok("Record Updated");


            }


        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           var sonngVar= await _dbContext.Songs.FindAsync(id);
            if (sonngVar==null)
            {
                return NotFound("Record not Found");

            }
            else
            {
                _dbContext.Songs.Remove(sonngVar);
                await _dbContext.SaveChangesAsync();
                return Ok("Deleted");
            }
     



        }
    }
}
