using UnityEngine;
using System.Collections;

public class Rook : Chessman
{
    public override bool[,,] PossibleMove() //possible moves for rook piece
    {
        bool[,,] r = new bool[8, 8, 7];//create rook bool instance r within chessboard
        Chessman c;//an arbitrary chesspiece c on chessboard that is not r (the unit selected)
        int i;//initialize arbitrary integer i

        //Right move:
        i = X;//set i to curent x value
        while (true) //while i = currentx is true
        {
            i++;//increment i
            if (i >= 8)//while i is bigger or equal to 8, more then 8 tiles from start in x-axis

                break;//break while loop

            c = BoardManager.Instance.Chessmans[i, Y, Z];//if object c is on i and on current y-axis
            if (c == null)//if this c is empty
                r[i, Y, Z] = true;//we can move to position
            else //if there is something in c
            {
                if (c.isWhite != isWhite)//if c is not white while white team is in play
                    r[i, Y, Z] = true;//we can move to position
                break;//if c is a piece on same team, break
            }
        }
            //Left move:
            i = X;//set i to curent x value
            while (true) //while i = currentx is true
            {
                i--;//decrement i
                if (i < 0)//while i is smaller than 0, outside of bounds
                
                    break;//break while loop

                    c = BoardManager.Instance.Chessmans[i, Y, Z];//if object c is on i and on current y-axis
                    if (c == null)//if this c is empty
                        r[i, Y, Z] = true;//we can move to position
                    else //if there is something in c
                    {
                        if (c.isWhite != isWhite)//if c is not white while white team is in play
                            r[i, Y, Z] = true;//we can move to position
                        break;//if c is a piece on same team, break
             }
        }
        //Forward move:
        i = Y;//set i to curent y value
        while (true) //while i = currentx is true
        {
            i++;//increment i
            if (i >= 8)//while i is greater or equal to 8, outside of bounds

                break;//break while loop

            c = BoardManager.Instance.Chessmans[X, i, Z];//if object c is on same x-axis and on i
            if (c == null)//if this c is empty
                r[X, i, Z] = true;//we can move to position
            else //if there is something in c
            {
                if (c.isWhite != isWhite)//if c is not white while white team is in play
                    r[X, i, Z] = true;//we can move to position
                break;//if c is a piece on same team, break
            }
        }
        //Backward move:
        i = Y;//set i to curent y value
        while (true) //while i = currentx is true
        {
            i--;//decrement i
            if (i < 0)//while i is less then 0, outside of bounds

                break;//break while loop

            c = BoardManager.Instance.Chessmans[X, i, Z];//if object c is on same x-axis and on i
            if (c == null)//if this c is empty
                r[X, i, Z] = true;//we can move to position
            else //if there is something in c
            {
                if (c.isWhite != isWhite)//if c is not white while white team is in play
                    r[X, i, Z] = true;//we can move to position
                break;//if c is a piece on same team, break
            }
        }
        return r;//return r at end of function
    }
}
