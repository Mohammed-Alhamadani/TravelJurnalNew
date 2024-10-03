namespace TravelJurnal.Models
{
    public class TripDestination
    {

        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

    }
}
