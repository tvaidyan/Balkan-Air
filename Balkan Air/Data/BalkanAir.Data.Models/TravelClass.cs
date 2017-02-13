﻿namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class TravelClass
    {
        public TravelClass()
        {
            this.Seats = new HashSet<Seat>();
            this.ReservedSeat = true;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public TravelClassType Type { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Meal { get; set; }
 
        public bool PriorityBoarding { get; set; }

        public bool ReservedSeat { get; set; }

        public bool EarnMiles { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Invalid travel class price!")]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int FlightId { get; set; }

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        [NotMapped]
        public int AvailableSeats
        {
            get
            {
                return this.Seats
                    .Where(s => !s.IsReserved)
                    .Count();
            }
        }
    }
}
