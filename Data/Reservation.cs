using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class Reservation
    {

        [Key]
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Guests { get; set; }
        public string Notes_Requirements { get; set; }
        public int ReservationTypeId { get; set; }
        public ReservationType ReservationType { get; set; }
        public Customer Customer { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public List<TableReservation> TableReservations { get; set; }
        public bool IsAssignedToTable {
            get { return TableReservations.Any(); }
        }
        public Reservation()
        {
            TableReservations = new List<TableReservation>();
        }

    }
}
