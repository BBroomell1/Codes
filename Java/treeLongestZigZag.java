// you can also use imports, for example:
// import java.util.*;
import java.util.concurrent.atomic.AtomicInteger;

// you can write to stdout for debugging purposes, e.g.
// System.out.println("this is a debug message");

class Solution {
    public int solution(Tree T) {

        //Check if the tree is empty
       if(T == null)
       {
           return 0;

       }
       //Check if the tree has no children
       if(T.l == null && T.r == null)
       {
           return 0;
       }


       AtomicInteger leftSide = new AtomicInteger(0);
       AtomicInteger rightSide = new AtomicInteger(0);

       if(T.l != null) traverseTree(T.l, 0, true, leftSide);
       if(T.r != null) traverseTree(T.r, 0, false, rightSide);


       if(leftSide.intValue() > rightSide.intValue())
       {
           return leftSide.intValue();
       }
       else{
           return rightSide.intValue();
       }

    }

    public void traverseTree(Tree current, int length, boolean isLeft, AtomicInteger longest)
    {
        if(length > longest.intValue())
        {
            longest.set(length);
        }

        //Left node
        if(isLeft == true)
        {
            if(current.l != null)
            {
                traverseTree(current.l, length, true, longest);
            }
            if(current.r != null)
            {
                traverseTree(current.r, length+1, false, longest);
            }
        }

        //Right node
        if(isLeft == false)
        {
            if(current.l != null)
            {
                traverseTree(current.l, length+1, true, longest);
            }
            if(current.r != null)
            {
                traverseTree(current.r, length, false, longest);
            }
        }

    }
}
