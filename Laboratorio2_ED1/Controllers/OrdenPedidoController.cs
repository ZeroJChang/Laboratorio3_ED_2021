using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorio2_ED1.Models.Storage;

namespace Laboratorio2_ED1.Controllers
{
    public class OrdenPedidoController : Controller
    {
        // GET: OrdenPedidoController
        public ActionResult Index()
        {
            TempData["nombre"] = Singleton.Instance.miCliente.Nombre;
            double total = 0;
            foreach (var item in Singleton.Instance.miPedido)
            {
                total += item.Precio * item.Existencia;
            }
            TempData["total"] = "$ " + total;
            return View(Singleton.Instance.miPedido);
        }
        public ActionResult ConfirmarP(string tag)
        {
            Singleton.Instance.miPedido.Clear();
            return RedirectToAction("Index", "Medicamento");
        }

      
    }
}
