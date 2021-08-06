import java.io.*;


public class StringsTest
{
    public static void main(String [] args)
    {
      String test = "aaaaa2005400";
      System.out.println(test);

      int total = 0;
      int [] intArray = new int[2];
      int column = 0, i = 0;
      String temp = "";
      while(('a' <= test.charAt(i)) && (test.charAt(i) <= 'z'))
      {
        temp += test.charAt(i);
        i++;
      }
      column = convertString(temp);
      intArray[0] = column;
      temp = "";
      while(i < test.length())
      {
        temp += test.charAt(i);
        i++;
      }
      intArray[1] = Integer.parseInt(temp);


      System.out.println("column is " + intArray[0]);
      System.out.println("row is " + intArray[1]);



    }
    public static int convertString(String temp)
    {
      int i = 0;
      int total = 0;
      char[] charArray = temp.toCharArray();
      int length = charArray.length;

      while(i < length)
      {
        total *= 26;
        total += charArray[i] % 96;
        i++;
      }
      return total;
    }//End convertString
}
