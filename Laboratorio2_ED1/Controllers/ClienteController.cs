using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorio2_ED1.Models.Storage;

namespace Laboratorio2_ED1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            Singleton.Instance.miCliente.Nombre = collection["Nombre"];
            Singleton.Instance.miCliente.Direccion = collection["Direccion"];
            Singleton.Instance.miCliente.Nit = long.Parse(collection["Nit"]);
            return RedirectToAction("Index", "OrdenPedido");
        }
    }
}
