// you can also use imports, for example:
// import java.util.*;

// you can write to stdout for debugging purposes, e.g.
// System.out.println("this is a debug message");

class Solution {
    public int solution(int[] A) {
        int currentIndex = 0;
        int count = 0;

        while(currentIndex != -1)
        {
            count++;
            currentIndex = A[currentIndex];
        }

        return count;
    }
}
