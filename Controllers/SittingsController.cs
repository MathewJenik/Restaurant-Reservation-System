using System;
using System.Diagnostics; // add System.Diagnostics for Debugging
using System.IO; // Add System.IO for Trace
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T2RMSWS.Data;

namespace T2RMSWS.Controllers
{
    public class SittingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SittingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sittings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sittings.Include(s => s.SittingStatus).Include(s => s.SittingType);          
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sittings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings
                .Include(s => s.SittingStatus)
                .Include(s => s.SittingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);
        }

        // GET: Sittings/Create
        public IActionResult Create()
        {
            using (FileStream myFileSteam = new FileStream("CreateSittingLog.txt", FileMode.Append))
            {
                TextWriterTraceListener listener = new TextWriterTraceListener(myFileSteam);
                Trace.Listeners.Add(listener);
                
                Debug.Indent();
                // Debug for the Sitting Create Action
                Debug.WriteLine($"{DateTime.Now} -- Debugging in Create Sittings Form to display tables and area");
                ViewData["SittingStatusId"] = new SelectList(_context.SittingStatuses, "Id", "Description");
                ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Description");
                var m = new Models.SittingsManagement.CreateVM();

                //get all tables from db 
                //and create list of tables that will be displayed on the page
                m.Tables = _context.Tables.Select(t => new Models.SittingsManagement.TableSitting
                {
                    Description = $"{t.TableNo} ({t.TableCapacity})",
                    Capacity = t.TableCapacity,
                    AreaId = t.AreaId,
                    TableNo = t.TableNo,
                    TableId = t.Id,
                    Selected = true
                }).ToList();
                //Check to see that there are tables to display
                //Debug.Assert(m.Tables.Count() > 40, $"{DateTime.Now} -- Table Count is less than 40");
                Debug.WriteLineIf(m.Tables is { }, $"{DateTime.Now} -- There are {m.Tables?.Count} Tables");

                //get all areas from db
                //and use them to create dropdown fields
                m.Areas = _context.Areas.Select(a => new Models.SittingsManagement.AreaModel
                {
                    Description = a.Description,
                    AreaId = a.Id
                }).ToList();
                // check to see that there are Areas 
                //Debug.Assert(m.Areas.Count() > 4, $"{DateTime.Now} -- There are less than 4 Areas in the resturant");
                Debug.WriteLineIf(m.Areas is { }, $"{DateTime.Now} -- There are {m.Areas?.Count} Areas");

                // Flush and close Debug
                Debug.Close();

                // **MUST** Flush and close Debug
                Trace.Close();

                m.Sitting = new Sitting();

                return View(m);
            }
            
        }


        // POST: Sittings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.SittingsManagement.CreateVM m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Sittings.Add(m.Sitting);
                    var selected = m.Tables.Where(table => table.Selected).ToArray();
                    foreach (var t in selected)
                    { 
                        m.Sitting.TableSittings.Add(new TableSitting{TableId = t.TableId});
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            ViewData["SittingStatusId"] = new SelectList(_context.SittingStatuses, "Id", "Description", m.Sitting.SittingStatusId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Description", m.Sitting.SittingTypeId);

            return View(m);
        }

        // GET: Sittings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting == null)
            {
                return NotFound();
            }

            ViewData["SittingStatusId"] = new SelectList(_context.SittingStatuses, "Id", "Description", sitting.SittingStatusId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Description", sitting.SittingTypeId);
            return View(sitting);
        }

        // POST: Sittings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SittingTypeId,StartDateTime,EndDateTime,Capacity,SittingStatusId")] Sitting sitting)
        {
            if (id != sitting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SittingExists(sitting.Id))
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
            ViewData["SittingStatusId"] = new SelectList(_context.SittingStatuses, "Id", "Description", sitting.SittingStatusId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Description", sitting.SittingTypeId);
            return View(sitting);
        }

        // GET: Sittings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings
                .Include(s => s.SittingStatus)
                .Include(s => s.SittingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);
        }

        // POST: Sittings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sitting = await _context.Sittings.FindAsync(id);
            _context.Sittings.Remove(sitting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SittingExists(int id)
        {
            return _context.Sittings.Any(e => e.Id == id);
        }
    }
}
