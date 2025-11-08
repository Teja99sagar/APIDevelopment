using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title is required")]
        public string title { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }

    
    }
}
