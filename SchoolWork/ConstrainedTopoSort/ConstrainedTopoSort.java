// Brandon Broomell
// NID br992401
// UCF ID 4414682
// COP 3503, Summer 2019

import java.io.*;
import java.util.*;

public class ConstrainedTopoSort
{
    boolean[][] adjMatrix;
    boolean[] visitedVertices;
    int[] degree;
    ArrayList<Integer> stack = new ArrayList<Integer>();
    int n;
    List<ArrayList<Integer>> allTopSorts = new ArrayList<ArrayList<Integer>>();

    // Populates adjacency matrix.
    public ConstrainedTopoSort(String filename) throws IOException
    {
      Scanner scanner = new Scanner(new File(filename));
      int temp, column, row;

      // n = number of vertices.
      n = scanner.nextInt();

      // Read in values from file into adjMatrix.
      // Increment the indegree of each vertex.
      adjMatrix = new boolean[n + 1][n + 1];
      degree = new int[n + 1];
      for (row = 1; row <= n; row++)
      {
        temp = scanner.nextInt();
        for (int j = 0; j < temp; j++)
        {
          column = scanner.nextInt();
          adjMatrix[row][column] = true;
          degree[column]++;
        }
      }
      visitedVertices = new boolean[n + 1];
      sort();
    }

    // Recursively does the topological sort.
    public void sort()
    {
      boolean flag = false;
      for (int i = 1; i <= n; i++)
      {
        // Visit vertex [i] if it hasnt been visited and its indegree is 0.
        // Add it to the stack of visited vertexes.
        if ((visitedVertices[i] == false) && (degree[i] == 0))
        {
          visitedVertices[i] = true;
          stack.add(i);
          for (int j = 1; j <= n; j++)
          {
            if (adjMatrix[i][j] == true)
            {
              degree[j]--;
            }
          }
          sort();

          // Reset this vertex to false. Remove from stack for backtracking.
          // Increase indegree on vertex.
          visitedVertices[i] = false;
          stack.remove(stack.size() - 1);
          for (int j = 1; j <= n; j++)
          {
            if (adjMatrix[i][j] == true)
            {
              degree[j]++;
            }
          }
          flag = true;
        }
      }

      // If we reached a valid topological sort.
      // Add each value to an ArrayList and add that ArrayList
      // to List allTopSorts.
      if (flag == false)
      {
        ArrayList<Integer> validSort = new ArrayList<Integer>();
        for (int i = 0; i < stack.size(); i++)
        {
          validSort.add(stack.get(i));
        }
        allTopSorts.add(validSort);
      }
    }

    // Check the ArrayList allTopSorts for a valid validSort
    // where x comes before y.
    public boolean hasConstrainedTopoSort( int x, int y)
    {
      // Loop through each topological sort. If x is found first
      // return true. If y is found first break out of current
      // topological sort.
      for (int i = 0; i < allTopSorts.size(); i++)
      {
        for (int j = 0; j < n; j++)
        {
          if (allTopSorts.get(i).get(j) == x)
          {
            return true;
          }
          else if (allTopSorts.get(i).get(j) == y)
          {
            break;
          }
        }
      }
      return false;
    }

    public static double difficultyRating()
    {
      return 3.0;
    }

    public static double hoursSpent()
    {
      return 10.0;
    }
}
