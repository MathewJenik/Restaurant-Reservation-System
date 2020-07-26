using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T2RMSWS.Data;

namespace T2RMSWS.Controllers
{
    public class StaffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Exists()
        {
            return View();
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staff.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone")] Staff newStaff)
        {
            var checkEmail = _context.People.Where(e => e.Email.ToLower() == newStaff.Email.ToLower()).FirstOrDefault();
            if (checkEmail != null)
            {
                return RedirectToAction(nameof(Exists));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var last = new Person();
                    last = _context.People.ToList().Last();
                    var lastId = last.Id.ToString();
                    int returnInt;
                    bool intResultTryParse = int.TryParse(lastId.ToString(), out returnInt);
                    var intLastId = 0;
                    if (intResultTryParse == true)
                    {
                        intLastId = returnInt;
                    }
                    else
                    {
                        intLastId = 5;
                    }
                    var peopleList = _context.People.Where(p => p.Id == (intLastId + 1).ToString());

                    while (peopleList.ToList().Count() != 0)
                    {
                        intLastId++;
                        peopleList = _context.People.Where(p => p.Id == intLastId.ToString());
                    }
                    //get last StaffId
                    int lastStaffId = _context.Staff
                        .Select(s => s.StaffId).ToList().Max();
                    //increment to next Id

                    int nexStaffId = lastStaffId + 1;

                    var nextStaff = new Staff
                    {
                        Id = intLastId.ToString(),
                        StaffId = nexStaffId,
                        FirstName = newStaff.FirstName,
                        LastName = newStaff.LastName,
                        Email = newStaff.Email,
                        Phone = newStaff.Phone
                    };
                    _context.Add(nextStaff);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
           

            }
            return View(newStaff);


        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StaffId,Id,FirstName,LastName,Email,Phone")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.Id))
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
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(string id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
