using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BancoChiloe.Data;
using BancoChiloe.Models;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Data;
using Infrastructure.Services.Clientes;
using Core;

namespace BancoChiloe.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ClientesService _clientesService;

        public ClientesController(ClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _clientesService.GetClientes());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {          

            var cliente = await _clientesService.GetClienteById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.FechaNacimiento >= DateTime.Now)
                {
                    ViewBag.server = "¡Error con la fecha! ¡Debe ser menor a la fecha actual.!";
                    return View();
                }

                          

                return RedirectToAction("Index", "Clientes", new { recepcion = "Cliente creado con éxito." });
            }
            return View(cliente);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (cliente.FechaNacimiento >= DateTime.Now)
                {
                    ViewBag.server = "¡Error con la fecha! ¡Debe ser menor a la fecha actual.!";
                    return View();
                }

                try
                {
                    _db.Update(cliente);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _db.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            
            try
            { 
            _db.Clientes.Remove(cliente);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.server = "Cliente se encuentra asociado a una cuenta, no puede eliminarlo.";
                return View(cliente);
            }                        
            
        }

        private bool ClienteExists(int id)
        {
            return _db.Clientes.Any(e => e.Id == id);
        }
    }
}
