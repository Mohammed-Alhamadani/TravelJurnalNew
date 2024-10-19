using System.ComponentModel.DataAnnotations;

namespace TravelJurnal.Models
{
    public class Destination
    {
        [Key]
        public int DestinationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        public string ImagePath { get; set; }

    }
}
