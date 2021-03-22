using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratorio2_ED1.Models.Storage;

namespace Laboratorio2_ED1.Models
{
    public class MedicamentoModel : IComparable<MedicamentoModel>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int CompareTo(MedicamentoModel otro)
        {
            if (otro == null)
            {
                return 0;
            }
            else
            {
                return this.Nombre.CompareTo(otro.Nombre);
            }
        }
        public static List<MedicamentoModel> Filter(string name)
        {
            return Singleton.Instance.miArbolMedicamentos.ObtenerLista().Where(x => x.Nombre.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
