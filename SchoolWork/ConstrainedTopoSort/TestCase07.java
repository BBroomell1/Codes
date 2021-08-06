import java.io.*;
import java.util.*;

public class TestCase07
{
	private static void failwhale(String params)
	{
		System.out.println("Test Case #7: hasConstrainedTopoSort(" + params + "): fail whale :(");
		System.exit(0);
	}

	public static void main(String [] args) throws IOException
	{
		ConstrainedTopoSort t = new ConstrainedTopoSort("g2.txt");

		if (t.hasConstrainedTopoSort(2, 1) != true) failwhale("2, 1");
		if (t.hasConstrainedTopoSort(1, 6) != false) failwhale("1, 6");
		if (t.hasConstrainedTopoSort(3, 5) != true) failwhale("3, 5");
		if (t.hasConstrainedTopoSort(1, 1) != true) failwhale("1, 1 is false");

		System.out.println("Test Case #7: PASS (Hooray!)");
	}
}
