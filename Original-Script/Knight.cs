using UnityEngine;
using System.Collections;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()//possible moves for knight piece
    {
        bool[,] r = new bool[8, 8];//create object r as possible move area on array 8x8

        //forwardleft:
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);
        //forwardright:
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);
        //backwardleft:
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);
        //backwardright:
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);
        //Rightforward:
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);
        //Rightbackward:
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);
        //Leftforward:
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);
        //Leftbackward:
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;//return r 
    }

    public void KnightMove(int x, int y, ref bool[,] r)//knight moves, with reference to bool array r
        //takes x and y inouts, and a reference to the bool array with r
    {
        Chessman c;//declare enemy chesspiece c
        if (x >= 0 && x < 8 && y >= 0 && y < 8)//if unit is inside chessboard boundaries:
        {
            c = BoardManager.Instance.Chessmans[x, y];//c is arbitrary chess unit on board
            if (c == null)//if c is emtpy
                r[x, y] = true;//r is true, unit can move on tile
            else if (isWhite != c.isWhite)//if c is not same color as unit
                r[x, y] = true;//unit can move to tile that c is on
        }
    }

}
