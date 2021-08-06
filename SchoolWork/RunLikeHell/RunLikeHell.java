// Brandon Broomell
// NID br992401
// UCF ID 4414682
// COP 3503, Summer 2019

import java.io.*;
import java.util.*;


public class RunLikeHell
{
  public static int maxGain(int[] array)
  {
    // with_prev will be sum with the previous element
    // without_prev will be the sum without previous element.
    // Temp is used to hold which is greater of these two.
    int with_prev = array[0];
    int without_prev = 0;
    int size = array.length;
    int temp;

    // Start at the second element since we already added the first to with_prev.
    // Loop through entire array testing if adding the previous will be more
    // than excluding the (previous + the current).
    for (int i = 1; i < size; i++)
    {
      if (with_prev > without_prev)
      {
        temp = with_prev;
      }
      else
      {
        temp = without_prev;
      }

      // Since we are going to increment to our next value.
      // The with_prev will get the sum of including the previous of that
      // array[i + 1] value.
      // Then without_prev will get the greater of the with and without previous
      // before we increment.
      with_prev = without_prev + array[i];
      without_prev = temp;
    }

    // Return whichever total is greater after looping through entire array.
    if (with_prev > without_prev)
    {
      return with_prev;
    }
    else
    {
      return without_prev;
    }
  }

  public static double difficultyRating()
  {
    return 4.0;
  }

  public static double hoursSpent()
  {
    return 10.0;
  }
}
