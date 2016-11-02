using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoteSharingApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace NoteSharingApp.Controllers
{
    [Authorize]
    public class UserCommentsController : Controller
    {
        private readonly NoteSharingContext _context;

        public UserCommentsController(NoteSharingContext context)
        {
            _context = context;    
        }

        // GET: UserComments
        public async Task<IActionResult> Index()
        {
            var noteSharingContext = _context.UserComments.Include(u => u.Document).Include(u => u.User);
            return View(await noteSharingContext.ToListAsync());
        }

        // GET: UserComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments.SingleOrDefaultAsync(m => m.ID == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // GET: UserComments/Create
        public IActionResult Create()
        {
            ViewData["DocumentID"] = new SelectList(_context.Documents, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: UserComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Comment,DocumentID,UserID,Vote")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DocumentID"] = new SelectList(_context.Documents, "ID", "ID", userComment.DocumentID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", userComment.UserID);
            return View(userComment);
        }

        // GET: UserComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments.SingleOrDefaultAsync(m => m.ID == id);
            if (userComment == null)
            {
                return NotFound();
            }
            ViewData["DocumentID"] = new SelectList(_context.Documents, "ID", "ID", userComment.DocumentID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", userComment.UserID);
            return View(userComment);
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Comment,DocumentID,UserID,Vote")] UserComment userComment)
        {
            if (id != userComment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCommentExists(userComment.ID))
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
            ViewData["DocumentID"] = new SelectList(_context.Documents, "ID", "ID", userComment.DocumentID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", userComment.UserID);
            return View(userComment);
        }

        // GET: UserComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments.SingleOrDefaultAsync(m => m.ID == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // POST: UserComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userComment = await _context.UserComments.SingleOrDefaultAsync(m => m.ID == id);
            _context.UserComments.Remove(userComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserCommentExists(int id)
        {
            return _context.UserComments.Any(e => e.ID == id);
        }
    }
}
