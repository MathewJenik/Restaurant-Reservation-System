using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2RMSWS.Data;

namespace T2RMSWS.Models.Reports
{
    public class ReportsDataModel
    {

        public int TotalReservations { get; set; }
        public int Seated { get; set; }
        public int Pending { get; set; }
        public int Cancelled { get; set; }
        public List<int> CurrentYearsReservationsByMonth { get; set; }
        public List<int> CurrentWeeksReservations { get; set; }

        public int startingDay { get; set; }
        public List<int> DaysOfWeek { get; set; }

        public List<int[]> MonthTableData { get; set; } // count of reservations made by type per month (walk in, phone etc).

        public ReportsDataModel() {

            CurrentYearsReservationsByMonth = new List<int>();
            CurrentWeeksReservations = new List<int>();
            DaysOfWeek = new List<int>();

            MonthTableData = new List<int[]>();
        }
        public int Online { get; set; }
        public int InPerson { get; set; }
        public int Phone { get; set; }
        public int Email { get; set; }


    }

  
    public class BookingType
    {
        public string Description { get; set; }
    
    }

    public class WeeklyCap
    {
        public int Guests { get; set; }
        public int Capacity { get; set; }
        public int SitingTypeId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }

    public class Staff
    {
        public int MyProperty { get; set; }
    }
    

    
}
