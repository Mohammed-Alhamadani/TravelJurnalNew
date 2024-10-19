using System.ComponentModel.DataAnnotations;

namespace TravelJurnal.Models
{
    public class Entry
    {
        public int EntryId { get; set; }
        public string Description { get; set; }
        public int TripId { get; set; }
        public int DestinationId { get; set; }

        // Navigation properties
        public Trip Trip { get; set; }
        public Destination Destination { get; set; }

    }
}
