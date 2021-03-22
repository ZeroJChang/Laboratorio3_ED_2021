using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorio2_ED1.Models.Storage;
using Laboratorio2_ED1.Models;
using System.IO;
using System.Web;
using System.Configuration;


namespace Laboratorio2_ED1.Controllers
{
    public class AgregarArchivoController : Controller
    {
        // GET: AgregarArchivoController
        public ActionResult Index()
        {
            CargarArchivo();
            return RedirectToAction("Index", "Home");
        }
        public void CargarArchivo()
        {                                                 // Poner la ubicacion exacta del archivo MOCK_DATA.txt

            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\ZeroJChang\Desktop\Laboratorio2_ED1\MOCK_DATA.txt");
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\ZeroJChang\Desktop\MOCK_DATA.txt");

            foreach (string line in lines)
            {
                string[] medicina = SplitString(line, ','); //dividir datos
                MedicamentoExtModel nuevoMedicamento = new MedicamentoExtModel
                {
                    Id = int.Parse(medicina[0]),
                    Nombre = medicina[1],
                    Descripcion = medicina[2],
                    CasaProd = medicina[3],
                    Precio = Convert.ToDouble(medicina[4]),
                    Existencia = int.Parse(medicina[5])
                };
                Singleton.Instance.misMedicamentosExt.Add(nuevoMedicamento);
                Singleton.Instance.miArbolMedicamentos.Add(nuevoMedicamento);
            }
        }

        string[] SplitString(string texto, char separador)
        {
            string[] Resultado = new string[6];
            int count = 0;
            int indiceVector = -1;
            string palabra = "";
            bool caracterEspecial = false;

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto.Substring(count, 1) != separador.ToString()) //comparar cadaletra con el separador
                {
                    if (texto.Substring(count, 1) == '\u0022'.ToString())
                    {
                        caracterEspecial = !caracterEspecial; //cambiar el estado de un " encontrado
                    }
                    else if (texto.Substring(count, 1) == '$'.ToString())
                    {
                        //hacer nada
                    }
                    else
                    {
                        palabra += texto.Substring(count, 1);
                    }
                    count++;
                }
                else if (texto.Substring(count, 1) == separador.ToString() && caracterEspecial == true)
                {
                    palabra += texto.Substring(count, 1);
                    count++;
                }
                else
                {
                    if (indiceVector < 6)
                    {
                        indiceVector++;
                        Resultado[indiceVector] = palabra;
                        palabra = "";
                        count++;
                    }
                }
            }

            string[] algo = texto.Split(',');
            Resultado[5] = algo[algo.Length - 1];
            return Resultado;
        }
        // [HttpGet]
        //public ActionResult SubirArchivo()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult SubirArchivo(IFormCollection collection)
        //{
        //    try
        //    {
        //        var path = collection["path"];
        //        StreamReader streamReader = new StreamReader(path);
        //        var line = streamReader.ReadLine();
        //        line = streamReader.ReadLine();
        //        var medicineData = new List<string>();
        //        while (line != null)
        //        {
        //            while (line != null)
        //            {
        //                var comilla = line.IndexOf('"');
        //                var coma = line.IndexOf(',');

        //                if (coma < comilla)
        //                {
        //                    var data = line.Substring(0, coma);
        //                    line = line.Remove(0, coma + 1);
        //                    medicineData.Add(data);
        //                }
        //                else
        //                {
        //                    if (comilla < 0)
        //                    {
        //                        if (line.Contains('$'))
        //                        {
        //                            line = line.Remove(0, 1);
        //                        }
        //                        comilla = line.Length;
        //                        coma = line.IndexOf(',');
        //                        string data = "";
        //                        if (coma < 0)
        //                        {
        //                            data = line;
        //                            line = null;
        //                        }
        //                        else
        //                        {
        //                            data = line.Substring(0, coma);
        //                            line = line.Remove(0, coma + 1);
        //                        }
        //                        medicineData.Add(data);
        //                    }
        //                    else
        //                    {
        //                        line = line.Remove(0, 1);
        //                        comilla = line.IndexOf('"');
        //                        var data = line.Substring(0, comilla);
        //                        line = line.Remove(0, comilla + 2);
        //                        medicineData.Add(data);
        //                    }
        //                }
        //            }

        //            MedicamentoExtModel nuevoMedicamento = new MedicamentoExtModel
        //            {
        //                Id = int.Parse(medicineData[0]),
        //                Nombre = medicineData[1],
        //                Descripcion = medicineData[2],
        //                CasaProd = medicineData[3],
        //                Precio = Convert.ToDouble(medicineData[4]),
        //                Existencia = int.Parse(medicineData[5])
        //            };
        //            Singleton.Instance.misMedicamentosExt.Add(nuevoMedicamento);
        //            Singleton.Instance.miArbolMedicamentos.Add(nuevoMedicamento);
        //            line = streamReader.ReadLine();
        //        }
        //        return RedirectToAction("Index", "Medicamento");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
    
    }
}
