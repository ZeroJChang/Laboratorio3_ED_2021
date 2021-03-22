using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorio2_ED1.Models.Storage;
using Laboratorio2_ED1.Models;
using PagedList;
//Paquete de nugget, PagedList.MVC https://albertcapdevila.net/paginar-mvc5/

namespace Laboratorio2_ED1.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        public ActionResult Index(int? page)
        {
            if (Request.Method != "GET")
            {
                page = 1;
            }
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(Singleton.Instance.miArbolMedicamentos.ObtenerLista().ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Index(int? page, IFormCollection collection)
        {
            if (Request.Method != "GET")
            {
                page = 1;
            }
            int pageSize = 25;
            int pageNumber = (page ?? 1);

            var name = collection["search"];
            return View(MedicamentoModel.Filter(name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ConfirmarM(string tag)
        {
            Singleton.Instance.miPedido.Clear();
            return RedirectToAction("Index", "Cliente");
        }




        public ActionResult Edit(int id)
        {
            var std = Singleton.Instance.misMedicamentosExt.Where(s => s.Id == id).FirstOrDefault();
            return View(std);
        }

        //
        // POST: /Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                int pedido = int.Parse(collection["Existencia"]);
                int exist = Singleton.Instance.misMedicamentosExt[id - 1].Existencia;
                bool agregar = true;

                if (exist == 0)
                {
                    ViewBag.Message = "No se encuentra en existencia este producto";
                    agregar = false;
                }
                else if (pedido > Singleton.Instance.misMedicamentosExt[id - 1].Existencia)
                {
                    ViewBag.Message = "Solo se agregaron: " + exist + " a la orden.";
                    pedido = exist;
                    exist = 0;
                }
                else
                {
                    if (pedido == exist)
                    {
                        exist = 0;
                    }
                    ViewBag.Message = pedido + " " + '"' + Singleton.Instance.misMedicamentosExt[id - 1].Nombre + '"' + " agregados a la orden.";
                    Singleton.Instance.misMedicamentosExt[id - 1].Existencia -= pedido;
                }

                if (exist == 0)
                {
                    Singleton.Instance.misMedicamentosExt[id - 1].Existencia = 0;
                    Singleton.Instance.miArbolMedicamentos.Remove(Singleton.Instance.misMedicamentosExt[id - 1]);
                }
                if (agregar)
                {
                    var nuevoPedido = new MedicamentoExtModel
                    {
                        Nombre = Singleton.Instance.misMedicamentosExt[id - 1].Nombre,
                        Precio = Singleton.Instance.misMedicamentosExt[id - 1].Precio,
                        Existencia = pedido
                    };
                    Singleton.Instance.miPedido.Add(nuevoPedido);
                }

                return View(Singleton.Instance.misMedicamentosExt[id - 1]);
            }
            catch
            {
                return View();
            }
        }




    }
}
