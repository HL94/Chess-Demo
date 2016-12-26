using UnityEngine;
using System.Collections;

public class King : Chessman
{
    public override bool[,,] PossibleMove()//possible moves for king
    {
        bool[,,] r = new bool[8, 8, 7];//instantiate r as bool array object

        Chessman c;//variable for enemy unit
        int i, j, k;//just like for bishop

        //start white king, start of board for both kings
        i = X - 1;//i set to left of king
        j = Y + 1;//j set to bottom of king, to prevent out of bounds
        k = Z;
        if (Y != 7)//if unit is not on last row of board
        {
            for (int n = 0; n < 3; n++)//run forloop 3 times, diagonal left, middle, and diagonal right
            {
                if(i >= 0 || i < 8)//within chessboard boundaries
                {
                    c = BoardManager.Instance.Chessmans[i, j, k];
                    if (c == null)//if tile is empty
                        r[i, j, k] = true;//can move there
                    else if (isWhite != c.isWhite)//enemy not on our team
                        r[i, j, k] = true;//can kill it
                }

                i++;

            }
        }

        //start Black king, backward, backward-diagonal movement
        i = X - 1;
        j = Y - 1;//to prevent out of bounds
        k = Z;
        if (Y != 0)//if unit is not on first row of board
        {
            for (int n = 0; n < 3; n++)//run forloop 3 times, diagonal left, middle, and diagonal right
            {
                if (i >= 0 || i < 8)//within chessboard boundaries
                {
                    c = BoardManager.Instance.Chessmans[i, j, k];
                    if (c == null)//if tile is empty
                        r[i, j, k] = true;//can move there
                    else if (isWhite != c.isWhite)//enemy not on our team
                        r[i, j, k] = true;//can kill it
                }

                i++;

            }
        }

        //Middleleft
        if(X != 0)//if not on last column(leftside)
        {
            c = BoardManager.Instance.Chessmans[X - 1, Y, Z];
            if (c == null)
                r[X - 1, Y, Z] = true;//allowed movement
            else if (isWhite != c.isWhite)
                r[X - 1, Y, Z] = true;
        }

        //Middleright
        if (X != 7)//if not on first column(rightside)
        {
            c = BoardManager.Instance.Chessmans[X - 1, Y, Z];
            if (c == null)
                r[X + 1, Y, Z] = true;//allowed movement
            else if (isWhite != c.isWhite)
                r[X + 1, Y, Z] = true;
        }


        return r;//reutrn r
    }
}
