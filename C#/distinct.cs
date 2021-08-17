//Brandon Broomell
//08-11-2021
//Codility challenge



using System;
using System.Collections;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int solution(int[] A) {
        int len = A.Length;
        if(0 == len || null == A)
        {
            return 0;
        }
        Array.Sort(A);

        int i, distVal = 1;

        for(i = 1; i < len; i++)
        {
            if(A[i] != A[i-1])
            {
                distVal++;
            }
        }
        return distVal;
    }
}
