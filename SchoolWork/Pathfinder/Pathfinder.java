// Brandon Broomell
// NID br992401
// UCF ID 4414682
// COP 3503, Summer 2019


import java.io.*;
import java.util.*;

public class Pathfinder
{
	// Used to toggle "animated" output on and off (for debugging purposes).
	private static boolean animationEnabled = false;

	// "Animation" rate (frames per second).
	private static double frameRate = 4.0;

	// Animation enable and disables.
	public static void enableAnimation() { Pathfinder.animationEnabled = true; }
	public static void disableAnimation() { Pathfinder.animationEnabled = false; }
	public static void setFrameRate(double fps) { Pathfinder.frameRate = frameRate; }

	// Constant Variables
	private static final char WALL       = '#';
	private static final char PERSON     = '@';
	private static final char EXIT       = 'e';
	private static final char BREADCRUMB = '.';
	private static final char SPACE      = ' ';

	// Hashset for storing all valid paths.
	// Stringbuilder for storing each valid path before adding to validPaths
	private static HashSet<String> validPaths = new HashSet<String>();
	private static StringBuilder moveString = new StringBuilder();

	public static HashSet<String> findPaths(char [][] maze)
	{
		int height = maze.length;
		int width = maze[0].length;

		// Keeps track of the visited rows and cols visited.
		char [][] visited = new char[height][width];
		for (int i = 0; i < height; i++)
			Arrays.fill(visited[i], SPACE);

		// Find starting row and col.
		int startRow = -1;
		int startCol = -1;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (maze[i][j] == PERSON)
				{
					startRow = i;
					startCol = j;
				}
			}
		}

		findPaths(maze, visited, startRow, startCol, height, width);
		return validPaths;
	}

	// Recursively find valid paths to the exit.
	private static void findPaths(char [][] maze, char [][] visited,
	                                 int currentRow, int currentCol,
	                                 int height, int width)
	{

		// If we hit the exit restore the exit to the maze,
		// Remove the last space in moveString before
		// adding the string to the hashset validPaths.
		// Then add the space back after.
		if (visited[currentRow][currentCol] == 'e')
		{
			maze[currentRow][currentCol] = EXIT;
			moveString.delete(moveString.length() - 1, moveString.length());
			validPaths.add(moveString.toString());
			moveString.append(" ");
			return;
		}

		// moves[0] == left, moves[1] == right,
		// moves[2] == up, moves[3] == down.
		int [][] moves = new int[][] {{0, -1}, {0, 1}, {-1, 0}, {1, 0}};

		for (int i = 0; i < moves.length; i++)
		{
			int newRow = currentRow + moves[i][0];
			int newCol = currentCol + moves[i][1];

			// Check if move is valid.
			if (isLegalMove(maze, visited, newRow, newCol, height, width))
				{
					// Add direction of move to the moveString.
					if (i == 0)
					{
						moveString.append("l ");
					}
					else if (i == 1)
					{
						moveString.append("r ");
					}
					else if (i == 2)
					{
						moveString.append("u ");
					}
					else if (i == 3)
					{
						moveString.append("d ");
					}

			// If next move is the exit set the exit in the visited matrix.
			if (maze[newRow][newCol] == EXIT)
			{
				visited[newRow][newCol] = EXIT;
			}


			// Mark the current row and col of maze and visited as visited(breadcrumb).
			// Move the person to the next placement.
			maze[currentRow][currentCol] = BREADCRUMB;
			visited[currentRow][currentCol] = BREADCRUMB;
			maze[newRow][newCol] = PERSON;

			// Perform recursive descent.
			findPaths(maze, visited, newRow, newCol, height, width);


			// Resets the maze and visited current rows and cols
			// for backtracking.
			maze[currentRow][currentCol] = SPACE;
			visited[currentRow][currentCol] = SPACE;

			// Deletes the last direction that we added to moveString.
			if (moveString.length() > 1)
			{
				moveString.delete(moveString.length() - 2, moveString.length());
			}
		}
	}
}

	// Return true if next move is not a wall, breadcrumb, or out of bounds.
	private static boolean isLegalMove(char [][] maze, char [][] visited,
	                                   int row, int col, int height, int width)
	{
		if (height <= row || row < 0 || width <= col || col < 0)
		{
			return false;
		}
		if (maze[row][col] == WALL || visited[row][col] == BREADCRUMB)
		{
			return false;
		}
		else
		{
			return true;
		}

	}


	// Reads the maze from file and populates the 2d maze array.
	private static char [][] readMaze(String filename) throws IOException
	{
		Scanner in = new Scanner(new File(filename));

		int height = in.nextInt();
		int width = in.nextInt();

		char [][] maze = new char[height][];

		// Gets rid of the new line character.
		in.nextLine();

		for (int i = 0; i < height; i++)
		{
			// Read each line into the corrosponding maze row.
			maze[i] = in.nextLine().toCharArray();
		}
		return maze;
	}

	public static double difficultyRating()
	{
		return 5.0;
	}

	public static double hoursSpent()
	{
		return 20.0;
	}
}
