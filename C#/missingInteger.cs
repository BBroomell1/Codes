//Brandon Broomell
//Date: 8/4/2021
//Codility challenge

/*Write a function:

class Solution { public int solution(int[] A); }

that, given an array A of N integers, returns the smallest positive integer (greater than 0) that does not occur in A.

For example, given A = [1, 3, 6, 4, 1, 2], the function should return 5.

Given A = [1, 2, 3], the function should return 4.

Given A = [−1, −3], the function should return 1.

Write an efficient algorithm for the following assumptions:

N is an integer within the range [1..100,000];
each element of array A is an integer within the range [−1,000,000..1,000,000].*/

using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int solution(int[] A) {
           Array.Sort(A);
           int len = A.Length;
           int lowest = 1;
            int i = 0;
            for(i = 0; i < len; i++)
            {
                if(A[i] > lowest)
                {
                    return lowest;
                }
                else if(A[i] == lowest){
                    lowest++;
                }
            }
            return lowest;
    }
}
