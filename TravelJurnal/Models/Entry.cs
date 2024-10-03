using System.ComponentModel.DataAnnotations;

namespace TravelJurnal.Models
{
    public class Entry
    {
        [Key]
        public int EntryId { get; set; }
        public string Description { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

    }
}
