using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        public int Length { get; set; }

        public QueueElement<T> Front { get; set; }
        public QueueElement<T> Back { get; set; }

        public void Add(T e)
        {
            var node = new QueueElement<T>(e);
            if (Back == null)
            {
                Front = node;
            }
            else
            {
                node.Previous = Back;
                Back.Next = node;
            }
            Back = node;
            Length++;
        }

        public void AddAt(int index, T i)
        {
            var found = Find(index);
            if (found != null)
            {
                var node = new QueueElement<T>(i);
                if (found == Front)
                {
                    found.Previous = node;
                    node.Next = found;
                    Front = node;
                }
                else
                {
                    node.Next = found;
                    node.Previous = found.Previous;
                    found.Previous.Next = node;
                    found.Previous = node;
                }
                Length++;
            }
            else
            {
                Add(i);
            }
        }

        public T ElementAt(int index)
        {
            var found = Find(index);
            return found != null ? found.Value : throw new IndexOutOfRangeException();
        }

        public void Remove(T item)
        {
            var index = GetIndex(item);
            if (index != -1)
            {
                RemoveAt(index);
            }
        }

        public T RemoveAt(int index)
        {
            var found = Find(index);
            if (found != null)
            {
                if (found == Back)
                {
                    Back = found.Previous;
                }
                else
                {
                    found.Next.Previous = found.Previous;
                }

                if (found == Front)
                {
                    Front = found.Next;
                }
                else
                {
                    found.Previous.Next = found.Next;
                }
                var value = found.Value;
                Length--;
                return value;
            }
            throw new IndexOutOfRangeException();
        }


        public DoublyLinkedListEnumerator GetEnumerator()
        {
            return new DoublyLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        private QueueElement<T> Find(int index)
        {
            var counter = 0;
            using var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (counter == index)
                {
                    return enumerator.CurrentElement;
                }
                counter++;
            }
            return null;
        }

        private int GetIndex(T value)
        {
            var counter = 0;
            using var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                var currentValue = enumerator.Current;
                if (currentValue.Equals(value))
                {
                    return counter;
                }
                counter++;
            }
            return -1;
        }

        public struct DoublyLinkedListEnumerator : IEnumerator<T>
        {
            private readonly DoublyLinkedList<T> _doubleLinkedList;
            private QueueElement<T> _currentElement;

            public DoublyLinkedListEnumerator(DoublyLinkedList<T> doubleLinedList)
            {
                _doubleLinkedList = doubleLinedList;
                _currentElement = null;
            }

            public bool MoveNext()
            {
                if (_currentElement == null)
                {
                    _currentElement = _doubleLinkedList.Front;
                }
                else
                {
                    _currentElement = _currentElement.Next;
                }
                return _currentElement != null;
            }

            public void Reset()
            {
                _currentElement = _doubleLinkedList.Front;
            }

            public T Current => _currentElement.Value;

            public QueueElement<T> CurrentElement => _currentElement;

            object? IEnumerator.Current => Current;

            public void Dispose()
            {

            }
        }
    }
}
