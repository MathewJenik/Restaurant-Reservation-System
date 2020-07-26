using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2RMSWS.Data;

namespace T2RMSWS.Models.ReservationsManagement
{
    public class ResStatusModel
    {

        public Reservation reservation { get; set; }
        public List<ReservationStatus> reservationStatusList {get; set;}
        
        public List<ReservationType> reservationTypeList { get; set; }

        // for editing the tables.
        public List<TableSitting> Tables { get; set; }
        public List<AreaModel> Areas { get; set; }


        public ResStatusModel() {
            reservationStatusList = new List<ReservationStatus>();
            reservationTypeList = new List<ReservationType>();
        }

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
