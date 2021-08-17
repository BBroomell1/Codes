//Brandon Broomell
//8/7/2021
//Codility Challenge

using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public int[] solution(int[] A) {
        int len = A.Length;
        if(len == 0 || A == null)
        {
            return null;
        }
        int[] row = new int[3];
        int[] col = new int[3];
        int max = 0;
        int i = 0, j = 0;

    //Set  sum of rows and columns.
        for(i = 0; i < 3; i++)
        {
            row[i] = A[i*3 + 0] + A[i*3 + 1] + A[i*3 + 2];
            if(max < row[i])
            {
                max = row[i];
            }
            col[i] = A[i] + A[i+ 3] + A[i + 6];
            if(max < col[i])
            {
                max = col[i];
            }
            //Console.WriteLine("Row: " + row[i] + "Col: " + col[i]);
        }
        //Console.WriteLine(max);
        bool allSame = true;

    //Loop through each row and column looking for a space that both row and column need to be inremented if they are less than max.
        do
        {
            allSame = true;
            for(i = 0; i < 3; i++)
            {
                //Console.WriteLine("i: " + i);
                for(j = 0; j < 3; j++)
                {
                    //Console.WriteLine("j: " + j);
                    if(row[i] < max && col[j] < max)
                    {
                        allSame = false;
                        //Console.WriteLine("row: " + i + " col: " + j);
                        A[i*3 + j]++;
                        //Console.WriteLine("after increment A")
                        row[i]++;
                        col[j]++;
                    }
                }
            }
        }while(allSame == false);

    return A;
    }
}
