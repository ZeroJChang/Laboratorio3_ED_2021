using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomGenerics.Estructuras;

namespace CustomGenerics.Interfaces
{
    public abstract class NonLinearDataStructureBase<T> where T : IComparable<T>
    {
        protected abstract Nodo<T> Insert(Nodo<T> nodo, T value);
        protected abstract void Delete(Nodo<T> nodo);
        protected abstract Nodo<T> Get(Nodo<T> nodo, T value);
    }
}
