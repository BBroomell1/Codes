//Brandon Broomell
//Date: 8/4/2021
//Codility challenge



using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int solution(int A, int B, int K) {
        int count= 0;
        int start = 0;

        if(A%K == 0)
        {
            count++;
            if(A==B)
            {
                return count;
            }
            start = A;
        }
        else
        {
            if(A==B)
            {
                return count;
            }
            start = (A + (K-(A%K)));
            count++;

        }
        count += (B-start)/K;
        return count;
    }
}
