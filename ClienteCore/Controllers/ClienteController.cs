using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClienteCore.Models;

namespace ClienteCore.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteDAL cli;
        public ClienteController(IClienteDAL cliente)
        {
            cli = cliente;
        }
        public IActionResult Index()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            listaClientes = cli.GetAllClientes().ToList();
            return View(listaClientes);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente = cli.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cli.AddCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente = cli.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                cli.UpdateCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente = cli.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            cli.DeleteCliente(id);
            return RedirectToAction("Index");
        }
    }
}
