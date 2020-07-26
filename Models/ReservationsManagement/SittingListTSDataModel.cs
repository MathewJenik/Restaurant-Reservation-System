using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using T2RMSWS.Data;

namespace T2RMSWS.Models
{
    public class SittingListTSDataModel
    {

        
        public List<Sitting> SittingList { get; set; }

        public List<int[][]> TimeSlotList { get; set; } // has a list of times slots, so first one will be for the first sitting,
        // then the times follow afterwards, timeSlot 0 [9] etc.
        //public int[][] timeSlots { get; set; }
        public int[] DaysAvailableTimes { get; set; }
        public List<int> DaysAvailableTimesList { get; set; }

        public int Counter { get; set; }

        public Reservation Reservation { get; set; }
        public List<Area> AreasList { get; set; }

        // previously entered data.
        public int Guests { get; set; } // including the person making the booking.
        public DateTime Duration { get; set; } // 1-6 (30 mins to 3 hours in increments of 30 mins.)
        public DateTime Date { get; set; } // 

        //
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public int StartTimeSlot { get; set; }
        public string AreaPicked { get; set; }
        //
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //

        public string Notes_Requirements { get; set; }


        // staff detail area:

        public string Email { get; set; }
        public string Phone { get; set; }
        public int ReservationTypePickedId { get; set; }
        public List<ReservationType> ReservationTypeList { get; set; }

        //public List<Table> Tables { get; set; }

        public List<TableSitting> Tables { get; set; }
        public List<AreaModel> Areas { get; set; }


        //

        public List<bool> StaffNeededForSittingList { get; set; }


        //Initialise the lists required.

        public SittingListTSDataModel()
        {
            SittingList = new List<Sitting>();
            TimeSlotList = new List<int[][]>();
            //daysAvailableTimes = new int[]();
            DaysAvailableTimesList = new List<int>();
            AreasList = new List<Area>();
            ReservationTypeList = new List<ReservationType>();
            //Tables = new List<TableSitting>();

            StaffNeededForSittingList = new List<bool>();
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
