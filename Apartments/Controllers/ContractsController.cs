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
    public class ContractsController : Controller
    {
        private readonly ApartmentContext _context;

        public ContractsController(ApartmentContext context)
        {
            _context = context;    
        }

        /*
                             GET: Contracts
          In the index we will obtain tenant, apartment and contract information
          ADDED: Sort and search capabilities:
            - Sort Rent and End date
            - Search for Name (last/first) and apt address 
        */

        public async Task<IActionResult> Index(string sortOrder, string searchString, string searchApt)
        {
            ViewData["RentSortParm"] = String.IsNullOrEmpty(sortOrder) ? "rent_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter2"] = searchApt;

            var apartmentContext = from ac in _context.Contracts.Include(c => c.Apartment).Include(c => c.Tenant)
                select ac;

            if (!String.IsNullOrEmpty(searchApt))
            {
                apartmentContext = apartmentContext.Where(ac => ac.Apartment.AptAddress.Contains(searchApt));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                apartmentContext = apartmentContext.Where(ac => ac.Tenant.FirstName.Contains(searchString)
                                                                || ac.Tenant.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "rent_desc":
                    apartmentContext = apartmentContext.OrderByDescending(ac => ac.MonthlyRent);
                    break;
                case "Date":
                    apartmentContext = apartmentContext.OrderBy(ac => ac.EndDate);
                    break;
                case "date_desc":
                    apartmentContext = apartmentContext.OrderByDescending(ac => ac.EndDate);
                    break;
                default:
                    apartmentContext = apartmentContext.OrderBy(ac => ac.MonthlyRent);
                    break;
            }

            return View(await apartmentContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Apartment)
                .Include(c => c.Tenant)
                .SingleOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "AptAddress");
            ViewData["TenantId"] = new SelectList(_context.Tenants, "TenantId", "Email");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,StartDate,EndDate,MonthlyRent,ApartmentId,TenantId")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", contract.ApartmentId);
            ViewData["TenantId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", contract.TenantId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.SingleOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "AptAddress", contract.ApartmentId);
            ViewData["TenantId"] = new SelectList(_context.Tenants, "TenantId", "Email", contract.TenantId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,StartDate,EndDate,MonthlyRent,ApartmentId,TenantId")] Contract contract)
        {
            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractId))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", contract.ApartmentId);
            ViewData["TenantId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", contract.TenantId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Apartment)
                .Include(c => c.Tenant)
                .SingleOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.SingleOrDefaultAsync(m => m.ContractId == id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }
    }
}
