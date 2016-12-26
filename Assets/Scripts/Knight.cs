using UnityEngine;
using System.Collections;

public class Knight : Chessman
{
    public override bool[,,] PossibleMove()//possible moves for knight piece
    {
        bool[,,] r = new bool[8, 8, 7];//create object r as possible move area on array 8x8

        //forwardleft:
        KnightMove(X - 1, Y + 2, Z, ref r);
        //forwardright:
        KnightMove(X + 1, Y + 2, Z, ref r);
        //backwardleft:
        KnightMove(X - 1, Y - 2, Z, ref r);
        //backwardright:
        KnightMove(X + 1, Y - 2, Z, ref r);
        //Rightforward:
        KnightMove(X + 2, Y + 1, Z, ref r);
        //Rightbackward:
        KnightMove(X + 2, Y - 1, Z, ref r);
        //Leftforward:
        KnightMove(X - 2, Y + 1, Z, ref r);
        //Leftbackward:
        KnightMove(X - 2, Y - 1, Z, ref r);

        return r;//return r 
    }

    public void KnightMove(int x, int y, int z, ref bool[,,] r)//knight moves, with reference to bool array r
        //takes x and y inouts, and a reference to the bool array with r
    {
        Chessman c;//declare enemy chesspiece c
        //for(int z = 0; z<8; z++)
        if (x >= 0 && x < 8 && y >= 0 && y < 8 && z >= 0 && z < 7)//if unit is inside chessboard boundaries:
        {
            c = BoardManager.Instance.Chessmans[x, y, z];//c is arbitrary chess unit on board
            if (c == null)//if c is emtpy
                r[x, y, z] = true;//r is true, unit can move on tile
            else if (isWhite != c.isWhite)//if c is not same color as unit
                r[x, y, z] = true;//unit can move to tile that c is on
        }
    }

}
