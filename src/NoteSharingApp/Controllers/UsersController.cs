using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteSharingApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace NoteSharingApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly NoteSharingContext _context;
        private readonly ILogger _logger;
        private readonly SignInManager<User> _signInManager;
        public UsersController(NoteSharingContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<UsersController>();
        }


        private bool IsValid(string _username, string _password)
        {
            if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
            {


                int count = _context.Users.Where(u => u.User_name.ToLower() == _username.Trim().ToLower() && u.Password == _password.Trim()).Count();



                if (count > 0)
                {

                    return true;
                }
                else
                {

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Models.User user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (IsValid(user.User_name, user.Password))
                {
                    // FormsAuthentication.SetAuthCookie(user.User_name, user.RememberMe);
                    try
                    {
                        var userLogin=_context.Users.Where(u => u.User_name.ToLower() == user.User_name.Trim().ToLower() && u.Password == user.Password.Trim()).FirstOrDefault();
                        const string Issuer = "http://noteSharingApp.ca";

                        var claims = new List<Claim> {
                    new Claim("UserID",userLogin.ID.ToString(),ClaimValueTypes.Integer64, Issuer),
                    new Claim(ClaimTypes.Name,userLogin.User_name , ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.GivenName, userLogin.FirstName?? "", ClaimValueTypes.String, Issuer),
                    new Claim("College", userLogin.College?? "", ClaimValueTypes.String, Issuer),
                    new Claim("Enrol_year", userLogin.Enrol_year?? "", ClaimValueTypes.String,Issuer),
                    
                    };

                        var userIdentity = new ClaimsIdentity(claims, "Passport");

                        var userPrincipal = new ClaimsPrincipal(userIdentity);
                    

                        await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
                        
                        _logger.LogInformation(1, "User logged in.");

                        return RedirectToLocal(returnUrl);
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", ""+ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
           
            await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        // GET: Users
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register([Bind("ID,College,Enrol_year,FirstName,LastName,Password,Program,Semester,User_name")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,College,Enrol_year,FirstName,LastName,Password,Program,Semester,User_name")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
