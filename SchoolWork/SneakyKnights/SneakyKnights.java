// Brandon Broomell
// NID br992401
// UCF ID 4414682
// COP 3503, Summer 2019

import java.io.*;
import java.util.ArrayList;
import java.util.HashMap;

public class SneakyKnights
{

  // Create HashMap using columns as keys and rows as values.
  // For each knight check if any possible moves contain a knight already.
  public static boolean allTheKnightsAreSafe(ArrayList<String> coordinateStrings, int boardSize)
  {

    HashMap<Integer, Integer> board = new HashMap<Integer, Integer>();
    int[] temp = new int[2];
    int tempRow, tempCol;

    for (String s: coordinateStrings)
    {
      temp = getRowsAndColumns(s);
      tempCol = temp[0];
      tempRow = temp[1];

      board.put(tempCol, tempRow);

      // Check the knights possible moves and check if those coordinates
      // are in the hashmap already and return false if they are.
      if (board.containsKey(tempCol + 2))
      {
        if (tempRow + 1 == board.get(tempCol + 2) ||
        tempRow - 1 == board.get(tempCol + 2))
        {
          return false;
        }
      }

      if (board.containsKey(tempCol + 1))
      {
        if (tempRow + 2 == board.get(tempCol + 1) ||
        tempRow - 2 == board.get(tempCol + 1))
        {
          return false;
        }
      }

      if (board.containsKey(tempCol - 1))
      {
        if (tempRow + 2 == board.get(tempCol - 1) ||
        tempRow - 2 == board.get(tempCol - 1))
        {
          return false;
        }
      }

      if (board.containsKey(tempCol - 2))
      {
        if (tempRow + 1 == board.get(tempCol - 2) ||
        tempRow - 1 == board.get(tempCol - 2))
        {
          return false;
        }
      }
    }
    return true;
  }

  // Seperate the letters from numbers and pass letters to convertString.
  // temp[0] is columns retrieved from convertString method
  // temp[1] is rows retrieved from the remaining numbers in the coordinateString
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
    intArray[1] = (Integer.parseInt(temp));
    return intArray;
  }

  // Convert the lowercase letters of String temp
  // to a number and return.
  public static int convertString(String temp)
  {
    int total = 0;
    char[] charArray = temp.toCharArray();
    for (int i = 0; i < charArray.length; i++)
    {
      total *= 26;
      total += charArray[i] % 96;
    }
    return (total);
  }

  // Difficulty rating and hours spent on assignment.
  public static double difficultyRating()
  {
    return 4.0;
  }
  public static double hoursSpent()
  {
    return 25.0;
  }
}
