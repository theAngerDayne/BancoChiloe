using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BancoChiloe.Data;
using BancoChiloe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoChiloe.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;



        public UsuariosController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            var usuarios = _db.AppUsers.ToList();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(AppUser usuario)
        {
            if (!ModelState.IsValid)
                return View();

            /*No pueden haber 2 email iguales*/

            var userFromdb = await _db.AppUsers.Where(u => u.Email == usuario.Email).FirstOrDefaultAsync();
            if (userFromdb != null)
            {
                ViewBag.server = "Ya existe un usuario registrado con el email " + usuario.Email;
                return View();
            }


            var user = new AppUser
            {
                Email = usuario.Email,
                UserName = usuario.Email,               
            };

            var result = await _userManager.CreateAsync(user, usuario.AuxPass);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));
            else
            {
                string mensaje = string.Empty;

                foreach (var item in result.Errors)
                {
                    mensaje += item.Description + "\n";
                }
                
                ViewBag.server = mensaje;
                return View();
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            return View(await _db.AppUsers.Where(u => u.Id == id).FirstOrDefaultAsync());
        }

        
        [HttpPost, ActionName("Edit")]
        public async  Task<IActionResult> EditPost(AppUser usuario)
        {
            if (!ModelState.IsValid)
                return View();

            /*No pueden haber 2 email iguales y es distinto a mi email*/

            var emailVal = await _db.AppUsers.Where(u => u.Email == usuario.Email).FirstOrDefaultAsync();

            if (emailVal != null && emailVal.Email != usuario.Email)
            {
                ViewBag.server = "Ya existe un usuario registrado con el email " + usuario.Email;
                return View();
            }

            var userFromdb = await _db.AppUsers.Where(u => u.Id == usuario.Id).FirstOrDefaultAsync();
            string emailAux = userFromdb.Email;

            userFromdb.Email = usuario.Email;
            userFromdb.UserName = usuario.Email;

            var user = await _userManager.FindByIdAsync(userFromdb.Id);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, usuario.AuxPass);


            if (result.Succeeded)
            {
                if (usuario.Email != emailAux)
                {
                    await _signInManager.SignOutAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string mensaje = string.Empty;

                foreach (var item in result.Errors)
                {
                    mensaje += item.Description + "\n";
                }

                ViewBag.server = mensaje;
                return View();
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();


            var user = await _db.AppUsers.FindAsync(id);

            if (user == null)
                return NotFound();
            return View(user);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _db.AppUsers.FindAsync(id);
            /*deberia obtener el usuario actual por si se elimina a si mismo y deslogear. Como no hay regla de negocio clara, se procede a eso*/

            string currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var curUserFromdb = _db.AppUsers.Where(u => u.Id == currentUser).FirstOrDefault();
            _db.Remove(usuario);
           // await _db.SaveChangesAsync();

            if(usuario.Email == curUserFromdb.Email)
                await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
