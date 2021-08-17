//Brandon Broomell
//Date: 8/4/2021
//Codility challenge


using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int[] solution(int[] A, int K) {
      int len = A.Length;
      if(len == 0)
      {
        return A;
      }
      int rot = K % len;
      if(rot == 0)
      {
          return A;
      }
      int[] ans = new int[len];

      for(int i = 0; i < len; i++)
      {
          ans[(i+K) %len] = A[i];
      }

      return ans;
    }
}
