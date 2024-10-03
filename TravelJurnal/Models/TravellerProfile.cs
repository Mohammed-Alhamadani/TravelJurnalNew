using System.ComponentModel.DataAnnotations;

namespace TravelJurnal.Models
{
    public class TravellerProfile
    {
        [Key]
        public int TravellerId { get; set; }
        public string TravellerName { get; set; }
        public string TravellerEmail { get; set; }

        public string Description { get; set; }
        public byte[] ProfilePicture { get; set; }
        public ICollection<Trip> Trip { get; set; }

    }
}
