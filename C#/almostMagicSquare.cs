//Brandon Broomell
//8/7/2021
//Codility Challenge

/*You are given a matrix, consisting of three rows and three columns, represented as an array A of nine integers. The rows of the matrix are numbered from 0 to 2 (from top to bottom) and the columns are numbered from 0 to 2 (from left to right). The matrix element in the J-th row and K-th column corresponds to the array element A[J*3 + K]. For example, the matrix below corresponds to array [0, 2, 3, 4, 1, 1, 1, 3, 1].

[0 2 3]
[4 1 1]
[1 3 1]

In one move you can increment any element by 1.

Your task is to find a matrix whose elements in each row and each column sum to an equal value, which can be constructed from the given matrix in a minimal number of moves.

Write a function:

class Solution { public int[] solution(int[] A); }

that, given an array A of nine integers, returns an array of nine integers, representing the matrix described above. If there are several possible answers, the function may return any of them.

Examples:

1. Given A = [0, 2, 3, 4, 1, 1, 1, 3, 1], the function could return [1, 2, 3, 4, 1, 1, 1, 3, 2]. The sum of elements in each row and each column of the returned matrix is 6. Two increments by 1 are enough. You can increment A[0] and A[8] (top-left and bottom-right matrix elements). This gives [1, 2, 3, 4, 1, 1, 1, 3, 2], which satisfies the statement's conditions. Alternatively, you can increment A[2] and A[6] (top-right and bottom-left matrix elements). This gives another correct solution: [0, 2, 4, 4, 1, 1, 2, 3, 1].

[0 2 3]
[4 1 1]
[1 3 1]

[0 2 3]
[4 1 1]
[1 3 1]

2. Given A = [1, 1, 1, 2, 2, 1, 2, 2, 1], the function should return [1, 1, 3, 2, 2, 1, 2, 2, 1]. The sum of elements in each row and each column of the returned matrix is 5. Two increments by 1 are enough. You can increment A[2] (top-right matrix element) twice. In this case, there are no other correct solutions.

[1 1 1]
[2 2 1]
[2 2 1]

Write an efficient algorithm for the following assumptions:

array A contains nine elements;
each element of array A is an integer within the range [0..100,000,000].*/

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
