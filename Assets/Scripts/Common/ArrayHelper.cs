using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace GameName.Common
{
  public static class ArrayHelper
  {
    public static T Find<T>(this T[] array, Func<T, bool> condition)
    {
      for (int i = 0; i < array.Length; i++)
      {
        if (condition(array[i]))
        {
          return array[i];
        }
      }
      return default(T);
    }

    public static T[] FindAll<T>(this T[] array, Func<T, T> condition)
    {
      List<T> result = new List<T>();

      for (int i = 0; i < array.Length; i++)
      {
        if (condition(array[i]) != null)
        {
          result.Add(array[i]);
        }
      }

      return result.ToArray();
    }

    public static Q[] Select<T, Q>(this T[] array, Func<T, Q> condition)
    {
      Q[] result = new Q[array.Length];
      for (int i = 0; i < array.Length; i++)
      {
        result[i] = condition(array[i]);
      }
      return result;
    }

    public static T GetMin<T, U>(this T[] array, Func<T, U> condition) where U : IComparable<U>
    {
      if (array == null || array.Length == 0)
      {
        throw new ArgumentException("Array is empty or null");
      }

      T min = array[0];
      U minValue = condition(min);

      for (int i = 1; i < array.Length; i++)
      {
        U value = condition(array[i]);
        if (value.CompareTo(minValue) < 0)
        {
          min = array[i];
          minValue = value;
        }
      }

      return min;
    }
  }


}