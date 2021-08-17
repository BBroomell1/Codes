//Brandon Broomell
//8/6/2021
//Codility Challenge



using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int[] solution(string S, int[] P, int[] Q) {
        int lenP = P.Length;
        int lenQ = Q.Length;
        int lenS = S.Length;
        int[] res = new int[lenP];
        int[,] prefSum = new int[3,lenS+1];
        int i;

        // Check if P and Q are the same length. if not return null;
        if(lenP != lenQ)
        {
            return null;
        }



        // Use a 2d array to get prefix sums. 0 == A, 1 == C, 2 == G.
        for(i = 0; i < lenS; i++)
        {
            if(S[i] == 'A')
            {
                prefSum[0,i+1] = prefSum[0,i] + 1;
                prefSum[1,i+1] = prefSum[1,i];
                prefSum[2,i+1] = prefSum[2,i];
            }

            if(S[i] == 'C')
            {
                prefSum[0,i+1] = prefSum[0,i];
                prefSum[1,i+1] = prefSum[1,i] + 1;
                prefSum[2,i+1] = prefSum[2,i];
            }

            if(S[i] == 'G')
            {
                prefSum[0,i+1] = prefSum[0,i];
                prefSum[1,i+1] = prefSum[1,i];
                prefSum[2,i+1] = prefSum[2,i] + 1;
            }
            if(S[i] == 'T')
            {
                prefSum[0,i+1] = prefSum[0,i];
                prefSum[1,i+1] = prefSum[1,i];
                prefSum[2,i+1] = prefSum[2,i];
            }

        }

        //Iterate through S. Use the pref sums to calculate lowest impact factor
        //and place result into res[].

        for(i = 0; i < lenP; i++)
        {
            if(prefSum[0,Q[i]+1] - prefSum[0,P[i]] > 0)
            {
                res[i] = 1;
            }
            else if(prefSum[1,Q[i]+1] - prefSum[1,P[i]] > 0)
            {
                res[i] = 2;
            }
            else if(prefSum[2,Q[i]+1] - prefSum[2,P[i]] > 0)
            {
                res[i] = 3;
            }
            else
            {
                res[i] = 4;
            }
        }

        return res;
    }
}
