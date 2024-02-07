using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MicrobytKonami.System
{
    [Serializable]
    public class LimitedList<T> : ICollection<T>
    {
        private readonly T[] _array;
        private int _count = 0;

        public int MaxSize { get; }

        public int Count => Count;

        public bool IsReadOnly => throw new global::System.NotImplementedException();

        public LimitedList(int maxSize)
        {
            _array = new T[maxSize];
            MaxSize = maxSize;
        }

        public T this[int idx] => _array[idx];

        public void Add(T item)
        {
            throw new global::System.NotImplementedException();
        }

        public void Clear()
        {
            throw new global::System.NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new global::System.NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new global::System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new global::System.NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator<T>(this);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Enumerator<T> : IEnumerator<T>
    {
        private readonly LimitedList<T> thing;
        private int index;
        internal Enumerator(LimitedList<T> thing)
        {
            this.thing = thing;
            index = 0;
            Current = default;
        }
        public void Dispose()
        {
        }
        public bool MoveNext()
        {
            var tthing = thing;
            if (index < tthing.MaxSize)
            {
                Current = tthing[index];
                index++;
                return true;
            }
            index = thing.MaxSize + 1;
            Current = default;
            return false;
        }
        public T Current { get; private set; }
        object IEnumerator.Current => Current;
        void IEnumerator.Reset()
        {
            index = 0;
            Current = default;
        }
    }
}
