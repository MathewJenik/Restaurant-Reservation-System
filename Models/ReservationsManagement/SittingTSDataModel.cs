using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2RMSWS.Data;

namespace T2RMSWS.Models
{
    public class SittingTSDataModel
    {
        public Sitting sitting { get; set; }
        
        public int[][] timeSlots { get; set; }

    }
}



