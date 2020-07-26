using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class TableReservation
    {
        [Key]
        public int Id { get; set; }
        public int? TableSittingId { get; set; }
        public TableSitting TableSitting { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

    }
}
