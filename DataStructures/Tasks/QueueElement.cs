namespace Tasks
{
    public class QueueElement<T>
    {
        public QueueElement(T value)
        {
            Value = value;
        }

    public T Value { get; set; }

    public QueueElement<T> Previous { get; set; }

    public QueueElement<T> Next { get; set; }
    }
}
