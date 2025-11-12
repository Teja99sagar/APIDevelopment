using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Helpers;
using MusicApi.Models;
using System.Linq;

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


        [HttpPost]

        public async Task<IActionResult> Post([FromForm] Song song)
        {
            var imageUrl= await FileHelpers.UploadImage(song.Image);
            song.ImageUrl = imageUrl;
            var audioUrl = await FileHelpers.UploadFile(song.AudioFile);
            song.AudioUrl = audioUrl;
            song.UploadedDate = DateTime.Now;
            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var songs = await (from Songs in _dbContext.Songs
                               where Songs.IsFeatured == true
                               select new
                        {

                            Id = Songs.Id,
                            Title = Songs.Title,
                            ImageUrl = Songs.ImageUrl,
                            AudioUrl = Songs.AudioUrl,
                            Duration = Songs.Duration

                        }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllSongs(int? pageNumber,int? pageSize)
        {
            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 10;
            var songs = await (from Songs in _dbContext.Songs

                               select new
                               {

                                   Id = Songs.Id,
                                   Title = Songs.Title,
                                   ImageUrl = Songs.ImageUrl,
                                   AudioUrl = Songs.AudioUrl,
                                   Duration = Songs.Duration

                               }).ToListAsync();
            return Ok(songs.Skip((currentPage - 1)* currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from Songs in _dbContext.Songs
                               orderby Songs.UploadedDate descending
                               select new
                               {

                                   Id = Songs.Id,
                                   Title = Songs.Title,
                                   ImageUrl = Songs.ImageUrl,
                                   AudioUrl = Songs.AudioUrl,
                                   Duration = Songs.Duration

                               }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSong(string query)
        {
            var songs = await (from Songs in _dbContext.Songs
                               where Songs.Title.StartsWith(query)
                               select new
                               {

                                   Id = Songs.Id,
                                   Title = Songs.Title,
                                   ImageUrl = Songs.ImageUrl,
                                   AudioUrl = Songs.AudioUrl,
                                   Duration = Songs.Duration

                               }).ToListAsync();
            return Ok(songs);
        }

    }
}
