// you can also use imports, for example:
// import java.util.*;

// you can write to stdout for debugging purposes, e.g.
// System.out.println("this is a debug message");

class Solution {
    public int solution(int K, int[] A) {
        int count = 0;
        int len = A.length;
        int i, j;

        if(len < 2)
        {
            return 0;
        }

        for(i = 0; i < len; i++)
        {
            for(j = i; j < len; j++)
            {
                if(Math.abs(max(A, i, j)-min(A, i, j)) <= K)
                {
                    count++;
                    if(count == 1000000000)
                    {
                        return count;
                    }
                }
            }
        }


        return count;


    }

    int max(int[] A, int start, int stop)
    {
        int max = A[start];
        int i = start;
        for(i = start; i <= stop; i++)
        {
            if(A[i] > max)
            {
                max = A[i];
            }
        }
        return max;

    }

    int min(int[] A, int start, int stop)
    {
        int min = A[start];
        int i = start;
        for(i = start; i <= stop; i++)
        {
            if(A[i] < min)
            {
                min = A[i];
            }
        }
        return min;
    }
}

//int value = Math.abs(n);
