﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio2_ED1.Models
{
    public class MedicamentoExtModel : MedicamentoModel
    {
        public string Descripcion { get; set; }
        public string CasaProd { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
    }
}
