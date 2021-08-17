//Brandon Broomell
//Date: 8/4/2021
//Codility challenge


using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int[] solution(int N, int[] A) {
        int[] sol= new int[N];
        int high = 0, low = 0;
        int len = A.Length;

        int i = 0, j = 0;
        for(i = 0; i < len; i++)
        {
            if(A[i] == N+1)
            {
               low = high;
            }
            else
            {
                if(sol[A[i]-1] < low)
                {
                    sol[A[i]-1] = low;
                }
                sol[A[i]-1]++;
                if(high < sol[A[i]-1])
                {
                high = sol[A[i]-1];
                }
            }
        }
        for(i = 0; i < N; i++)
        {
            if(sol[i] < low)
            {
                sol[i] = low;
            }
        }
        return sol;
    }
}
