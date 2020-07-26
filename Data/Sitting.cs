using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2RMSWS.Data
{
    public class Sitting
    {
        public Sitting()
        {
            TableSittings = new List<TableSitting>(); 
        }

        [Key]
        public int Id { get; set; }
        public int SittingTypeId { get; set; }
        public SittingType SittingType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public TimeSpan Duration { get { return EndDateTime.TimeOfDay - StartDateTime.TimeOfDay; } }
        public int Capacity { get; set; }
        public int SittingStatusId { get; set; }
        public SittingStatus SittingStatus { get; set; }
        public List<TableSitting> TableSittings { get; set; }

    }
}
