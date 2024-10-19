using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelJurnal.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Title { get; set; }
        public int TravelerId { get; set; }

        // Navigation property
        public ICollection<DestinationTrip> DestinationTrips { get; set; } = new List<DestinationTrip>();
    }
}
