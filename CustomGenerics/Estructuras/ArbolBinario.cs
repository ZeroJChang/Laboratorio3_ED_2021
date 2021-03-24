using System;
using System.Collections.Generic;
using System.Text;
using CustomGenerics.Interfaces;

namespace CustomGenerics.Estructuras
{
    public class ArbolBinario<T> : NonLinearDataStructureBase<T> where T : IComparable<T>
    {
        private Nodo<T> Raiz = new Nodo<T>();
        private Nodo<T> temp = new Nodo<T>();
        private List<T> listaOrdenada = new List<T>();

        public void Add(T value)
        {
            Insert(Raiz, value);
        }
        // Metodo recursivo para incertar segun orden alfabetico
        protected override Nodo<T> Insert(Nodo<T> nodo, T value)
        {
            try
            {
                if (nodo.Valor == null)
                {
                    nodo.Valor = value;
                    nodo.Derecho = new Nodo<T>();
                    nodo.Izquierdo = new Nodo<T>();
                }
                else if (value.CompareTo(nodo.Valor) == -1)
                {
                    nodo.Izquierdo = Insert(nodo.Izquierdo, value);
                    nodo = BalancearNodo(nodo);
                }
                else if (value.CompareTo(nodo.Valor) == 1)
                {
                    nodo.Derecho = Insert(nodo.Derecho, value);
                    nodo = BalancearNodo(nodo);
                }
                return nodo;
            }
            catch
            {
                throw;
            }
        }
        public T Remove(T deleted)
        {
            Nodo<T> busc = new Nodo<T>();
            busc = Get(Raiz, deleted);
            if (busc != null)
            {
                Delete(busc);
            }
            return deleted;
        }
        protected override void Delete(Nodo<T> nodo)
        {
            if (nodo.Izquierdo.Valor == null && nodo.Derecho.Valor == null) // Caso 1
            {
                nodo.Valor = nodo.Derecho.Valor;
            }
            else if (nodo.Derecho.Valor == null) // Caso 2
            {
                nodo.Valor = nodo.Izquierdo.Valor;
                nodo.Derecho = nodo.Izquierdo.Derecho;
                nodo.Izquierdo = nodo.Izquierdo.Izquierdo;
            }
            else // Caso 3
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    temp = Derecha(nodo.Izquierdo);
                }
                else
                {
                    temp = Derecha(nodo);
                }
                nodo.Valor = temp.Valor;
            }
            BalancearNodoDelete(Raiz);
        }
        // Metodo ayuda para el caso 3 de eliminacion
        private Nodo<T> Derecha(Nodo<T> nodo)
        {
            if (nodo.Derecho.Valor == null)
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    return Derecha(nodo.Izquierdo);
                }
                else
                {
                    Nodo<T> temporal = new Nodo<T>();
                    temporal.Valor = nodo.Valor;
                    nodo.Valor = nodo.Derecho.Valor;
                    return temporal;
                }
            }
            else
            {
                return Derecha(nodo.Derecho);
            }
        }
        // Busqueda recursiva de un valor dentro del arbol
        protected override Nodo<T> Get(Nodo<T> nodo, T value)
        {
            if (value.CompareTo(nodo.Valor) == 0)
            {
                return nodo;
            }
            else if (value.CompareTo(nodo.Valor) == -1)
            {
                if (nodo.Izquierdo.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Izquierdo, value);
                }
            }
            else
            {
                if (nodo.Derecho.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Derecho, value);
                }
            }
        }
        public T Buscar(T buscado)
        {
            Nodo<T> busc = new Nodo<T>();
            busc = Get(Raiz, buscado);
            if (busc == null)
            {
                return temp.Valor;
            }
            else
            {
                return busc.Valor;
            }
        }

        //Altura del nodo
        private int GetAltura(Nodo<T> nodo)
        {
            if (nodo == null) return -1;
            var IzquierdoH = GetAltura(nodo.Izquierdo);
            var rightH = GetAltura(nodo.Derecho);
            return Math.Max(IzquierdoH, rightH) + 1;
        }

        private int FactorEquilibrio(Nodo<T> nodoActual)
        {
            int iz = GetAltura(nodoActual.Izquierdo);
            int der = GetAltura(nodoActual.Derecho);
            int factorE = iz - der;
            return factorE;
        }

        public List<T> ObtenerLista()
        {
            listaOrdenada.Clear();
            InOrder(Raiz);
            return listaOrdenada;
        }

        private Nodo<T> BalancearNodo(Nodo<T> nodoActual)
        {
            int factorE = FactorEquilibrio(nodoActual);
            if (factorE > 1)
            {
                if (FactorEquilibrio(nodoActual.Izquierdo) > 0)
                {
                    nodoActual = RotacionDer(nodoActual);
                }
                else
                {
                    nodoActual = RotacionDobleDer(nodoActual);
                }
            }
            else if (factorE < -1)
            {
                if (FactorEquilibrio(nodoActual.Derecho) > 0)
                {
                    nodoActual = RotacionDobleIzq(nodoActual);
                }
                else
                {
                    nodoActual = RotacionIzq(nodoActual);
                }
            }
            return nodoActual;
        }

        //Balanceo de la eliminacion
        private void BalancearNodoDelete(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                BalancearNodoDelete(nodo.Izquierdo);
                BalancearNodoDelete(nodo.Derecho);
                BalancearNodo(nodo);
            }
        }

        private Nodo<T> RotacionDer(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Izquierdo.Valor,
                Izquierdo = nodoActual.Izquierdo.Izquierdo,
                Derecho = nodoActual.Izquierdo.Derecho
            };
            nodoActual.Izquierdo = temp.Derecho;
            temp.Derecho = nodoActual;

            if (nodoActual.Valor.CompareTo(Raiz.Valor) == 0)
            {
                Raiz = temp;
            }
            return temp;
        }

        private Nodo<T> RotacionIzq(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Derecho.Valor,
                Izquierdo = nodoActual.Derecho.Izquierdo,
                Derecho = nodoActual.Derecho.Derecho
            };
            nodoActual.Derecho = temp.Izquierdo;
            temp.Izquierdo = nodoActual;

            if (nodoActual.Valor.CompareTo(Raiz.Valor) == 0)
            {
                Raiz = temp;
            }
            return temp;
        }
        //Rotaciones dobles
        private Nodo<T> RotacionDobleDer(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Izquierdo.Valor,
                Izquierdo = nodoActual.Izquierdo.Izquierdo,
                Derecho = nodoActual.Izquierdo.Derecho
            };
            nodoActual.Izquierdo = RotacionIzq(temp);
            return RotacionDer(nodoActual);
        }
        private Nodo<T> RotacionDobleIzq(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Derecho.Valor,
                Izquierdo = nodoActual.Derecho.Izquierdo,
                Derecho = nodoActual.Derecho.Derecho
            };
            nodoActual.Derecho = RotacionDer(temp);
            return RotacionIzq(nodoActual);
        }
        // Recorre la lista en orden y agrega los valores a la listaOrdenada
        private void InOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                InOrder(nodo.Izquierdo);
                listaOrdenada.Add(nodo.Valor);
                InOrder(nodo.Derecho);
            }
        }


        public List<T> ObtenerListaPre()
        {
            listaOrdenada.Clear();
            PreOrder(Raiz);
            return listaOrdenada;
        }


        private void PreOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                listaOrdenada.Add(nodo.Valor);
                PreOrder(nodo.Izquierdo);
                PreOrder(nodo.Derecho);
            }
        }


        public List<T> ObtenerListaPost()
        {
            listaOrdenada.Clear();
            PostOrder(Raiz);
            return listaOrdenada;
        }

        private void PostOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                PostOrder(nodo.Izquierdo);
                PostOrder(nodo.Derecho);
                listaOrdenada.Add(nodo.Valor);
                
                
            }
        }


    }
}
