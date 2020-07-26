using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public string TableNo { get; set; }
        public int TableCapacity { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
