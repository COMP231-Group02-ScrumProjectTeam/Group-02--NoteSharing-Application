using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoteSharingApp.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

/// <summary>
/// 
/// </summary>
namespace NoteSharingApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly NoteSharingContext _context;

        private IHostingEnvironment hostingEnv;
        public DocumentsController(NoteSharingContext context, IHostingEnvironment env)
        {
            _context = context;
            this.hostingEnv = env;
        }

        [Authorize]
        private IEnumerable<UserForDisplay> GetUserList()
        {
            return _context.Users.Select(u => new UserForDisplay
            { ID = u.ID,
                DisplayName = u.FirstName +
              ((!string.IsNullOrWhiteSpace(u.FirstName) && !string.IsNullOrWhiteSpace(u.LastName))
               ? " " : "") + u.LastName

            }

            );
        }

        [Authorize]
        public IActionResult PostNotes()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            //var UserID = claims.Where(c => c.Type== "UserID");
            var UserID=claims.Where(c => c.Type == "UserID")
                   .Select(c => c.Value).SingleOrDefault();

            int ID = int.Parse(UserID);

            var loginUser = _context.Users.Where(u => u.ID == ID).FirstOrDefault();

            ViewBag.UserID = loginUser.ID;
            ViewBag.Name = loginUser.FirstName + " " + loginUser.LastName;
            ViewBag.School = loginUser.College;
            ViewBag.EnrollmentYear = loginUser.Enrol_year;
            ViewBag.CurrentSemester = loginUser.Semester;

            ViewData["Message"] = "Your post notes page.";


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostNotes([Bind("ID,DocumentTypeID,Extension,FileName,ModifiedData,Size,Title,UserID")] Document document,IList<IFormFile> files)
        {
            long size = 0;
            if (ModelState.IsValid)
            {
                foreach (var file in files)
            {
                
                    Document docForSave = new Document();
                    docForSave.DocumentType = file.ContentType;
                        docForSave.UserID = document.UserID;
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                    var modificationDate = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition).ModificationDate;

                    string extension = Path.GetExtension(filename);




                    docForSave.FileName = filename;

                    docForSave.Extension = extension;
                    docForSave.Size = file.Length;
                    docForSave.UploadDateTimeOffset = new DateTimeOffset(DateTime.Now,
                  TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").GetUtcOffset(DateTime.Now));

                    _context.Add(docForSave);
                    await _context.SaveChangesAsync();
                    filename = hostingEnv.WebRootPath + $@"\{filename}";
                    size += file.Length;
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                  
                }
                ViewBag.Message = $"{files.Count} file(s) / {size} bytes uploaded successfully!";
                return RedirectToAction("Index");

            }

            ViewData["UserID"] = new SelectList(GetUserList(), "ID", "DisplayName");

            return View(document);
           // return View();
        }


        // GET: Documents
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var noteSharingContext = _context.Documents.Include(d => d.User);
            return View(await noteSharingContext.ToListAsync());
        }

        // GET: Documents/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.ID == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        [Authorize]
        public IActionResult Create()
        {

            ViewData["UserID"] = new SelectList(GetUserList(), "ID", "DisplayName");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,DocumentTypeID,Extension,FileName,ModifiedData,Size,Title,UserID")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewData["UserID"] = new SelectList(GetUserList(), "ID", "DisplayName");
            return View(document);
        }

        // GET: Documents/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.ID == id);
            if (document == null)
            {
                return NotFound();
            }

            ViewData["UserID"] = new SelectList(GetUserList(), "ID", "DisplayName");
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Extension,DocumentType,FileName,UploadDateTimeOffset,Size,Title,UserID")] Document document)
        {
            if (id != document.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.ID))
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
          
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", document.UserID);
            return View(document);
        }

        // GET: Documents/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.ID == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.SingleOrDefaultAsync(m => m.ID == id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.ID == id);
        }
    }
}
