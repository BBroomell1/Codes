// you can also use imports, for example:
// import java.util.*;

// you can write to stdout for debugging purposes, e.g.
// System.out.println("this is a debug message");

class Solution {
    public int[] solution(int[] A, int K) {
        int len = A.length;
        if(len == 0 || (K%len) == 0)
        {
            return A;
        }

        int[] res = new int[len];

        int i;
        for(i = 0; i < len; i++)
        {
            res[(i+K) % len] = A[i];
        }


        return res;
    }
}
