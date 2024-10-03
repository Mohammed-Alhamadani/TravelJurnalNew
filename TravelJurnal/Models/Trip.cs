using System.ComponentModel.DataAnnotations;
using TravelJurnal.Data.Migrations;

namespace TravelJurnal.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public string Title { get; set; }
        public int TravelerId { get; set; }
        public TravellerProfile Traveler { get; set; }
        public ICollection<Destination> Destinations { get; set; }
        public ICollection<Entry> Entries { get; set; }



    }
}
