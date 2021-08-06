//Brandon Broomell
//Date: 8/4/2021
//Codility challenge

/*Write a function:

class Solution { public int solution(int A, int B, int K); }

that, given three integers A, B and K, returns the number of integers within the range [A..B] that are divisible by K, i.e.:

{ i : A ≤ i ≤ B, i mod K = 0 }

For example, for A = 6, B = 11 and K = 2, your function should return 3, because there are three numbers divisible by 2 within the range [6..11], namely 6, 8 and 10.

Write an efficient algorithm for the following assumptions:

A and B are integers within the range [0..2,000,000,000];
K is an integer within the range [1..2,000,000,000];
A ≤ B.*/

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
