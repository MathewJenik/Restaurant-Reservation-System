using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SQLitePCL;
using T2RMSWS.Data;
using T2RMSWS.Migrations;
using T2RMSWS.Models;
using T2RMSWS.Models.ReservationsManagement;
using T2RMSWS.Models.SittingsManagement;

namespace T2RMSWS.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        private Reservation res = new Reservation();
        //private List<Sitting> sits;


        private async Task<int[][]> getTimeSlotFromDuration(int[][] times, DateTime dur)
        {

            int[][] timeSlots = times;
            int[][] newTimeSlots = new int[times.Length][];
            var duration = dur;
            var availabilityCounter = 0;
            var timeLength = timeSlots.Length;
            int timeSlotsNeeded = dur.Hour * 2 + (dur.Minute / 30);

            for (int i = 0; i < timeSlots.Length; i++)
            {
                var available = true;
                for (int j = 0; j < timeSlotsNeeded; j++)
                {
                    if (j >= timeSlots.Length)
                    {
                        available = false;
                    }
                    else
                    {
                        newTimeSlots[i] = new int[5];
                        if (available == true)
                        {
                            if (i + j < timeSlots.Length)
                            {
                                if (timeSlots[i + j][3] == 1)
                                {
                                    available = false;
                                }
                            }
                            else
                            {
                                available = false;
                            }
                        }
                    }
                }

                if (available == true)
                {
                    newTimeSlots[availabilityCounter] = timeSlots[i];
                    newTimeSlots[availabilityCounter][0] = timeSlots[i][0];
                    newTimeSlots[availabilityCounter][1] = timeSlots[i][1];
                    newTimeSlots[availabilityCounter][2] = timeSlots[i][2];
                    newTimeSlots[availabilityCounter][3] = timeSlots[i][3];
                    newTimeSlots[availabilityCounter][4] = timeSlots[i][4];
                    availabilityCounter++;
                }
                else
                {
                    newTimeSlots[availabilityCounter][3] = 1;
                    availabilityCounter++;
                }

            }

            if (availabilityCounter == 0)
            {
                newTimeSlots = new int[0][];
            }

            return newTimeSlots;
        }

        //function to create the timeslots for the sitting.
        private async Task<int[][]> getTimeSlotsAsync(int sittingID)
        {

            var sit = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingID);
            var duration = sit.Duration;
            var startTime = sit.StartDateTime.TimeOfDay;
            //duration = duration;

            var timeslot = (duration.Hours * 2) + (duration.Minutes / 30);

            //timeslot = timeslot;

            //var tables = await _context.Tables.Find()
            //var tableSittings = await _context.TableSittings.Where(s => s.SittingId == sittingID);
            //var tabSittings = sit.TableSittings.Where(s => s.SittingId == sittingID).Count();
            var tabSittings = _context.TableSittings.Where(s => s.SittingId == sittingID).Count();
            //tabSittings = tabSittings;

            //var cRes = _context.TableReserations.Where(r => r.Reservation.ReservationId == res.ReservationId).Include(r => r.TableSitting.Sitting.SittingId == sittingID).ToArray();
            //var curRes = _context.TableReserations
            //var curRes = _context.TableSittings.Where(r => r.Sitting.SittingId == sittingID);
            //var cuurRes = _context.TableReserations.Where(r => r.Reservation.ReservationId == );

            var reservationsList = await _context.TableReservations.Where(s => s.TableSitting.Sitting.Id == sittingID).Include(r => r.Reservation).Include(ts => ts.TableSitting).ToArrayAsync();


            //// the 5 stored ints are: time, guests, reservations, booked out (0 == not booked out, 1 == booked out). Tables Required
            int[][] timeSlots = new int[timeslot][];


            //loop through each of the timeslots, assigning the data required
            for (int i = 0; i < timeslot; i++)
            {
                timeSlots[i] = new int[5];
                //var dt = new DateTime();
                var nst = startTime;
                //var d = new DateTime();
                var hours = nst.Hours;
                var mins = nst.Minutes;

                var tMin = new TimeSpan(hours, mins + i * 30, 0);
                var tMax = new TimeSpan(hours, mins + (i * 30) + 30, 0);
                //var addTime = new TimeSpan(0, 30, 0);
                //tMax.Add(addTime);

                //tMin = tMin;
                //tMax = tMax;

                timeSlots[i][0] = (tMin.Hours * 100) + tMin.Minutes;


                //loops through and assigns data regarding the reservations
                for (int j = 0; j < reservationsList.Count(); j++)
                {

                    if (reservationsList[j].Reservation != null)
                    {
                        if (reservationsList[j].Reservation.StartDateTime.TimeOfDay <= tMin &&
                            reservationsList[j].Reservation.EndDateTime.TimeOfDay >= tMax)
                        {

                            timeSlots[i][1] += reservationsList[j].Reservation.Guests; // was + 1.
                            timeSlots[i][2] += 1;


                            if (reservationsList[j].Reservation.Guests <= 4)
                            {
                                timeSlots[i][4] += 1;

                            }
                            else if (reservationsList[j].Reservation.Guests % 4 == 0)
                            {
                                timeSlots[i][4] += reservationsList[j].Reservation.Guests / 4;
                            }
                            else
                            {
                                timeSlots[i][4] += ((int)(reservationsList[j].Reservation.Guests / 4)) + 1;
                            }

                        }
                    }

                    if (timeSlots[i][4] == tabSittings)
                    {
                        //timeSlots[i][3] = 1;
                    }


                }
            }
            //

            ///for (int i = 0; i < timeslot)

            //this is where you check the timeslots against the duration.
            //timeSlots = await getTimeSlotFromDuration(timeSlots, dur);
            //

            // returns the time slots that have been created.
            return timeSlots;

        }


        //this function calculates a timeslots availability by using the amount of guests and the duration function.
        private async Task<int[][]> getAvailableTimeSlots(int[][] times, DateTime dur)
        {

            int[][] timeSlots = times;
            int[][] newTimeSlots = new int[times.Length][];
            int timeSlotCounter = 0;

            for (int i = 0; i < timeSlots.Length; i++)
            {

                if (timeSlots[i][3] == 0)
                {
                    timeSlots[timeSlotCounter] = timeSlots[i];
                    timeSlotCounter++;
                }
            }

            //int[][] durTimes = await getTimeSlotFromDuration(newTimeSlots, dur);

            return newTimeSlots;
            //return durTimes;
        }


        //function to enforce restrictions to prevent overbooking, such as too many guests for the capacity, or not enough tables.
        private async Task<int[][]> OLDGetAvailableTimeSlotsAsync(int[][] times, int guests, int sittingID)
        {

            int[][] timeSlots = times;

            int totalTables = _context.TableSittings.Where(s => s.SittingId == sittingID).Count();
            int capacity = _context.Sittings.FirstOrDefault(s => s.Id == sittingID).Capacity;

            //var tablesRequired = (guests / 4) + (guests % 4 * 1);
            var tablesRequired = 0;

            if (guests <= 4)
            {
                tablesRequired = 1;
            }
            else if (guests % 4 == 0)
            {
                tablesRequired = guests / 4;
            }
            else
            {
                tablesRequired = (int)(guests / 4) + 1;
            }


            for (int i = 0; i < timeSlots.Length; i++)
            {
                if (capacity < timeSlots[i][1] + guests)
                {
                    timeSlots[i][3] = 1;
                }
                else
                {
                    if (totalTables < timeSlots[i][4] + tablesRequired)
                    {
                        timeSlots[i][3] = 1;
                    }
                }
            }

            return timeSlots;
        }

        //NEW TEST ONE:
        private async Task<int[][]> GetAvailableTimeSlotsAsync(int[][] times, int guests, int sittingID, DateTime dur)
        {

            int[][] timeSlots = times;

            int totalTables = _context.TableSittings.Where(s => s.SittingId == sittingID).Count();
            int capacity = _context.Sittings.FirstOrDefault(s => s.Id == sittingID).Capacity;

            var tableSittings = _context.TableSittings.Where(s => s.SittingId == sittingID).Include(s => s.Table);

            //var a = tables.First();
            
            //
            int[] unsortedTables = new int[tableSittings.Count()];
            for (int i = 0; i < unsortedTables.Count(); i++) {
                unsortedTables[i] = tableSittings.ToArray()[i].Table.TableCapacity;
            }
            //tables.OrderBy()
            int[] tables = unsortedTables;
            Array.Sort(tables);

            int[] remainingTables = tables;
            var tableRemoved = false;

            int[][] tsData = new int[timeSlots.Length][];
            
            var tabCounter = 0;



            // remove the already assigned tables from the list.

            //find the already assigned tables within the sitting
            //var tableReservations = _context.TableReservations.Where(tr => tr.TableSitting.SittingId == tableSittings.ToList().ElementAt(0).SittingId).ToList();
            var tableReservations = _context.TableReservations.Where(tr => tr.TableSitting.SittingId == sittingID).ToList();
            tableReservations.RemoveRange(0, tableReservations.Count());
            for (int i = 0; i < tableSittings.ToList().Count(); i++) {
                var databaseTRItem = _context.TableReservations.Where(tr => tr.TableSitting.SittingId == tableSittings.ToList().ElementAt(i).SittingId && tr.TableSitting.TableId == tableSittings.ToList().ElementAt(i).TableId);
                for (int j = 0; j < databaseTRItem.ToList().Count(); j++) {
                    tableReservations.Add(databaseTRItem.ToList().ElementAt(j));
                }

                //if (databaseTRItem != null)
                //{
                //    tableReservations.Add(_context.TableReservations.First(tr => tr.TableSitting.SittingId == tableSittings.ToList().ElementAt(i).SittingId && tr.TableSitting.TableId == tableSittings.ToList().ElementAt(i).TableId));
                //}

            }

            //backup code below, for the above code
            /*
             var tableReservations = _context.TableReservations.Where(tr => tr.TableSitting.SittingId == sittingID).ToList();
            tableReservations.RemoveRange(0, tableReservations.Count());
            for (int i = 0; i < tableSittings.ToList().Count(); i++) {
                var databaseTRItem = _context.TableReservations.FirstOrDefault(tr => tr.TableSitting.SittingId == tableSittings.ToList().ElementAt(i).SittingId && tr.TableSitting.TableId == tableSittings.ToList().ElementAt(i).TableId);
                if (databaseTRItem != null)
                {
                    tableReservations.Add(_context.TableReservations.First(tr => tr.TableSitting.SittingId == tableSittings.ToList().ElementAt(i).SittingId && tr.TableSitting.TableId == tableSittings.ToList().ElementAt(i).TableId));
                }

            }
             
             */



            //

            /*
            for (int i = 0; i < tableReservations.ToList().Count(); i++)
            {
                var rem = false;
                for (int j = 0; j < remainingTables.Length; j++)
                {


                    if (tableReservations.ToList().ElementAt(i).TableSitting.Table.TableCapacity == remainingTables[j] && rem == false)
                    {
                        int numToRemove = Array.IndexOf(remainingTables, remainingTables[j]);
                        remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();
                        rem = true;
                    }

                }
            }
            */
            //



            var allTables = tables;
            //int numToRemove = Array.IndexOf(remainingTables, tables[k + 1]);
            //remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();

            tables = remainingTables;

            //
            // loop through each of the time slots and filter
            for (int i = 0; i < timeSlots.Length; i++)
            {

                tables = allTables;
                remainingTables = allTables;
                // this is where the tables are checked within the timeSlot
                //if (tableReservations.ElementAt(0).Reservation.StartDateTime)
                TimeSpan time = new TimeSpan(timeSlots[i][0] / 100, (timeSlots[i][0]) - ((timeSlots[i][0] / 100) * 100), 0);

                for (int z = 0; z < tableReservations.Count(); z++) {
                    if (tableReservations.ToList().ElementAt(z).Reservation.StartDateTime.TimeOfDay >= time) {
                        if (tableReservations.ToList().ElementAt(z).Reservation.EndDateTime.TimeOfDay <= time + dur.TimeOfDay) {
                            //



                            var rem = false;
                            for (int y = 0; y < remainingTables.Length; y++)
                            {


                                if (tableReservations.ToList().ElementAt(z).TableSitting.Table.TableCapacity == remainingTables[y] && rem == false)
                                {
                                    int numToRemove = Array.IndexOf(remainingTables, remainingTables[y]);
                                    remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();
                                    rem = true;
                                }

                            }



                            //
                        }
                    }
                }


                tables = remainingTables;

                Array.Sort(tables);
                var tableDataGuests = false;

                // checks if the staff is required by the booking, this is done by checking if the amount of guests wanted is less than the 
                if (tables.Length != 0)
                {
                    if (tables[tables.Length - 1] < guests)
                    {
                        // sets a 'flag' that indicates that a staff member is required
                        timeSlots[i][3] = 2;
                        tableDataGuests = true;

                    }
                }

                if (tableDataGuests == false) {


                    for (int j = 0; j < tsData.Length; j++)
                    {





                        //
                        tsData[j] = new int[2];
                        tableRemoved = false;
                        for (int k = 0; k < tables.Length - 1; k++)
                        {
                            if (tableRemoved == false)
                            {
                                if (tsData[j][0] <= tables[k])
                                {
                                    //timeSlots[i][5][j] = new int[timeSlots.length+1];
                                    tsData[j][1 + tabCounter] = tables[k];
                                    //remainingTables.remove(tables[k + 1]);
                                    int numToRemove = Array.IndexOf(remainingTables, tables[k + 1]);
                                    remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();


                                    tableRemoved = true;
                                    //The first instance that has the size of tableSizes[k+1];

                                }
                                else if (tsData[j][0] > tables[k] && tsData[j][0] <= tables[k + 1])
                                {
                                    tsData[j][1] = tables[k + 1];
                                    //remainingTables.(tables[k + 1]);
                                    //int[] numbers = { 1, 3, 4, 9, 2, 4 };
                                    //int numToRemove = 4;
                                    //int numIndex = Array.IndexOf(numbers, numToRemove);
                                    //numbers = numbers.Where((val, idx) => idx != numIndex).ToArray();

                                    int numToRemove = Array.IndexOf(remainingTables, tables[k + 1]);
                                    remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();

                                    tableRemoved = true;
                                    //The first instance that has the size of tableSizes[k+1];
                                }
                            }
                            //timeSlots[i][5][j][1]
                        }

                        if (tableRemoved == false)
                        {
                            if (remainingTables.Length > 1) // was != 0
                            {
                                //this is where the max table is set
                                // we know that timeSlots[i][5][j][0] > the last table (MAX SIZE AVAILABLE).
                                tsData[j][0] -= remainingTables.Last();
                                //remainingTables.remove(remainingTables.last());
                                int numToRemove = Array.IndexOf(remainingTables, tables.Last());
                                remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();
                                j -= 1;
                            }
                        }
                        else
                        {
                            tabCounter = 0;
                        }

                    }



                    if (remainingTables != null && remainingTables.Length > 0)
                    {

                        //calculate the tables required using the guests
                        var amountTablesNeeded = 0;
                        var tableFound = false;
                        var remTables = remainingTables;
                        Array.Sort(remTables);
                        var peopleNeeded = guests;
                        var noTablesAvailable = false;
                        while (tableFound == false)
                        {
                            if (remTables.Length != 0)
                            {

                                for (int l = 0; l < remTables.Length - 1; l++)
                                {
                                    if (peopleNeeded <= remTables[l])
                                    {
                                        amountTablesNeeded += 1;
                                        tableFound = true;
                                    }
                                    else if (peopleNeeded > remTables[l] && peopleNeeded <= remTables[l + 1])
                                    {
                                        amountTablesNeeded += 1;
                                        tableFound = true;
                                    }
                                }
                                if (remTables.Length == 1)
                                {
                                    if (peopleNeeded <= remTables[0])
                                    {
                                        amountTablesNeeded += 1;
                                        tableFound = true;
                                    }
                                    else
                                    {
                                        tableFound = false;
                                    }
                                }
                                if (tableFound == false)
                                {
                                    peopleNeeded -= remainingTables.Last();
                                    int numToRemove = Array.IndexOf(remainingTables, remainingTables.Last());
                                    remainingTables = remainingTables.Where((val, idx) => idx != numToRemove).ToArray();

                                }
                            }
                            else {

                                noTablesAvailable = true;
                                tableFound = true;
                            }

                        }

                        if (noTablesAvailable == false)
                        {
                            if (capacity < timeSlots[i][1] + guests)
                            {
                                //timeSlots[i][3] = 1;
                                timeSlots[i][3] = 1;
                            }
                            else
                            {
                                //if (totalTables < timeSlots[i][4] + tablesRequired)
                                //{
                                //    timeSlots[i][3] = 1;
                                //}
                                if (remainingTables.Length < amountTablesNeeded) {
                                    timeSlots[i][3] = 1;
                                }

                            }
                        }
                        else {
                            timeSlots[i][3] = 1;
                        }
                    }
                    else {
                        timeSlots[i][3] = 1;
                    }

                }

            }



            // returns the newly filtered timeslots
            return timeSlots;
            


            //var tablesRequired = (guests / 4) + (guests % 4 * 1);



            /*
            for (int i = 0; i < timeSlots.Length; i++)
            {
                if (capacity < timeSlots[i][1] + guests)
                {
                    timeSlots[i][3] = 1;
                }
                else
                {
                    if (totalTables < timeSlots[i][4] + tablesRequired)
                    {
                        timeSlots[i][3] = 1;
                    }
                }
            }
            */

            //return timeSlots;
        }



        public ReservationController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.Include(r => r.Customer).ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _context.Reservations.Where(r => r.Id == id).Include(c => c.Customer).FirstOrDefault();
            //var reservation = await _context.Reservations
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            
            var reservationStatuses = _context.ReservationStatuses.ToList();
            ResStatusModel resStatusModel = new ResStatusModel();
            resStatusModel.reservation = reservation;
            resStatusModel.reservationStatusList = reservationStatuses;
            var reservationTypes = _context.ReservationTypes.ToList();
            resStatusModel.reservationTypeList = reservationTypes;

            return View(resStatusModel);
        }


        public IActionResult Guests()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guests([Bind("ReservationId,Guests,StartDateTime,EndDateTime")] Reservation reservation)
        {

            
            res.Id = reservation.Id;
            res.Guests = reservation.Guests;
            res.StartDateTime = reservation.StartDateTime;
            res.EndDateTime = reservation.EndDateTime;

            //.Include(r => r.Customer)
            //    .Where(r => r.Id == id)
            //var sittings = _context.Sittings.ToListAsync();


            // collect a list of all sittings for the day
            var sittings = _context.Sittings.Where(s => s.StartDateTime.Date == res.StartDateTime.Date).Include(s => s.SittingType);
            //var reservationsList = await _context.TableReserations.Where(s => s.TableSitting.Sitting.Id == sittingID).Include(r => r.Reservation).Include(ts => ts.TableSitting).ToArrayAsync();


            //sits = await sittings.ToListAsync();

            // create an array of all the sittings
            var sittingArray = await sittings.ToArrayAsync();
            //var reservation = await _db.Reservations
            //   .Include(r => r.Sitting)
            //      .ThenInclude(s => s.Restaurant)
            // .Include(r => r.Sitting)
            //int[][] a = getTimeSlots(sittings.First().SittingId);


            List<Sitting> sittingList = new List<Sitting>();
            
            //loop through the array of sittings to filter through them
            for (int i = 0; i < sittingArray.Count(); i++)
            {

                // create the time slots for the sitting
                int[][] sa = await getTimeSlotsAsync(sittingArray[i].Id);

                // filter through the time slots by duration
                int[][] sd = await getTimeSlotFromDuration(sa, res.EndDateTime);

                // filter through the time slots through other restrictions (guest size, table availability).
                int[][] restrictedSittings = await GetAvailableTimeSlotsAsync(sd, res.Guests, sittingArray[i].Id, res.EndDateTime);


                // loop through the array of time slots
                for (int j = 0; j < sa.Length; j++)
                {
                    if (sa[j][3] == 0 || sa[j][3] == 2)
                    { // meaning there is a spot available
                        //sits.Add(sittingArray[i]);

                        if (sd.Length != 0)
                        {
                            for (int k = 0; k < sd.Length; k++)
                            {
                                if (sd[k][3] == 0 || sd[k][3] == 2)
                                {
                                    //sittingList.Add(sittingArray[i]);
                                    //k = sd.Length + 1;
                                    //j = sa.Length + 1;

                                    //above is without enforcing restricted sittings.


                                    // add the available timeslots to the array
                                    if (restrictedSittings[k][3] == 0)
                                    {
                                        sittingList.Add(sittingArray[i]);
                                        k = sd.Length + 1;
                                        j = sa.Length + 1;
                                    }
                                    else if (restrictedSittings[k][3] == 2) {
                                        sittingList.Add(sittingArray[i]);
                                        k = sd.Length + 1;
                                        j = sa.Length + 1;
                                    }


                                }
                            }

                        }

                    }
                }

            }


            // create a new sittingListTSDataModel to hold the required data and pass it to the view.
            SittingListTSDataModel sittingListTSDataModel = new SittingListTSDataModel();
            sittingListTSDataModel.SittingList = sittingList;

            List<int[][]> timeSlotList = new List<int[][]>();
            List<bool> staffNeededForSittingList = new List<bool>();

            // loop through the sittingList
            for (int i = 0; i < sittingListTSDataModel.SittingList.Count(); i++)
            {
                //timeSlotList.Add(await getTimeSlotsAsync(sittingArray[i].Id, res.EndDateTime));
                //timeSlotList.Add(await getTimeSlotsAsync(sittingArray[i].Id));

                //

                //int[][] times = await getTimeSlotsAsync(sittingArray[i].Id);
                //timeSlotList.Add(await getAvailableTimeSlots(times, res.EndDateTime));

                //int[][] times = await getTimeSlotsAsync(sittingArray[i].Id);

                // create array of timeSlots per sitting.
                int[][] times = await getTimeSlotsAsync(sittingListTSDataModel.SittingList[i].Id);
                // filter by duration
                int[][] timeDur = await getTimeSlotFromDuration(times, res.EndDateTime);
                // filter by restrictions.
                int[][] timeRes = await GetAvailableTimeSlotsAsync(timeDur, res.Guests, sittingListTSDataModel.SittingList[i].Id, res.EndDateTime);
                //timeSlotList.Add(timeDur);


                //check if staff is required for booking.
                var staffNeeded = false;
                for (int j = 0; j < timeRes.Length; j++) {
                    if (timeRes[j][3] == 2 && staffNeeded == false) {

                        staffNeededForSittingList.Add(true);
                        staffNeeded = true;
                    }
                }

                if (staffNeeded == false) {
                    staffNeededForSittingList.Add(false);
                }

                timeSlotList.Add(timeRes);


            }

            //for (int i = 0; i < timeSlotList.Count(); i++) {
            //    for (int j = 0; j < timeSlotList.ToArray()[i].Length; j++) {

            //    }
            //}

            

            // assign all of the required data to the SittingListTSDataModel
            sittingListTSDataModel.TimeSlotList = timeSlotList;
            //var a = sittingListTSDataModel.sittingList.ToArray()[0].SittingType.Description;

            sittingListTSDataModel.Reservation = reservation;

            //sittingListTSDataModel.Duration = res.EndDateTime;
            sittingListTSDataModel.Reservation.EndDateTime = res.EndDateTime;
            sittingListTSDataModel.EndDateTime = res.EndDateTime;

            sittingListTSDataModel.Reservation.StartDateTime = res.StartDateTime;
            sittingListTSDataModel.StartDateTime = res.StartDateTime;

            

           
            var areas = _context.Areas;

            for (int i = 0; i < areas.Count(); i++) {
                sittingListTSDataModel.AreasList.Add(areas.ToArray().ElementAt(i));
            }
                

            List<bool> sl = new List<bool>();

            if (User.IsInRole("Manager") || User.IsInRole("Staff"))
            {

                for (int i = 0; i < staffNeededForSittingList.Count(); i++)
                {
                    sl.Add(false);
                }
            }
            else {
                sl = staffNeededForSittingList;
            }
            
            
            sittingListTSDataModel.StaffNeededForSittingList = sl;


           
            return AvailableSittings(sittingListTSDataModel, 0);
            //return View(sittingListTSDataModel);
            //return View("AvailableSittings", sittingListTSDataModel);
            //return await AvailableSittings();
            //return View("AvailableSittings", await _context.Sittings.ToListAsync()); // should fix the error i was having
            //return View("AvailableSittings", sittingList); // should fix the error i was having
        }



        public IActionResult AvailableSittings(SittingListTSDataModel st, int a)
        {

            var s = st;

            //s = s;
            //s = s;
            //s = s;

            //return View(st);
            return View("AvailableSittings", s);
            //return Content()
            //RedirectToAction("AvailableSittings",s);
            //return View("AvailableSittings");
        }

        //

            //not used i dont think, here as backupcode just in case
        //Had: [Bind("ReservationId,Guests,StartDateTime,EndDateTime")] Reservation reservation
        public async Task<IActionResult> AvailableSittings()
        {
            //ViewBag["sittingStartDate"] = res.StartDateTime.Date;
            //var sittings = _context.Sittings.ToList();
            //var sittings = _context.Sittings.Where(s => s.StartDateTime.Date == res.StartDateTime.Date);
            //return View(await _context.Sittings.ToListAsync());
            //var sittings = _context.Sittings.Where(s => s.StartDateTime.Date == res.StartDateTime.Date);
            //int[][] a = getTimeSlots(sittings.First().SittingId);

            var sittings = _context.Sittings.Where(s => s.StartDateTime.Date == res.StartDateTime.Date);
            //sits = await sittings.ToListAsync();

            var sittingArray = await sittings.ToArrayAsync();
            //var reservation = await _db.Reservations
            //   .Include(r => r.Sitting)
            //      .ThenInclude(s => s.Restaurant)
            // .Include(r => r.Sitting)
            //int[][] a = getTimeSlots(sittings.First().SittingId);

            List<Sitting> sittingList = new List<Sitting>();
            for (int i = 0; i < sittingArray.Count(); i++)
            {

                int[][] sa = await getTimeSlotsAsync(sittingArray[i].Id);

                for (int j = 0; j < sa.Length; j++)
                {
                    if (sa[j][3] == 0)
                    { // meaning there is a spot available
                        //sits.Add(sittingArray[i]);
                        sittingList.Add(sittingArray[i]);
                        j = sa.Length + 1;
                    }
                }

            }


            SittingListTSDataModel sittingListTSDataModel = new SittingListTSDataModel();
            sittingListTSDataModel.SittingList = sittingList;

            List<int[][]> timeSlotList = new List<int[][]>();

            for (int i = 0; i < sittingListTSDataModel.SittingList.Count(); i++)
            {
                //timeSlotList.Add(await getTimeSlotsAsync(sittingArray[i].Id, res.EndDateTime));
                //timeSlotList.Add(await getTimeSlotsAsync(sittingArray[i].Id, res.EndDateTime));

                //int[][] times = await getTimeSlotsAsync(sittingArray[i].Id);
                //timeSlotList.Add(await getTimeSlotFromDuration(times, res.EndDateTime));

                int[][] times = await getTimeSlotsAsync(sittingArray[i].Id);
                int[][] timeDur = await getTimeSlotFromDuration(times, res.EndDateTime);

                timeSlotList.Add(timeDur);

            }

            sittingListTSDataModel.TimeSlotList = timeSlotList;



            return View("AvailableSittings", sittingListTSDataModel);
            //return View("AvailableSittings", sittingList);            
        }



        //[Bind("ReservationId,Guests,StartDateTime,EndDateTime")] Reservation reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AvailableSittings(SittingListTSDataModel st)
        {

            

            var data = st;

            if (User.IsInRole("Member")) {
                var person = _context.People.FirstOrDefault(p => p.Email == User.Identity.Name);
                
                data.FirstName = person.FirstName;
                data.LastName = person.LastName;
            }
            //TempData["e"] = TempData["Details"];

            //data.startDateTime = 
            //data.endDateTime = data.

            var timeHours = (int)(data.StartTimeSlot / 100);
            var timeMins = data.StartTimeSlot - timeHours*100;
            //data.endDateTime.Hour = timeHours;
            DateTime dateTime = new DateTime(data.StartDateTime.Year, data.StartDateTime.Month, data.StartDateTime.Day, timeHours, timeMins, 0);

            data.StartDateTime = dateTime;

            var dateEndTimeDates = dateTime;

            data.EndDateTime = dateEndTimeDates.AddHours(data.EndDateTime.Hour).AddMinutes(data.EndDateTime.Minute);
            
            //var dateEndTime = data.endDateTime;
            //data.endDateTime = dateEndTime.AddHours(dateTime.Hour).AddMinutes(dateTime.Minute);
            
            
            //data.endDateTime = dateEndTime.AddMinutes(dateTime.Minute);

            data.Date = data.StartDateTime;
            data.Duration = data.EndDateTime;

            // sends the user to the member creation page for booking a reservation.
            if (User.IsInRole("Member"))
            {
                return View("MemberCreate", data);
            } // sends the user to the staff/manager/admin page for booking a reservation.
            else if (User.IsInRole("Staff") || User.IsInRole("Manager") || User.IsInRole("Admin")) {
                var reservationTypes = _context.ReservationTypes.ToList();
                data.ReservationTypeList = reservationTypes;

                //

                //get all tables from db 
                //and create list of tables that will be displayed on the page

                //data.Tables = _context.Tables.Select(t => new Models.SittingListTSDataModel.TableSitting

                var sitting = _context.Sittings.FirstOrDefault(s => s.StartDateTime <= data.StartDateTime && s.EndDateTime >= data.EndDateTime);


                var availableTables = _context.TableSittings.Where(ts => ts.Sitting.Id == sitting.Id);
                //availableTables = null;

                //weird bug was occuring where sometimes where availableTables would be null, so when this occurs
                // check if its null and set the tables that the staff member will see to null, they can still edit the
                // tables within the reservations edit sections.
                if (availableTables != null)
                {

                    data.Tables = availableTables.Select(t => new Models.TableSitting
                    {
                        Description = $"{t.Table.TableNo} ({t.Table.TableCapacity})",
                        Capacity = t.Table.TableCapacity,
                        AreaId = t.Table.AreaId,
                        TableNo = t.Table.TableNo,
                        TableId = t.Table.Id,
                        Selected = false

                    }).ToList();

                    var newTables = availableTables.Select(t => new Models.TableSitting
                    {
                        Description = $"{t.Table.TableNo} ({t.Table.TableCapacity})",
                        Capacity = t.Table.TableCapacity,
                        AreaId = t.Table.AreaId,
                        TableNo = t.Table.TableNo,
                        TableId = t.Table.Id,
                        Selected = false

                    }).ToList();



                    var allReservations = _context.Reservations.Where(t => t.StartDateTime <= data.StartDateTime && t.EndDateTime >= data.EndDateTime).Include(t => t.TableReservations).ThenInclude(t => t.TableSitting).ThenInclude(t => t.Table).ToList();

                    var tCounter = 0;
                    //var newTables = data.Tables;
                    var removedAmount = 0;
                    //checks if there is another reservation that is using the table.
                    // removed the table if another table is using it.
                    var resCounterT = 0;
                    foreach (var t in data.Tables)
                    {

                        //if (availableTables.ElementAt(tCounter).)
                        for (int i = 0; i < allReservations.Count(); i++)
                        {

                            if (allReservations.ToArray().ElementAt(i).IsAssignedToTable == true)
                            {
                                var ntRemoved = false;
                                foreach (var rt in allReservations.ToArray().ElementAt(i).TableReservations)
                                {
                                    if (ntRemoved == false)
                                    {
                                        //checks if the table id being used matches the one in the list
                                        if (rt.TableSitting.Table.Id == data.Tables.ElementAt(tCounter).TableId)
                                        {
                                            //newTables.RemoveAt(tCounter - removedAmount);
                                            //newTables.RemoveAt(resCounterT - removedAmount);
                                            //newTables.RemoveAt(resCounterT - removedAmount);
                                            //newTables.Remove(t => t.TableId == rt.TableSitting.Table.Id);
                                            newTables.RemoveAt(newTables.FindIndex(t => t.TableId == data.Tables.ElementAt(tCounter).TableId));
                                            //newTables.Remove(data.Tables.ElementAt(tCounter));
                                            removedAmount += 1;
                                            ntRemoved = true;
                                        }
                                    }
                                }
                            }
                            resCounterT++;
                        }

                        tCounter++;
                    }


                    data.Tables = newTables;
                }
                else {
                    data.Tables = new List<Models.TableSitting>();
                }



                /*data.Tables = _context.Tables.Select(t => new Models.TableSitting
                {
                    Description = $"{t.TableNo} ({t.TableCapacity})",
                    Capacity = t.TableCapacity,
                    AreaId = t.AreaId,
                    TableNo = t.TableNo,
                    TableId = t.Id,
                    Selected = false
                }).ToList();
                */



                //data.Tables.Sort();

                //Array.Sort(data.Tables.ToArray());
                data.Tables = data.Tables.OrderBy(t => t.TableNo).ToList();
                //get all areas from db
                //and use them to create dropdown fields
                //data.Areas = _context.Areas.Select(a => new Models.SittingListTSDataModel.AreaModel
                data.Areas = _context.Areas.Select(a => new Models.AreaModel
                {
                    Description = a.Description,
                    AreaId = a.Id
                }).ToList();
                
                //data.Sitting = new Sitting();

                //

                return View("StaffCreate", data);
            } // sends the user to the generic booking page.
            else
            {
                return View("Create", data);
            }
        }


        public async Task<IActionResult> SelectTimeSlot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var reservation = await _context.Reservations
            //   .FirstOrDefaultAsync(m => m.ReservationId == id);
            //if (reservation == null)
            //{
            //    return NotFound();
            //}
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }


            var ts = await getTimeSlotsAsync(sitting.Id);

            //for (int i = 0; i < ts.Length; i++) { 

            //}

            //ViewData["timeSlots"] = ts;
            //ViewBag["timeSlots"] = ts;

            Models.SittingTSDataModel sittingTSDataModel = new Models.SittingTSDataModel();
            sittingTSDataModel.sitting = sitting;
            sittingTSDataModel.timeSlots = ts;

            return View(sittingTSDataModel);

            //return View(sitting.timeSlot = ts;
            //return View(sittingTSDataModel);
            //Object sit// = new Object(sitting,ts);
            //dynamic sitTs = new { sitting, ts };

            ///dynamic sitTs = new ExpandoObject();
            ///sitTs.Sitting = sitting;
            ///sitTs.timeSlots = ts;

            //return View(sitTs);
        }


        //Staff Create
        public IActionResult StaffCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StaffCreate(SittingListTSDataModel sittingListTSDataModel)
        {

            // gets the person from the database if present.
            var person = _context.People.FirstOrDefault(p => p.Email.ToLower() == sittingListTSDataModel.Email.ToLower());


            
            
            SittingListTSDataModel sltsdm = sittingListTSDataModel;
            // assigns the required details
            Reservation reservation = new Reservation();
            reservation.StartDateTime = sittingListTSDataModel.Date;
            reservation.EndDateTime = sittingListTSDataModel.Duration;
            reservation.Guests = sittingListTSDataModel.Guests;
            reservation.Notes_Requirements = "AREA: " + sittingListTSDataModel.AreaPicked.ToString() + ".\n";
            reservation.Notes_Requirements += sittingListTSDataModel.Notes_Requirements;

            //if (User.IsInRole("Member"))
            //{
                var u = User.Identity.Name;
                //u = u;
                //var c = _context.People.FirstOrDefault(c => c.Id == "2".ToString());
                //c = c;
                //var c = _context.People.ToList();
                //c = c;
                var cust = _context.People.FirstOrDefault(c => c.Email.ToLower() == sittingListTSDataModel.Email.ToLower());
                var status = _context.ReservationStatuses.ToList();
                var types = _context.ReservationTypes.ToList();
            //var a = _context.People.ToList();
            
            //checks if a customer exists for it
            if (cust != null)
            {
                reservation.Customer = new Customer
                {
                    //Id = cust.Id,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Phone = cust.Phone
                };



                reservation.Customer = (Customer)cust;
                //reservation.UserId = int.Parse(cust.Id);
                
            }
            else {
                // if the customer doesnt exist create a new one and assign the reservation to the new customer
                //var count = _context.People.Count();
                var last = new Person();
                if (_context.People.ToList() == null)
                {
                    last = new Person { Id = "0"};
                }
                else {
                    last = _context.People.ToList().Last();
                }

                
                //last = last;
                //last = last;
                //last = last;

                var lastId = last.Id.ToString();
                //var intLastId = int.Parse(lastId.ToString());

                int returnInt;
                bool intResultTryParse = int.TryParse(lastId.ToString(), out returnInt);
                var intLastId = 0;
                if (intResultTryParse == true)
                {
                    intLastId = returnInt;
                }
                else {
                    intLastId = 5;
                }


                var peopleList = _context.People.Where(p => p.Id == (intLastId + 1).ToString());

                // create the new id for the customer.
                while (peopleList.ToList().Count() != 0) {
                    intLastId++;
                    peopleList = _context.People.Where(p => p.Id == intLastId.ToString());
                }


                //var intLastId = int.Parse(lastId.ToString());
                //intLastId += 1;
                var customer = new Customer {
                    Id = intLastId.ToString(),
                    FirstName = sittingListTSDataModel.FirstName,
                    LastName = sittingListTSDataModel.LastName,
                    Email = sittingListTSDataModel.Email,
                    Phone = sittingListTSDataModel.Phone
                };

                //Person p1 = _context.People;
                
                //var lastId = int.Parse(p1.Id);
                //lastId += 1;
                //customer.Id = Guid.NewGuid().ToString();
                

                // add the customer to the context
                _context.Add(customer);
                //save the changes
                 _context.SaveChanges();

                var c = _context.Customers.FirstOrDefault(c => c.Email.ToLower() == customer.Email.ToLower());
                //c.CustomerId = c.Id;
                // set the customersId to the id of the person
                c.CustomerId = int.Parse(c.Id);
                //update the changes
                _context.Update(c);
                //save the context.
                await _context.SaveChangesAsync();

                reservation.Customer = (Customer)c;
                //reservation.UserId = int.Parse(c.Id);

            }

                
            // assign details regarding reservation such as the type and status

            //reservation.ReservationType = types.Find(t => t.Id == 1);
            reservation.ReservationType = types.Find(t => t.Id == sittingListTSDataModel.ReservationTypePickedId);

            if (sittingListTSDataModel.Tables.Where(t => t.Selected == true).ToList().Count() > 0)
            {
                reservation.ReservationStatus = status.Find(s => s.Id == 2);
            }
            else {
                reservation.ReservationStatus = status.Find(s => s.Id == 1);
            }
            // sets the end time
            reservation.EndDateTime = reservation.EndDateTime;

            /* reservation.User = new User { 
                 Id = int.Parse(cust.Id)
             };
             */
            //}

            var lastRes = _context.Reservations.ToList().Last();

            //reservation.Id = lastRes.Id + 1;

            var sitting = _context.Sittings.FirstOrDefault(s => s.StartDateTime <= reservation.StartDateTime && s.EndDateTime >= reservation.EndDateTime);


            if (ModelState.IsValid)
            {

                //
                //saves all the details to the reservation
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                //
                
                var res = _context.Reservations.FirstOrDefault(r => r.Guests == reservation.Guests && r.StartDateTime == reservation.StartDateTime &&
                    r.EndDateTime == reservation.EndDateTime && r.Notes_Requirements == reservation.Notes_Requirements && r.ReservationStatus == reservation.ReservationStatus &&
                    r.ReservationType == reservation.ReservationType);
                //var res = _context.Reservations.Last();
                
                for (int i = 0; i < sittingListTSDataModel.Tables.Count(); i++) {
                    if (sittingListTSDataModel.Tables.ElementAt(i).Selected == true) {
                        var tableSitting = _context.TableSittings.FirstOrDefault(ts => ts.SittingId == sitting.Id && ts.TableId == sittingListTSDataModel.Tables.ElementAt(i).TableId);
                        var tr = new TableReservation()
                        {
                            ReservationId = res.Id,
                            Reservation = reservation,
                            TableSitting = tableSitting,
                            TableSittingId = tableSitting.Id
                        };

                        _context.Add(tr);
                        
                    }
                }
                // save the changes
                await _context.SaveChangesAsync();



                //
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }


        // Member Create Reservation: (used when a member is creating a reservation)
        public IActionResult MemberCreate()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MemberCreate(SittingListTSDataModel sittingListTSDataModel)
        {
            // this is all used when a member is creating a reservation, follows the same procedure as the staff creation.

            SittingListTSDataModel sltsdm = sittingListTSDataModel;

            Reservation reservation = new Reservation();
            reservation.StartDateTime = sittingListTSDataModel.Date;
            reservation.EndDateTime = sittingListTSDataModel.Duration;
            reservation.Guests = sittingListTSDataModel.Guests;
            reservation.Notes_Requirements = "AREA: " + sittingListTSDataModel.AreaPicked.ToString() +".\n";
            reservation.Notes_Requirements += sittingListTSDataModel.Notes_Requirements;

            if (User.IsInRole("Member"))
            {
                var u = User.Identity.Name;
                //u = u;
                //var c = _context.People.FirstOrDefault(c => c.Id == "2".ToString());
                //c = c;
                //var c = _context.People.ToList();
                //c = c;
                var cust = _context.People.FirstOrDefault(c => c.Email.Equals(u));
                var status = _context.ReservationStatuses.ToList();
                var types = _context.ReservationTypes.ToList();
                //var a = _context.People.ToList();


                reservation.Customer = new Customer
                {
                    //Id = cust.Id,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Phone = cust.Phone
                };

                reservation.Customer = (Customer)cust;
                
                reservation.ReservationStatus = status.Find(s => s.Id == 1);
                reservation.ReservationType = types.Find(t => t.Id == 1);
                //cust = null;

                reservation.EndDateTime = reservation.EndDateTime;
                //reservation.UserId = int.Parse(cust.Id);
                //reservation.UserId = _context.U
                /* reservation.User = new User { 
                     Id = int.Parse(cust.Id)
                 };
                 */
            }



            if (ModelState.IsValid)
            {

                //

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }







        // GET: Reservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OldCreate([Bind("Id,StartDateTime,EndDateTime,Guests,Notes_Requirements,CustomerId")] Reservation reservation)
        {


            if (User.IsInRole("Member"))
            {
                var u = User.Identity.Name;
                //u = u;
                //var c = _context.People.FirstOrDefault(c => c.Id == "2".ToString());
                //c = c;
                //var c = _context.People.ToList();
                //c = c;

                // get all the statuses and types as well as the customer
                var cust = _context.People.FirstOrDefault(c => c.Email.Equals(u));
                var status = _context.ReservationStatuses.ToList();
                var types = _context.ReservationTypes.ToList();
                //var a = _context.People.ToList();

                // set the reservations customer to teh newly created customer
                reservation.Customer = new Customer
                {
                    //Id = cust.Id,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Phone = cust.Phone
                };


                // set the customer and reservation types and statuses
                reservation.Customer = (Customer)cust;
                reservation.ReservationStatus = status.Find(s => s.Id == 1);
                reservation.ReservationType = types.Find(t => t.Id == 1);
                //cust = null;

                reservation.EndDateTime = reservation.EndDateTime;
                reservation.UserId = int.Parse(cust.Id);
                /* reservation.User = new User { 
                     Id = int.Parse(cust.Id)
                 };
                 */
            }



            if (ModelState.IsValid)
            {

                //

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SittingListTSDataModel sittingListTSDataModel)
        {

            // this essentailly follows the same ideas as the StaffCreate, with slight changes

            // assign the details required
            SittingListTSDataModel sltsdm = sittingListTSDataModel;

            Reservation reservation = new Reservation();
            reservation.StartDateTime = sittingListTSDataModel.Date;
            reservation.EndDateTime = sittingListTSDataModel.Duration;
            reservation.Guests = sittingListTSDataModel.Guests;
            reservation.Notes_Requirements = "AREA: " + sittingListTSDataModel.AreaPicked.ToString() + ".\n";
            reservation.Notes_Requirements += sittingListTSDataModel.Notes_Requirements;

            if (User.IsInRole("Member"))
            {
                var u = User.Identity.Name;
                //u = u;
                //var c = _context.People.FirstOrDefault(c => c.Id == "2".ToString());
                //c = c;
                //var c = _context.People.ToList();
                //c = c;
                var cust = _context.People.FirstOrDefault(c => c.Email.Equals(u));
                var status = _context.ReservationStatuses.ToList();
                var types = _context.ReservationTypes.ToList();
                //var a = _context.People.ToList();

                // create a new customer and assign it to the reservation
                reservation.Customer = new Customer
                {
                    //Id = cust.Id,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Phone = cust.Phone
                };



                reservation.Customer = (Customer)cust;
                reservation.ReservationStatus = status.Find(s => s.Id == 1);
                reservation.ReservationType = types.Find(t => t.Id == 1);
                //cust = null;

                reservation.EndDateTime = reservation.EndDateTime;
                reservation.UserId = int.Parse(cust.Id);
                /* reservation.User = new User { 
                     Id = int.Parse(cust.Id)
                 };
                 */
            }



            if (ModelState.IsValid)
            {

                //

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }




        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var sittings = _context.Sittings.Where(s => s.StartDateTime.Date == res.StartDateTime.Date).Include(s => s.SittingType);
            var reservation = _context.Reservations.Where(r => r.Id == id).Include(c => c.Customer).Include(r => r.TableReservations).FirstOrDefault();
            //var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationStatuses = _context.ReservationStatuses.ToList();
            ResStatusModel resStatusModel = new ResStatusModel();
            resStatusModel.reservation = reservation;
            resStatusModel.reservationStatusList = reservationStatuses;

            // tables assignment stuff below

            if (reservation.IsAssignedToTable == true) {
                
            }
            // gets the sitting
            var sitting = _context.Sittings.FirstOrDefault(s => s.StartDateTime <= resStatusModel.reservation.StartDateTime && s.EndDateTime >= resStatusModel.reservation.EndDateTime);

            // gets teh available tables
            var availableTables = _context.TableSittings.Where(ts => ts.Sitting.Id == sitting.Id).Include(ts => ts.Table);

            resStatusModel.Tables = availableTables.Select(t => new Models.ReservationsManagement.TableSitting
            {
                Description = $"{t.Table.TableNo} ({t.Table.TableCapacity})",
                Capacity = t.Table.TableCapacity,
                AreaId = t.Table.AreaId,
                TableNo = t.Table.TableNo,
                TableId = t.Table.Id,
                Selected = false

            }).ToList();

            var newTables = availableTables.Select(t => new Models.ReservationsManagement.TableSitting
            {
                Description = $"{t.Table.TableNo} ({t.Table.TableCapacity})",
                Capacity = t.Table.TableCapacity,
                AreaId = t.Table.AreaId,
                TableNo = t.Table.TableNo,
                TableId = t.Table.Id,
                Selected = false

            }).ToList();



            var allReservations = _context.Reservations.Where(t => t.StartDateTime <= resStatusModel.reservation.StartDateTime &&
            t.EndDateTime >= resStatusModel.reservation.EndDateTime).Include(t => t.TableReservations)
            .ThenInclude(t => t.TableSitting).ThenInclude(t => t.Table).ToList();

            var tCounter = 0;
            //var newTables = data.Tables;
            var removedAmount = 0;
            //checks if there is another reservation that is using the table.
            // removed the table if another table is using it.
            foreach (var t in resStatusModel.Tables)
            {

                //if (availableTables.ElementAt(tCounter).)
                for (int i = 0; i < allReservations.Count(); i++)
                {
                    if (allReservations.ToArray().ElementAt(i).IsAssignedToTable == true)
                    {
                        var ntRemoved = false;
                        foreach (var rt in allReservations.ToArray().ElementAt(i).TableReservations)
                        {
                            if (ntRemoved == false)
                            {
                                //checks if the table id being used matches the one in the list
                                if (rt.TableSitting.Table.Id == resStatusModel.Tables.ElementAt(tCounter).TableId)
                                {
                                    if (rt.ReservationId != resStatusModel.reservation.Id)
                                    {
                                        newTables.RemoveAt(tCounter - removedAmount);
                                        removedAmount += 1;
                                        ntRemoved = true;
                                    }
                                    else {
                                        newTables.ElementAt(tCounter-removedAmount).Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }

                tCounter++;
            }
            // sets the tables to the model
            resStatusModel.Tables = newTables;




            //get all areas from db
            //and use them to create dropdown fields
            //data.Areas = _context.Areas.Select(a => new Models.SittingListTSDataModel.AreaModel
            resStatusModel.Areas = _context.Areas.Select(a => new Models.ReservationsManagement.AreaModel
            {
                Description = a.Description,
                AreaId = a.Id
            }).ToList();

            
            var tableReservations = _context.TableReservations.Where(tr => tr.ReservationId == resStatusModel.reservation.Id).Include(tr => tr.TableSitting).Include(tr => tr.Reservation);
            resStatusModel.reservation.TableReservations = tableReservations.ToList();

            // tables assignment stuff above

            return View(resStatusModel);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResStatusModel resStatusModel)
        {
            if (id != resStatusModel.reservation.Id)
            {
                return NotFound();
            }

            //

            //if (resStatusModel.reservation.TableReservations.Count() == resStatusModel.Tables.Count())
            var sitting = _context.Sittings.FirstOrDefault(s => s.StartDateTime <= resStatusModel.reservation.StartDateTime && s.EndDateTime >= resStatusModel.reservation.EndDateTime);

            var count = 0;
            
            for (int i = 0; i < resStatusModel.Tables.Count(); i++) {
                if (resStatusModel.Tables.ElementAt(i).Selected == true) {
                    count++;
                }

                var table = _context.Tables.First(t => t.Id == resStatusModel.Tables.ElementAt(i).TableId);
                resStatusModel.Tables.ElementAt(i).Table = table;

            }

            //var tableReservations = _context.TableReservations.Where(tr => tr.ReservationId == resStatusModel.reservation.Id).Include(tr => tr.TableSitting).Include(tr => tr.Reservation);
            //resStatusModel.reservation.TableReservations = tableReservations.ToList();
            //resStatusModel.Tables.ToList().Where(t => t.Selected == true).ToList()
            var tableSittings = _context.TableSittings.Where(ts => ts.SittingId == sitting.Id).AsNoTracking();


            var tableReservations = _context.TableReservations.Where(tr => tr.ReservationId == resStatusModel.reservation.Id).AsNoTracking().Include(tr => tr.TableSitting).AsNoTracking().Include(tr => tr.Reservation).AsNoTracking();
            resStatusModel.reservation.TableReservations = tableReservations.ToList();

            
            var deleteDuplicates = false;
            var deletedCounter = 0;
            // edits the pre existing table reservations to suit the new table assignment (doesnt require any new tableReservations to be added or removed).
            if (resStatusModel.reservation.TableReservations.Count() == count)
            {
                var selectedTables = resStatusModel.Tables.Where(t => t.Selected == true);
                for (int i = 0; i < resStatusModel.reservation.TableReservations.Count(); i++) {
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting.TableId = resStatusModel.Tables.ToList().Where(t => t.Selected == true).ElementAt(i).TableId;
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting = resStatusModel.Tables.ToList().Where(t => t.Selected == true).ElementAt(i);
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting = tableSittings.Where(s => s.SittingId == sitting.Id).ToList().ElementAt(i);
                    resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting = tableSittings.First(s => s.SittingId == sitting.Id && s.TableId == selectedTables.ToList().ElementAt(i).TableId);
                }

            }// removes tableReservations from the database to suit the new table assignment (tables have been removed or reduced)
            else if (resStatusModel.reservation.TableReservations.Count() > count)
            {

                deleteDuplicates = true;
                deletedCounter = resStatusModel.reservation.TableReservations.Count() - count;
                //Remove extra tableReservations from database

                if (count > 0) { 

                resStatusModel.reservation.TableReservations.RemoveRange(resStatusModel.reservation.TableReservations.Count() - count, resStatusModel.reservation.TableReservations.Count() - 1);

                //var tabRes = tableReservations;
                /*
                for (int i = 0; i < resStatusModel.reservation.TableReservations.ToList().Count() - count; i++)
                {
                    //Error appeasrs to be with this line
                    _context.TableReservations.Remove(resStatusModel.reservation.TableReservations.ToList().ElementAt(i));
                }

                await _context.SaveChangesAsync();
                */

                ///


                var selectedTables = resStatusModel.Tables.ToList().Where(st => st.Selected == true);
                //resStatusModel.reservation.TableReservations.ElementAt(0).TableSitting = tableSittings.First(ts => ts.Id == 8);
                //resStatusModel.reservation.TableReservations.ElementAt(0).TableSittingId = 2;

                //for (int i = 0; i < resStatusModel.Tables.ToList().Where(t => t.Selected == true).Count() - resStatusModel.reservation.TableReservations.Count(); i++) {
                for (int i = 0; i < resStatusModel.reservation.TableReservations.Count(); i++) {
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting.TableId = resStatusModel.Tables.ToList().Where(t => t.Selected == true).ElementAt(i).TableId;
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting = tableSittings.First(s => s.TableId == selectedTables.ElementAt(i).TableId);
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSittingId = tableSittings.ElementAt(i).Id;
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSittingId = tableSittings.ToList().ElementAt(i).Id;
                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSittingId = tableSittings.ToList().ElementAt(i).Id;


                    //resStatusModel.reservation.TableReservations.ElementAt(i).TableSittingId = tableSittings.First(ts => ts.TableId == selectedTables.ToList().ElementAt(i).TableId).Id;
                    if (count != 0)
                    {
                        resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting = _context.TableSittings.FirstOrDefault(tabSit => tabSit.Id == tableSittings.First(ts => ts.TableId == selectedTables.ToList().ElementAt(i).TableId).Id);
                    }


                }




                //var reservation = await _context.Reservations.FindAsync(id);
                //_context.Reservations.Remove(reservation);
                //await _context.SaveChangesAsync();


                //}
                //for (int i = 0; i < resStatusModel.reservation.TableReservations.Count())

                //    resStatusModel.Tables.ToList().Where(t => t.Selected == true).ElementAt(i).TableId;
                //_context.Update(resStatusModel.reservation);
                //await _context.SaveChangesAsync();

                }
            }// adds new tableReservations to the database to suit the new table assignment
            else {

                _context.Update(resStatusModel.reservation);
                // error happens below here
                // attempt to delete previous tableReservations, then add the new ones on top.
                for (int i = 0; i < resStatusModel.reservation.TableReservations.Count(); i++)
                {
                    _context.TableReservations.Remove(resStatusModel.reservation.TableReservations.ElementAt(i));
                }


                await _context.SaveChangesAsync();

                var newTableRes = _context.TableReservations.Where(tr => tr.ReservationId == resStatusModel.reservation.Id).Include(tr => tr.TableSitting).AsNoTracking().Include(tr => tr.Reservation).AsNoTracking();
                resStatusModel.reservation.TableReservations = newTableRes.ToList();

                //for (int i = 0; i < resStatusModel.reservation.TableReservations.Count() - count; i++)
                //{
                //    _context.TableReservations.Remove(resStatusModel.reservation.TableReservations.ElementAt(i + count));
                //}


                //await _context.SaveChangesAsync();
                // new code above.

                for (int i = 0; i < resStatusModel.reservation.TableReservations.Count(); i++)
                {
                    
                    // update the pre-existing details about the tableReservation.

                    var selectedTables = resStatusModel.Tables.Where(t => t.Selected == true);
                    
                    resStatusModel.reservation.TableReservations.ElementAt(i).TableSitting = tableSittings.First(s => s.SittingId == sitting.Id && s.TableId == selectedTables.ToList().ElementAt(i).TableId);

                    _context.TableReservations.Update(resStatusModel.reservation.TableReservations.ElementAt(i));
                    
                }

                
                _context.SaveChanges();
                //error is happening before here


                //var rS1M = resStatusModel.Tables.ToList().Where(t => t.Selected == true);
                var rSM = resStatusModel.Tables.ToList();
                //for (int i = 0; i < resStatusModel.Tables.Count(); i++) {
                rSM.RemoveRange(0, resStatusModel.Tables.Count());
                //}
                for (int i = 0; i < resStatusModel.Tables.Count(); i++) {
                    if (resStatusModel.Tables.ElementAt(i).Selected == true)
                    {
                        rSM.Add(resStatusModel.Tables.ElementAt(i));
                    }
                }
                //resStatusModel.reservation.TableReservations.Add(resStatusModel)
                var trCounter = 0;
                for (int i = 0; i < rSM.Count()-resStatusModel.reservation.TableReservations.Count()+trCounter; i++)
                {
                    //var ts = _context.TableSittings.FirstOrDefault(ts => ts.TableId == rSM.ElementAt(i).TableId);
                    var tId = (rSM.ElementAt(i + resStatusModel.reservation.TableReservations.Count()-trCounter).Table);
                    var table = _context.Tables.First(t => t.Id == tId.Id);

                    var ts = _context.TableSittings.FirstOrDefault(ts => ts.TableId == tId.Id && ts.SittingId == sitting.Id);

                    var newTr = new TableReservation { 
                        Reservation = resStatusModel.reservation,
                        ReservationId = resStatusModel.reservation.Id,
                        TableSitting = ts,
                        TableSittingId = ts.Id
                    };
                    //var tr = _context.TableReservations.Add(newTr);

                    resStatusModel.reservation.TableReservations.Add(newTr);
                    
                    trCounter += 1;
                    //_context.TableReservations.Add(newTr);
                    //_context.SaveChanges();


                }

                //_context.TableReservations.Update(resStatusModel.reservation.TableReservations);


            }

            //

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resStatusModel.reservation);
                    await _context.SaveChangesAsync();

                    if (deleteDuplicates == true) {
                        /*for (int i = 0; i < resStatusModel.reservation.TableReservations.ToList().Count() - count+deletedCounter; i++)
                        {
                            //Error appeasrs to be with this line
                            _context.TableReservations.Remove(resStatusModel.reservation.TableReservations.ToList().ElementAt(i));
                        }
                        */
                        //gets all the table reservations that are assigned to the reservation
                        var assignedReservations = _context.TableReservations.Where(tr => tr.ReservationId == resStatusModel.reservation.Id).ToList();
                        var toBeRemoved = _context.TableReservations.Where(tr => tr.ReservationId == resStatusModel.reservation.Id).ToList();
                        toBeRemoved.RemoveRange(0, toBeRemoved.Count());
                        for (int i = 0; i < assignedReservations.Count(); i++) {
                            var removeTR = true;
                            for (int j = 0; j < resStatusModel.reservation.TableReservations.ToList().Count(); j++) {
                                if (resStatusModel.reservation.TableReservations.ToList().ElementAt(j).TableSittingId == assignedReservations.ElementAt(i).TableSittingId) {
                                    removeTR = false;
                                }
                            
                            }

                            if (removeTR == true) {
                                toBeRemoved.Add(assignedReservations.ElementAt(i));
                            }

                        }


                        for (int i = 0; i < toBeRemoved.ToList().Count(); i++)
                        {
                            //Error appeasrs to be with this line
                            _context.TableReservations.Remove(toBeRemoved.ToList().ElementAt(i));
                        }

                        for (int i = 0; i < resStatusModel.reservation.TableReservations.Count() - count; i++) {
                            _context.TableReservations.Remove(resStatusModel.reservation.TableReservations.ElementAt(i+count));
                        }


                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(resStatusModel.reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(resStatusModel.reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservation = _context.Reservations.Where(r => r.Id == id).Include(c => c.Customer).FirstOrDefault();
            //var reservation = await _context.Reservations
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            


            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }


    }

}