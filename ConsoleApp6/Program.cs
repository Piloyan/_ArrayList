using System;
using System.Collections;

namespace _ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            _ArrayList AL1 = new _ArrayList(0);
            AL1.Add(1);
            AL1.Add("aaa");
            AL1.Add("bbb");
            AL1.Add(-2);
            AL1.Contains("ccc");
            object[] iTI = { 1, 5, "n"};
            AL1.InsertRange(5, iTI);


        }

    }
    public class _ArrayList
    {
        private Object[] _items;
        private int _size;
        private int _version;
        private object[] _emptyArray = new object[0];
        private const int _defaultCapacity = 4;

        public _ArrayList(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException();
            if (capacity == 0)
                _items = _emptyArray;
            else
                _items = new Object[capacity];
        }
        public _ArrayList(int count, object[] _items)
        {

            if (count == 0)
            {
                _items = _emptyArray;
            }
            else
            {
                _items = new Object[count];
                AddRange();
            }

        }
        public _ArrayList(object[] emptyArray)
        {
            _emptyArray = emptyArray;
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                // We don't want to update the version number when we change the capacity.
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        Object[] newItems = new Object[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = new Object[_defaultCapacity];
                    }
                }
            }
        }
        public void AddRange()
        {
            InsertRange(_size, _items);
        }
        public void InsertRange(int index, object[] itemsToInsert)
        {

            if (index < 0 || index > _size) throw new ArgumentOutOfRangeException();

            else
            {
                EnsureCapacity(_size + Count);

                if (index < _size)
                {
                    Array.Copy(_items, index, _items, index + Count, _size - index);
                }

                Object[] iTI = new Object[Count];

                iTI.CopyTo(_items, index);
                _size += Count;
                _version++;
            }
        }
        public void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
                // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
                if ((uint)newCapacity > 0X7FEFFFFF) newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public void Add(object item)
        {
            if (_size == _items.Length)
                EnsureCapacity(_size + 1);
            _items[_size++] = item;
        }
        public bool Contains(object element)
        {
            foreach (var item in _items)
            {
                if (Equals(element, item)) return true;
            }
            return false;
        }
    }
}