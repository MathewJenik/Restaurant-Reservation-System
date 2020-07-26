using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class Customer : Person
    {
        public int CustomerId { get; set; }
    }
}
