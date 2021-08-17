//Brandon Broomell
//Date: 8/4/2021
//Codility challenge



using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int solution(int X, int[] A) {
        int[] tempArr = new int[X + 1];
        int len = A.Length;
        int tempCount = X;

        int i = 0;
        for(i = 0; i < len;i++)
        {
            if(tempArr[A[i]] != 1)
            {
                tempArr[A[i]] = 1;
                tempCount--;
                if(tempCount < 1)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
