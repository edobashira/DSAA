using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSAA
{
  public static class BinaryHeapEx
  {
    public static void MakeHeap<T>(this IList<T> items, bool maxheap = true)
      where T : IComparable<T>
    {
      for (int i = items.Count / 2; i >= 0; i--)
        HeapifyDown(items, items.Count, i, maxheap);
    }

    public static void PushHeap<T>(this IList<T> items, T item, bool maxheap = true)
      where T : IComparable<T>
    {
      items.Add(item);
      HeapifyUp(items, items.Count - 1, maxheap);
    }

    public static T PopHeap<T>(this IList<T> items, bool maxheap = true)
      where T : IComparable<T>
    {
      T t = items[0];
      if (items.Count > 0)
      {
        items[0] = items.Last();
        items.RemoveAt(items.Count - 1);
        HeapifyDown(items, 0, maxheap);
      }
      return t;
    }

    public static void HeapSort<T>(this IList<T> items, bool maxheap = true)
      where T : IComparable<T>
    {
      for (int i = items.Count - 1; i > 0; i--)
      {
        Swap(items, 0, i);
        HeapifyDown(items, i, 0, maxheap);
      }
    }

    public static bool IsHeap<T>(this IList<T> items, bool maxheap = true)
      where T : IComparable<T>
    {
      bool b = true;
      if (items.Count > 0)
        b = IsHeap(items, 0, maxheap);
      return b;
    }

    public static void HeapToDot<T>(IList<T> items, String path)
    {
      using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
      {
        HeapToDot(items, fs);
      }
    }

    public static void HeapToDot<T>(IList<T> items, Stream stream)
    {
      using (StreamWriter sw = new StreamWriter(stream))
      {
        sw.WriteLine("digraph BST {");
        sw.WriteLine("node [fontname=\"Arial\"];");
        HeapToDot(items, 0, sw);
        sw.WriteLine("}");
      }
    }

    static bool IsHeap<T>(IList<T> items, int i, bool maxheap)
      where T : IComparable<T>
    {
      int l = LeftChild(i);
      int r = RightChild(i);
      if (l < items.Count)
        if (Better(items[l], items[i], maxheap))
          return false;
        else
          return IsHeap(items, l, maxheap);

      if (r < items.Count)
        if (Better(items[l], items[i], maxheap))
          return false;
        else
          return IsHeap(items, r, maxheap);
      return true;
    }

    static void HeapToDot<T>(IList<T> items, int i, StreamWriter sw)
    {
      sw.WriteLine(" {0} [label=\"{1}\"];", i, items[i]);
      int l = LeftChild(i);
      if (l < items.Count)
      {
        sw.WriteLine("{0} -> {1};", i, l);
        HeapToDot(items, l, sw);
      }

      int r = RightChild(i);
      if (r < items.Count)
      {
        sw.WriteLine("{0} -> {1};", i, r);
        HeapToDot(items, r, sw);
      }
    }

    static void Swap<T>(IList<T> items, int a, int b)
    {
      T tmp = items[a];
      items[a] = items[b];
      items[b] = tmp;
    }

    static int Parent(int i)
    {
      return (i - 1) / 2;
    }

    static int LeftChild(int i)
    {
      return i * 2 + 1;
    }

    static int RightChild(int i)
    {
      return i * 2 + 2;
    }

    static bool Better<T>(T x, T y, bool maxheap)
      where T : IComparable<T>
    {
      int c = x.CompareTo(y);
      return maxheap ? c > 0 : c < 0;
    }

    //
    static void HeapifyUp<T>(IList<T> items, int i, bool maxheap)
      where T : IComparable<T>
    {
      int p = Parent(i);
      if (p == -1)
        return;
      if (Better(items[i], items[p], maxheap))
      {
        Swap(items, i, p);
        HeapifyUp(items, p, maxheap);
      }
    }

    //
    static void HeapifyDown<T>(IList<T> items, int n, int i, bool maxheap)
      where T : IComparable<T>
    {
      int l = LeftChild(i);
      int r = RightChild(i);
      int largest = i;
      if (l < n && Better(items[l], items[i], maxheap))
        largest = l;
      if (r < n && Better(items[r], items[largest], maxheap))
        largest = r;
      if (largest != i)
      {
        Swap(items, i, largest);
        HeapifyDown(items, n, largest, maxheap);
      }
    }

    static void HeapifyDown<T>(IList<T> items, int i, bool maxheap)
      where T : IComparable<T>
    {
      HeapifyDown(items, items.Count, i, maxheap);
    }
  }

  class BinaryHeap<T>
    where T : IComparable<T>
  {
    bool _maxheap;
    List<T> _items = new List<T>();

    public BinaryHeap(bool maxheap = true)
    {
      _maxheap = maxheap;
    }

    public BinaryHeap(IEnumerable<T> collection, bool maxheap = true)
    {
      _items.AddRange(collection);
      _maxheap = maxheap;
      BinaryHeapEx.MakeHeap(_items, _maxheap);
    }

    public T Peek()
    {
      return _items.First();
    }

    public void Enqueue(T item)
    {
      BinaryHeapEx.PushHeap(_items, item, _maxheap);
    }

    public T Dequeue()
    {
      return BinaryHeapEx.PopHeap(_items, _maxheap);
    }

    public int Count { get { return _items.Count; } }
  }
}
