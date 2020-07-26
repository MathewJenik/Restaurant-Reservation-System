using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T2RMSWS.Data;

namespace T2RMSWS.Models.TableReservations
{
    public class TableReservationsModel
    {
        public List<TR> TRs { get; set; }
        public List<SittingEvent> SittingEvents { get; set; }
    }
    public class TR
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public int Guests { get; set; }
        public string Notes { get; set; }
    }
    public class SittingEvent
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ResourceId { get; set; }
        public string Rendering { get; set; }
        public string Color { get; set; }
        public int GroupId { get;  set; }
    }

}
