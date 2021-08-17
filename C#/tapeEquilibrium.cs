//Brandon Broomell
//Date: 8/4/2021
//Codility challenge


using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int solution(int[] A) {
        int total = 0;
        int left = 0, right = 0;
        int lowest = 0;
        int P = 0;
        int len = A.Length;


        int i = 0;
        for(i = 0; i < len; i++)
        {
            total += A[i];
        }
        right = total - A[0];
        left = A[0];
        lowest = Math.Abs(right - left);
        for(P = 2; P < len; P++)
        {
            left += A[P-1];
            right -= A[P-1];
            if(lowest > Math.Abs(right - left))
            {
                lowest = Math.Abs(right - left);
            }
        }
        return lowest;
    }
}
