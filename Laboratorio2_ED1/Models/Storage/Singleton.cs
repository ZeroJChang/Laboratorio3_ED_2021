using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomGenerics.Estructuras;
using Laboratorio2_ED1.Models;

namespace Laboratorio2_ED1.Models.Storage
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        public ClienteModel miCliente = new ClienteModel();

        public List<MedicamentoExtModel> misMedicamentosExt = new List<MedicamentoExtModel>();
        public ArbolBinario<MedicamentoModel> miArbolMedicamentos = new ArbolBinario<MedicamentoModel>();
        public List<MedicamentoExtModel> miPedido = new List<MedicamentoExtModel>();

        public List<MedicamentoExtModel> miAsbastecer = new List<MedicamentoExtModel>();
    }
}
