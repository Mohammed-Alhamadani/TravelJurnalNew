namespace TravelJurnal.Models
{
    public class DestinationTrip
    {
        public int DestinationTripId { get; set; } // Primary Key
        public int TripId { get; set; } // Foreign Key
        public int DestinationId { get; set; } // Foreign Key

        // Navigation properties
        public Trip Trip { get; set; }
        public Destination Destination { get; set; }
    }
}
