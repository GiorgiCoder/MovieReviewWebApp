using System.ComponentModel.DataAnnotations;

namespace MovieReviewApp.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
