using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public Manager Manager { get; set; }
        public Staff Staff { get; set; }
        public Member Member { get; set; }

    }
}
