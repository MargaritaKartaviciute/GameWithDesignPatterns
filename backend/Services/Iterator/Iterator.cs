using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Iterator
{
    public class Iterator<T> : IIterator<T>
    {
        private readonly List<T> List;
        private readonly T[] Array;
        private readonly LinkedList<T> LinkedList;
        private readonly int structure = 0;
        private int pos = 0;

        public Iterator(List<T> elements)
        {
            this.List = elements;
            this.structure = 1;
        }

        public Iterator(T[] elements)
        {
            this.Array = elements;
            this.structure = 2;
        }

        public Iterator(LinkedList<T> elements)
        {
            this.LinkedList = elements;
            this.structure = 3;
        }

        public void Reset()
        {
            this.pos = 0;
        }

        public bool HasNext()
        {
            if (structure == 1)
            {
                return pos < List.Count && List.ElementAt(pos) != null;
            }
            else if (structure == 2)
            {
                return pos < Array.Length && Array[pos] != null;
            }
            else if (structure == 3)
            {
                return pos < LinkedList.Count() && LinkedList.ElementAt(pos) != null;
            }
            return false;
        }

        public T Next()
        {
            T element;
            if (structure == 1)
            {
                element = List.ElementAt(pos);
                pos += 1;
                return element;
            }
            else if (structure == 2)
            {
                element = Array[pos];
                pos += 1;
                return element;
            }
            else if (structure == 3)
            {
                element = LinkedList.ElementAt(pos);
                pos += 1;
                return element;
            }
            return default;
        }
    }
}
