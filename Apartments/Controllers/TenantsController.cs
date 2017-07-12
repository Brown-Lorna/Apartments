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
    public class TenantsController : Controller
    {
        private readonly ApartmentContext _context;

        public TenantsController(ApartmentContext context)
        {
            _context = context;
        }

        /*
                             GET: Tenants
          In the index we will get tenants' information
          ADDED: Sort and search capabilities:
            - Sort Name
            - Search for Name (last/first)
        */

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Lname_desc" : "Lname_asc";
            ViewData["CurrentFilter"] = searchString;

            var tenant = from t in _context.Tenants
                select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tenant = tenant.Where(t => t.FirstName.Contains(searchString)
                                           || t.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    tenant = tenant.OrderByDescending(t => t.FirstName);
                    break;
                case "Lname_asc":
                    tenant = tenant.OrderBy(t => t.LastName);
                    break;
                case "Lname_desc":
                    tenant = tenant.OrderByDescending(t => t.LastName);
                    break;
                default:
                    tenant = tenant.OrderBy(t => t.FirstName);
                    break;
            }
            return View(await tenant.AsNoTracking().ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .Include(s => s.Contracts)
                .ThenInclude(e => e.Apartment)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)

            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Phone,Email")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.SingleOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,FirstName,LastName,Phone,Email")] Tenant tenant)
        {
            if (id != tenant.TenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.TenantId))
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
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .SingleOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.SingleOrDefaultAsync(m => m.TenantId == id);
            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.TenantId == id);
        }
    }
}
