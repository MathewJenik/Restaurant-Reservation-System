using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2RMSWS.Data;

namespace T2RMSWS.Models.SittingsManagement
{
    public class CreateVM
    {
        public Sitting Sitting { get; set; }
        public List<TableSitting> Tables { get; set; }
        public List<AreaModel> Areas { get; set; }
    }

    public class TableSitting
    {
        public int AreaId { get; set; }
        public string TableNo { get; set; }
        public int Capacity { get; set; }
        public Table Table { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
        public int TableId { get; set; }
    }

    public class AreaModel
    {
        public int AreaId { get; set; }
        public string Description { get; set; }
    }
}
