using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSAA
{

  public class SortStats
  {    
    public override string ToString()
    {
      return String.Format("compares = {0}, swaps = {1}", Compares, Swaps);
    }

    public int Compares { get; set; }
    public int Swaps { get; set; }
  }


  public static class QuickSortEx
  {
    static Random rand = new Random();
    public static void QuickSort<T>(this IList<T> items)
      where T : IComparable<T>
    {
      var stats = new SortStats();
      QuickSort(items, 0, items.Count - 1, stats);
      Console.WriteLine("QuickSort: {0}", stats);
    }

    static void InsertionSort<T>(IList<T> items, int p, int r, SortStats ss)
      where T : IComparable<T>
    {
      for (int i = p; i < r; i++)
      {
        T x = items[i];
        int j = i - 1;
        while (j >= 0 && items[j].CompareTo(x) > 0)
        {
          ss.Compares++;
          items[j + 1] = items[j];
          j--;
          ss.Swaps++;
        }
        items[j + 1] = x;
      }
    }



    static void QuickSort<T>(IList<T> A, int p, int r, SortStats ss)
      where T : IComparable<T>
    {
      if (p < r)
      {
        int q = Partition(A, p, r, ss);
        QuickSort(A, p, q - 1, ss);
        QuickSort(A, q + 1, r, ss);
      }
    }

    static void Swap<T>(IList<T> A, int a, int b)
    {
      T tmp = A[a];
      A[a] = A[b];
      A[b] = tmp;
    }

    static int Partition<T>(IList<T> A, int p, int r, SortStats ss)
       where T : IComparable<T>
    {
      T x = A[r];
      int i = p - 1;
      for (int j = p; j < r; ++j)
      {
        ss.Compares++;
        if (A[j].CompareTo(x) <= 0)
        {
          ss.Swaps++;
          Swap(A, ++i, j);
        }
      }
      ss.Swaps++;
      Swap(A, ++i, r);
      return i;
    }


    public static void QuickSort2<T>(IList<T> input)
      where T : IComparable<T>
    {
      QuickSort2(input, 0, input.Count - 1);
    }


    public static void QuickSort2<T>(IList<T> input, int start, int finish)
      where T : IComparable<T>
    {
      if (finish - start < 1)
        return;      
      int pivotIndex = (finish + start) / 2;      
      int i = start, j = finish;
      while (true)
      {
        while (input[i].CompareTo(input[pivotIndex]) < 0)
          i++;
        while (input[j].CompareTo(input[pivotIndex]) > 0)
          j--;
        if (i <= j)
        {
          T temp = input[i];
          input[i] = input[j];
          input[j] = temp;
          i++;
          j--;
        }
      }      
      QuickSort2(input, start, i - 1);
      QuickSort2(input, i + 1, finish);
    }
  }
  public static class MergeSortEx
  {
    public static void MergeSortInPlace<T>(IList<T> input)
      where T : IComparable<T>
    {
      SortStats ss = new SortStats();
      T[] scratch = new T[input.Count];
      MergeSortInPlace(input, 0, input.Count - 1, ss, scratch);
      Console.WriteLine("Mergesort: {0}", ss);
    }


    public static void MergeSortInPlace<T>(IList<T> input, int startIndex, int endIndex, SortStats ss, T[] scratch)
      where T : IComparable<T>
    {
      if (endIndex - startIndex <= 0)
        return;
      int half = (endIndex - startIndex + 1) / 2;
      MergeSortInPlace(input, startIndex, startIndex + half - 1, ss, scratch);
      MergeSortInPlace(input, startIndex + half, endIndex, ss, scratch);
      MergeInPlace(input, startIndex, startIndex + half, endIndex, ss, scratch);
    }



    private static void MergeInPlace<T>(IList<T> input, int startA, int startB, int endB, SortStats ss, T[] scratch)
      where T : IComparable<T>
    {
      var c = new T[endB - startA + 1];
      int ap = startA, bp = startB, cp = 0;
      while (cp < endB - startA + 1)
      {
        ss.Compares++;
        if (bp > endB || (ap <= (startB - 1) && (input[ap].CompareTo(input[bp]) < 0)))
        {
          c[cp] = input[ap];
          ap++;
        }
        else
        {
          c[cp] = input[bp];
          bp++;
        }
        cp++;
      }

      for (int i = startA; i <= endB; i++)
        input[i] = c[i - startA];
    }


    public static void Merge<T>(IList<T> A, int p, int q, int r, T[] tmp)
      where T : IComparable<T>
    {
      int i = p;
      int j = q + 1;
      int n = p;
      while (i < q && j < p)
      {
        if (A[i].CompareTo(A[j]) < 0)
          tmp[n++] = A[i++];
        else
          tmp[n++] = A[j++];
      }

      while (i < q)
        tmp[n++] = A[i++];

      while (j < r)
        tmp[n++] = A[j++];

      for (i = p; i < r; i++)
        A[i] = tmp[i];
    }

    static void MergeSort<T>(IList<T> A, int p, int r, T[] tmp)
      where T : IComparable<T>
    {
      if (p < r)
      {
        int q = (p + r) / 2;
        MergeSort(A, p, q, tmp);
        MergeSort(A, q + 1, r, tmp);
        Merge(A, p, q, r, tmp);
      }
    }
    public static void MergeSort<T>(this IList<T> items)
      where T : IComparable<T>, new()
    {
      T[] tmp = new T[items.Count];
      MergeSort(items, 0, items.Count - 1, tmp);
    }
  }

  public class InsertionSortEx
  {
    public static void Swap<T>(IList<T> X, int a, int b)
      where T : IComparable<T>
    {
      T t = X[a];
      X[a] = X[b];
      X[b] = t;
    }

    public static void InsertionSort<T>(IList<T> items)
      where T : IComparable<T>
    {      
      for (int i = 1; i < items.Count; i++)      
        for (int j = i; j > 0 && items[j - 1].CompareTo(items[j]) > 0; j--)
          Swap(items, j - 1, j);      
    }
  }
}
