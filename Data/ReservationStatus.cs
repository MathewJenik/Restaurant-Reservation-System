using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class ReservationStatus
    {

        [Key]
        public int Id { get; set; }
        public string Desc { get; set; }
    }
}
