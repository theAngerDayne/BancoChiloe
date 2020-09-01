using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BancoChiloe.Data;
using BancoChiloe.Models;
using BancoChiloe.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BancoChiloe.Extensiones;
using System.Security.Claims;

namespace BancoChiloe.Controllers
{
    [Authorize]
    public class CuentasController : Controller
    {
        private readonly ApplicationDbContext _db;       

        [BindProperty]
        public CuentaViewModel CuentaVM { get; set; }


        public CuentasController(ApplicationDbContext db)
        {
            _db = db;
            
            CuentaVM = new CuentaViewModel()
            {
                Cuenta = new Cuenta()
            };
        }

        // GET: Cuentas
        public async Task<IActionResult> Index()
        {
            return View(await _db.Cuentas.Include(c => c.Cliente).Where(c => c.IsActive == true).ToListAsync());
        }

        // GET: Cuentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _db.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        
        public IActionResult Create()
        {
            CuentaVM.ListaClientes = _db.Clientes.ToList();
            return View(CuentaVM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {

                var cliente = await _db.Clientes.Where(c => c.Id == CuentaVM.Cuenta.AuxClienteID).FirstOrDefaultAsync();
              
                /*Chequear si cliente tiene cuenta? la regla de negocio deberia ser que el banco pueda admitir que el cliente tenga multiples cuentas, 
                 pero como no se explica en el documento, omitiremos*/
                var cuentaFromDb = await _db.Cuentas.Include(c => c.Cliente).Where(c => c.Cliente.Id == cliente.Id).FirstOrDefaultAsync();
                

                /*Lo mismo, no hay regla de negocio clara, pero podria volver a activar si es que existe el rut*/
                if (cuentaFromDb != null)
                {
                    ViewBag.server = "Ya hay una cuenta asociada a ese rut.";
                    
                    CuentaVM.ListaClientes = _db.Clientes.ToList();
                    return View(CuentaVM);                    
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var usuario = _db.AppUsers.Find(userId);                

                CuentaVM.Cuenta.NombreUsuario = usuario.Email;

                CuentaVM.Cuenta.Cliente = cliente;
                CuentaVM.Cuenta.FechaApertura = DateTime.Now;                

                _db.Cuentas.Add(CuentaVM.Cuenta);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CuentaVM);
        }

        // GET: Cuentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _db.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }
            return View(cuenta);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cuenta cuenta)
        {
            if (id != cuenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var usuario = _db.AppUsers.Find(userId);

                    cuenta.NombreUsuario = usuario.Email;

                    _db.Update(cuenta);

                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaExists(cuenta.Id))
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
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _db.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuenta = await _db.Cuentas.FindAsync(id);
            cuenta.IsActive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentaExists(int id)
        {
            return _db.Cuentas.Any(e => e.Id == id);
        }
    }
}
