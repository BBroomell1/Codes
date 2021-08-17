//Brandon Broomell
//08-11-2021
//Codility challenge


using System;
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
        int res = 0, resTwo = 0;

        Array.Sort(A);
        res = A[len-1] * A[len-2] * A[len-3];
        resTwo = A[0] * A[1] * A[len-1];
        if(res > resTwo)
        {
            return res;
        }
        else
        {
            return resTwo;
        }
    }
}
