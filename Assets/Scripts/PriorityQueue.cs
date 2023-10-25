using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<T> data;
    private Comparison<T> comparison;

    public PriorityQueue() : this(Comparer<T>.Default) { }

    public PriorityQueue(IComparer<T> comparer)
    {
        data = new List<T>();
        comparison = comparer.Compare;
    }

    public PriorityQueue(Comparison<T> comparison)
    {
        data = new List<T>();
        this.comparison = comparison;
    }

    public int Count
    {
        get { return data.Count; }
    }

    public bool Contains(T item)
    {
        return data.Contains(item);
    }

    public void Enqueue(T item)
    {


        data.Add(item);
        int childIndex = data.Count - 1;

        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (comparison(data[childIndex], data[parentIndex]) >= 0)
                break;

            Swap(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        int lastIndex = data.Count - 1;
        T frontItem = data[0];
        data[0] = data[lastIndex];
        data.RemoveAt(lastIndex);

        lastIndex--;

        int parentIndex = 0;
        while (true)
        {
            int leftChildIndex = parentIndex * 2 + 1;
            if (leftChildIndex > lastIndex)
                break;

            int rightChildIndex = leftChildIndex + 1;
            if (rightChildIndex <= lastIndex && comparison(data[leftChildIndex], data[rightChildIndex]) > 0)
            {
                leftChildIndex = rightChildIndex;
            }

            if (comparison(data[parentIndex], data[leftChildIndex]) <= 0)
                break;

            Swap(parentIndex, leftChildIndex);
            parentIndex = leftChildIndex;
        }

        return frontItem;
    }

    public T Peek()
    {
        if (data.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        return data[0];
    }

    private void Swap(int a, int b)
    {
        T temp = data[a];
        data[a] = data[b];
        data[b] = temp;
    }
}
