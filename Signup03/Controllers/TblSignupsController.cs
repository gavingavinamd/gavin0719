using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Signup03.Models;

namespace Signup03.Controllers
{
    public class TblSignupsController : Controller
    {
        private readonly Gavin01DBContext _context;

        public TblSignupsController(Gavin01DBContext context)
        {
            _context = context;
        }

        // GET: TblSignups
        public async Task<IActionResult> Index()
        {
              return _context.TblSignups != null ? 
                          View(await _context.TblSignups.ToListAsync()) :
                          Problem("Entity set 'Gavin01DBContext.TblSignups'  is null.");
        }

        // GET: TblSignups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblSignups == null)
            {
                return NotFound();
            }

            var tblSignup = await _context.TblSignups
                .FirstOrDefaultAsync(m => m.CId == id);
            if (tblSignup == null)
            {
                return NotFound();
            }

            return View(tblSignup);
        }

        // GET: TblSignups/Create
        public IActionResult Create()
        {
            var activeItems = _context.TblActiveItems.ToList(); //把tclActiveItem的資料帶進來  tolist有點像實體化
            var SignupForm = new SignupForm();                  //實體化SignupForm
            SignupForm.ActiveItem = activeItems;                

            return View(SignupForm);
        }

        // POST: TblSignups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SignupForm tblSignup)
        {
            if (ModelState.IsValid)
            {
                var tblSignupItem = new TblSignupItem();
                //var signupForm = new SignupForm();

                tblSignupItem.CSignupId = _context.TblSignups.Max(x => x.CId) + 1; 

                for(int i =0; i<tblSignup.Activities.Count; i++)
                {
                    tblSignupItem.CItemId = Convert.ToInt32(tblSignup.Activities[i]);                    
                    _context.Add(tblSignupItem);
                    await _context.SaveChangesAsync();
                }
                                         
                tblSignup.CCreateDt = DateTime.Now;
                
                _context.Add(tblSignup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblSignup);
        }

        // GET: TblSignups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblSignups == null)
            {
                return NotFound();
            }

            var tblSignup = await _context.TblSignups.FindAsync(id);
            if (tblSignup == null)
            {
                return NotFound();
            }
            return View(tblSignup);
        }

        // POST: TblSignups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CId,CMobile,CName,CEmail,CCreateDt")] TblSignup tblSignup)
        {
            if (id != tblSignup.CId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSignup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSignupExists(tblSignup.CId))
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
            return View(tblSignup);
        }

        // GET: TblSignups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblSignups == null)
            {
                return NotFound();
            }

            var tblSignup = await _context.TblSignups
                .FirstOrDefaultAsync(m => m.CId == id);
            if (tblSignup == null)
            {
                return NotFound();
            }

            return View(tblSignup);
        }

        // POST: TblSignups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblSignups == null)
            {
                return Problem("Entity set 'Gavin01DBContext.TblSignups'  is null.");
            }
            var tblSignup = await _context.TblSignups.FindAsync(id);
            if (tblSignup != null)
            {
                _context.TblSignups.Remove(tblSignup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSignupExists(int id)
        {
          return (_context.TblSignups?.Any(e => e.CId == id)).GetValueOrDefault();
        }

        public IActionResult Search()
        {
            ViewData["Message"] = "搜尋活動列表";
            return View(new SearchView());
        }
        [HttpPost]
        public IActionResult Search(ActiveSearchParams searchParams)
        {
            var searchView = new SearchView();

            var result = _context.TblSignups.ToList(); //先把_context.TblSignups實體化

            if (searchParams.SearchName != null)
            {
                result = (List<TblSignup>)_context.TblSignups.Where(x => x.CName == searchParams.SearchName);
            }



                return View(searchView);
        }
    }
}
