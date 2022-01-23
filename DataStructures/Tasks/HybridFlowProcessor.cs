using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly IDoublyLinkedList<T> _doubleLinkedList;

        public HybridFlowProcessor()
        {
            _doubleLinkedList = new DoublyLinkedList<T>();
        }
        public T Dequeue()
        {
            try
            {
                return _doubleLinkedList.RemoveAt(0);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
        }

        public void Enqueue(T item)
        {
            _doubleLinkedList.Add(item);
        }

        public T Pop()
        {
            try
            {
                return _doubleLinkedList.RemoveAt(0);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
        }

        public void Push(T item)
        {
            _doubleLinkedList.AddAt(0, item);
        }
    }
}
