//Brandon Broomell
//Date: 8/4/2021
//Codility challenge



using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int solution(int X, int Y, int D) {
        int ans = 0;
        int temp = Y-X;
        ans = temp/D;
        if(temp%D != 0)
        {
            ans++;
        }
return ans;
    }
}
