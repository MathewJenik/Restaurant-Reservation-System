using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class TableSitting
    {
        [Key]
        public int Id { get; set; }
        public int SittingId { get; set; }
        public Sitting Sitting { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
    }
}
