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
        int len = A.Length;
        int i;

        Array.Sort(A);
        for(i = 0; i < len; i++)
        {
            if(A[i] != i+1)
            {
                return i+1;
            }
        }
        return i + 1;
}
}
