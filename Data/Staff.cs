using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class Staff : Person
    {
        public int StaffId { get; set; }
        public Manager Manager { get; set; }

    }
}
