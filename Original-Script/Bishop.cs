using UnityEngine;
using System.Collections;

public class Bishop : Chessman
{
    public override bool[,] PossibleMove()//possible moves for bishop
    {
        bool[,] r = new bool[8, 8];//instantiate r is bool object within board boundaries

        Chessman c;//non unit arbitrary chesspiece define as c
        int i, j;//instantiate i and j as integers

        //Forwardleft
        i = CurrentX;//set i to current x-axis location
        j = CurrentY;//set j to current y-axis location
        while (true)
        {
            i--;//going left
            j++;//going forward
            if (i < 0 || j >= 8)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j];//if piece is on board
            if (c == null)//nothing on c
                r[i, j] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j] = true;//we can attack it

                break;
            }
        }

        //Forwardright
        i = CurrentX;//set i to current x-axis location
        j = CurrentY;//set j to current y-axis location
        while (true)
        {
            i++;//going right
            j++;//going forward
            if (i >= 8 || j >= 8)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j];//if piece is on board
            if (c == null)//nothing on c
                r[i, j] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j] = true;//we can attack it

                break;
            }
        }

        //Backwardleft
        i = CurrentX;//set i to current x-axis location
        j = CurrentY;//set j to current y-axis location
        while (true)
        {
            i--;//going left
            j--;//going backward
            if (i < 0 || j < 0)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j];//if piece is on board
            if (c == null)//nothing on c
                r[i, j] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j] = true;//we can attack it

                break;
            }
        }

        //Backwardright
        i = CurrentX;//set i to current x-axis location
        j = CurrentY;//set j to current y-axis location
        while (true)
        {
            i++;//going right
            j--;//going backward
            if (i >= 8 || j < 0)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j];//if piece is on board
            if (c == null)//nothing on c
                r[i, j] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j] = true;//we can attack it

                break;
            }
        }


        return r;//return r
    }
}
