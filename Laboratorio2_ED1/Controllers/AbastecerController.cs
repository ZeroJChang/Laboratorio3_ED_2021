using Laboratorio2_ED1.Models.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio2_ED1.Controllers
{
    public class AbastecerController : Controller
    {
        // GET: AbastecerController
        public ActionResult Index()
        {
            foreach (var item in Singleton.Instance.misMedicamentosExt)
            {
                if (item.Existencia == 0)
                {
                    Singleton.Instance.miAsbastecer.Add(item);
                }
            }
            return View(Singleton.Instance.miAsbastecer);
        }
        public ActionResult ReAbastecer(string tag)
        {
            Random rnd = new Random();

            foreach (var item in Singleton.Instance.miAsbastecer)
            {
                var std = Singleton.Instance.misMedicamentosExt.Where(s => s.Id == item.Id).FirstOrDefault();
                if (std.Existencia == 0)
                {
                    std.Existencia = rnd.Next(1, 15);
                    Singleton.Instance.miArbolMedicamentos.Add(std);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
