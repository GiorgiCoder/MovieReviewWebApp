using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }
        public IEnumerable<ActorMovie> Cast { get; set; }
        public Country Country { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
    }
}
