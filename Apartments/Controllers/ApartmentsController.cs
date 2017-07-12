using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apartments.Data;
using Apartments.Models;

namespace Apartments.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApartmentContext _context;

        public ApartmentsController(ApartmentContext context)
        {
            _context = context;    
        }

        /*
                        GET: Apartments
            In the index we will get all relevant information from the db
            ADDED: Sort and search capabilities:
            - Sort address and clean date
            - Search for apt address 
        */
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var apts = from a in _context.Apartments
                select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                apts = apts.Where(a => a.AptAddress.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    apts = apts.OrderByDescending(t => t.AptAddress);
                    break;
                case "Date":
                    apts = apts.OrderBy(t => t.LastCleanDate);
                    break;
                case "date_desc":
                    apts = apts.OrderByDescending(t => t.LastCleanDate);
                    break;
                default:
                    apts = apts.OrderBy(t => t.AptAddress);
                    break;
            }
            return View(await apts.AsNoTracking().ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .SingleOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,AptAddress,SqFootage,MonthUtilityFee,MonthParkfee,LastCleanDate,IsVacant")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.SingleOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,AptAddress,SqFootage,MonthUtilityFee,MonthParkfee,LastCleanDate,IsVacant")] Apartment apartment)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .SingleOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartments.SingleOrDefaultAsync(m => m.ApartmentId == id);
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.ApartmentId == id);
        }
    }
}
