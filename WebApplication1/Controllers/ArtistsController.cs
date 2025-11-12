using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Helpers;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private ApiDbContext _dbContext;


        public ArtistsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist)
        {
            var imageurl = await FileHelpers.UploadImage(artist.Image);
            artist.ImageUrl = imageurl;
            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            //var artists = await _dbContext.Artists.ToListAsync();
            //return Ok(artists);

            var artists = await (from artist in _dbContext.Artists
                          select new
                          {
                              Id = artist.Id,
                              Name = artist.Name,
                              ImageUrl = artist.ImageUrl


                          }).ToListAsync();

            return Ok(artists);

            
            
            
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> ArtistDetails(int artistId)
        {
            var artistDetails=await _dbContext.Artists.Where(a=>a.Id==artistId).Include(a => a.Songs).ToListAsync();
            return Ok(artistDetails);

        }
    }
}
