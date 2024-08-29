using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MicrobytKonami.System
{
    [Serializable]
    public class LimitedList<T> : ICollection<T>
    {
        [SerializeField] private T[] _array;
        [SerializeField] private int _count = 0;
        [SerializeField] private int _size;

        public int Size => _size;

        public int Count => _count;

        public bool IsReadOnly => false;

        public static implicit operator T[](LimitedList<T> list) => list._array;

        public LimitedList(int size = 0)
        {
            _array = new T[size];
            _size = size;
        }

        public T this[int index]
        {
            get
            {
                if (index > _size)
                    throw new ArgumentOutOfRangeException($"Index {index} is out of range {_size - 1}");

                return _array[index];
            }
            set
            {
                if (index > _size)
                    throw new Exception($"Index {index} is out of range {_size - 1}");

                _array[index] = value;
                if (index >= _count - 1)
                    _count = index + 1;
            }
        }

        public void Add(T item)
        {
            if (_count >= _size)
                throw new ArgumentOutOfRangeException($"{_count + 1} out of range {_size}");
            _array[_count++] = item;
        }

        public void Clear()
        {
            for (int i = 0; i < _count; i++)
            {
                var item = _array[i];

                if (item != null)
                {
                    FreeItem(item, i);
                }
            }
            _count = 0;
        }

        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < _count; i++)
                if (comparer.Equals(_array[i], item))
                    return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) => Array.Copy(_array, array, Math.Min(_count, array.Length));

        public bool Remove(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < _count; i++)
            {
                var itemDummy = _array[i];

                if (comparer.Equals(itemDummy, item))
                {
                    FreeItem(item, i);
                    if (i < Count - 1)
                        Array.Copy(_array, i + 1, _array, i, Count - 1);

                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);

        public void AddRange(T[] collection)
        {
            int _countNew = _count + collection.Length;

            if (_countNew > _size)
                throw new ArgumentOutOfRangeException($"{_countNew} out of range {_size}");

            Array.Copy(collection, 0, _array, _count, collection.Length);
            _count = _countNew;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }

        public void Resize(int size)
        {
            Array.Resize(ref _array, size);
            _size = size;
        }

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator<T>(this);

        private void FreeItem(T item, int i)
        {
            if (item is IDisposable itemDisposable)
                itemDisposable.Dispose();
            _array[i] = default(T);
        }
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
            if (index < tthing.Count)
            {
                Current = tthing[index];
                index++;
                return true;
            }
            index = thing.Count + 1;
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
