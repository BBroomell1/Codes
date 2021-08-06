// Brandon Broomell
// NID br992401
// UCF ID 4414682
// COP 3503, Summer 2019

import java.io.*;
import java.util.ArrayList;

public class SneakyQueens
{
    public static void main(String [] args)
    {

    }// End main.

    // temp[0] == column, temp[1] == row.
    public static boolean allTheQueensAreSafe(ArrayList<String> coordinateStrings, int boardSize)
    {
      int[] columnArray =  new int[boardSize + 1];
      int[] rowArray = new int[boardSize + 1];
      int[] diagonalArrayMain = new int[2 * (boardSize + 1)];
      int[] diagonalArraySecond = new int[2 * (boardSize + 1)];
      int[] temp = new int[2];
      int mainDiagonal, secondDiagonal;

      // Initalize all array values to 0.
      for (int i = 0; i < boardSize; i++)
      {
        columnArray[i] = 0;
        rowArray[i] = 0;
        diagonalArrayMain[i] = 0;
        diagonalArraySecond[i] = 0;
      }

      // Iterate through string array and populate board columns, rows, and diagonals.
      // Return false if any have been used, else place 1 in appropriate column, row, or diagonal.
      // Return true if no Queens can attack eachother.
      for (String s: coordinateStrings)
      {
        temp = getRowsAndColumns(s);
        if ((columnArray[temp[0]] == 1) || (rowArray[temp[1]] == 1))
        {
          return false;
        }
        else
        {
          columnArray[temp[0]] = 1;
          rowArray[temp[1]] = 1;
        }
        // Diagonals going from top left to bottom right.
        mainDiagonal = temp[0] + temp[1];
        if (diagonalArrayMain[mainDiagonal] == 1)
        {
          return false;
        }
        else
        {
          diagonalArrayMain[mainDiagonal] = 1;
        }
        // Swap row number around to get diagonals going from
        // bottom left to top right.
        secondDiagonal = temp[0] + (boardSize + 1 - temp[1]);
        if (diagonalArraySecond[secondDiagonal] == 1)
        {
          return false;
        }
        else
        {
          diagonalArraySecond[secondDiagonal] = 1;
        }
      }// End advanced for loop.
      return true;
    }// End allTheQueensAreSafe.

    // Places column in intArray[0] and row in intArray[1] and returns intArray.
    public static int[] getRowsAndColumns(String coordinateString)
    {
      int [] intArray = new int[2];
      int i = 0;
      String temp = "";
      while (('a' <= coordinateString.charAt(i)) && (coordinateString.charAt(i) <= 'z'))
      {
        temp = temp + coordinateString.charAt(i);
        i++;
      }
      intArray[0] = convertString(temp);
      temp = "";
      while (i < coordinateString.length())
      {
        temp += coordinateString.charAt(i);
        i++;
      }
      intArray[1] = Integer.parseInt(temp);
      return intArray;
    }// End getRowsAndColumns.

    // Horners rule to get column number.
    public static int convertString(String temp)
    {
      int total = 0;
      char[] charArray = temp.toCharArray();
      for (int i = 0; i < charArray.length; i++)
      {
        total *= 26;
        total += charArray[i] % 96;
      }
      return total;
    }// End convertString.

    public static double difficultyRating()
    {
      return 4.0;
    }// End difficultyRating.

    public static double hoursSpent()
    {
      return 20.0;
    }// End hoursSpent.
}// End Public Class Sneaky Queens.
