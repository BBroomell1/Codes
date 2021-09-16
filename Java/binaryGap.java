// you can also use imports, for example:
// import java.util.*;

// you can write to stdout for debugging purposes, e.g.
// System.out.println("this is a debug message");

class Solution {
    public int solution(int N) {
        int res = 0, i = 0, count;


        String num = Integer.toBinaryString(N);

        int len = num.length();
        if(len < 3)
        {
            return 0;
        }

        for(i = 0; i < len; i++)
        {
            if(num.charAt(i) == '1')
            {
                i++;
                if(i == len)
                {
                    return res;
                }
                count = 0;
                while(num.charAt(i) == '0')
                {
                    i++;
                    count++;
                    if(i == len)
                    {
                        return res;
                    }
                }
                if(num.charAt(i) == '1')
                {
                    if(count > res)
                    {
                        res = count;
                    }
                    i--;
                }
            }
        }
    return res;
    }
}
