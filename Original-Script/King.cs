using UnityEngine;
using System.Collections;

public class King : Chessman
{
    public override bool[,] PossibleMove()//possible moves for king
    {
        bool[,] r = new bool[8, 8];//instantiate r as bool array object

        Chessman c;//variable for enemy unit
        int i, j;//just like for bishop

        //start white king, start of board for both kings
        i = CurrentX - 1;//i set to left of king
        j = CurrentY + 1;//j set to bottom of king, to prevent out of bounds
        if (CurrentY != 7)//if unit is not on last row of board
        {
            for (int n = 0; n < 3; n++)//run forloop 3 times, diagonal left, middle, and diagonal right
            {
                if(i >= 0 || i < 8)//within chessboard boundaries
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null)//if tile is empty
                        r[i, j] = true;//can move there
                    else if (isWhite != c.isWhite)//enemy not on our team
                        r[i, j] = true;//can kill it
                }

                i++;

            }
        }

        //start Black king, end of board for both kings
        i = CurrentX - 1;
        j = CurrentY - 1;//to prevent out of bounds
        if (CurrentY != 0)//if unit is not on first row of board
        {
            for (int n = 0; n < 3; n++)//run forloop 3 times, diagonal left, middle, and diagonal right
            {
                if (i >= 0 || i < 8)//within chessboard boundaries
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null)//if tile is empty
                        r[i, j] = true;//can move there
                    else if (isWhite != c.isWhite)//enemy not on our team
                        r[i, j] = true;//can kill it
                }

                i++;

            }
        }

        //Middleleft
        if(CurrentX != 0)//if not on last column(leftside)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;//allowed movement
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        //Middleright
        if (CurrentX != 7)//if not on first column(rightside)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;//allowed movement
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }


        return r;//reutrn r
    }
}
