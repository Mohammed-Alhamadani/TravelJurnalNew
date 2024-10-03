using System.ComponentModel.DataAnnotations;

namespace TravelJurnal.Models
{
    public class Destination
    {
        [Key]
        public int DestinationId { get; set; }
        public string Location { get; set; } = string.Empty;

        public ICollection<Trip> Trips { get; set; }
        public ICollection<Entry> Entry { get; set; }



    }
}
