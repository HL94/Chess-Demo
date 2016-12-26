using UnityEngine;
using System.Collections;

public class Pawn : Chessman
{
    public override bool[,,] PossibleMove() //possible moves for the pawn piece
    {
        bool[,,] r = new bool[8, 8, 7]; //create object r within the chessboard array
        Chessman c, c2; //declares two chesspieces, can be any arbitrary chesspiece from board
        int[] e = BoardManager.Instance.EnPassantMove;//temporary int array e is referenced to Enpassantmove
        //r[3, 3] = true; //test r value for the pawn to move into, tile also gets highlighted
        //white team move:
        if (isWhite)
        {
            //Diagonal Left
            if(X != 0 && Y != 7) //if pawn is not at x = 0 and y = 7
            {
                if (e[0] == X - 1 && e[1] == Y + 1)//enPassant shit
                    r[X - 1, Y + 1, Z] = true;//enPassantshit
                c = BoardManager.Instance.Chessmans[X - 1, Y + 1, Z];//if a chesspiece is in front of unit
                if (c != null && !c.isWhite)//if c is not empty and is not a white piece
                {
                    r[X - 1, Y + 1, Z] = true;//is c is an enemy unit, this move is allowed
                }
            }
            //Diagonal Right
            if (X != 7 && Y != 7) //if pawn is not at x = 7 and y = 7
            {
                if (e[0] == X + 1 && e[1] == Y + 1)//enPassantshit
                    r[X + 1, Y + 1, Z] = true;//enPassantshit
                c = BoardManager.Instance.Chessmans[X + 1, Y + 1, Z];//if a chesspiece is in front of unit
                if (c != null && !c.isWhite)//if c is not empty and is not a white piece
                {
                    r[X + 1, Y + 1, Z] = true;//is c is an enemy unit, this move is allowed
                }
            }
            //Forward
            if (Y != 7) //if pawn is not on y = 7
            {
                c = BoardManager.Instance.Chessmans[X, Y + 1, Z];//if a chesspiece is in front of unit
                if (c == null) //as long as there is no piece in front of unit
                {
                    r[X, Y + 1, Z] = true;//pawn can move forward
                }
            }
            //Forward on first move
            if (Y == 1)//if pawn hasn't moved yet
            {
                c = BoardManager.Instance.Chessmans[X, Y + 1, Z];//if a chesspiece is in front of unit
                c2 = BoardManager.Instance.Chessmans[X, Y + 2, Z];//if a chesspiece is two tiles in front of unit
                if (c == null && c2 == null)//if both spaces in front of pawn are empty
                {
                    r[X, Y + 2, Z] = true;//can move two spaces 
                }
            }
        }
        else //black team pawns:
        {
            //Diagonal Left
            if (X != 0 && Y != 0) //if pawn is not at x = 0 and y = 0 since starting from other side
            {
                if (e[0] == X - 1 && e[1] == Y - 1)//enPassantshit
                    r[X - 1, Y - 1, Z] = true;//enPassantshit
                c = BoardManager.Instance.Chessmans[X - 1, Y - 1, Z];//if a chesspiece is in front of unit
                if (c != null && c.isWhite)//if c is not empty and is a white piece
                {
                    r[X - 1, Y - 1, Z] = true;//is c is an enemy unit, this move is allowed
                }
            }
            //Diagonal Right
            if (X != 7 && Y != 0) //if pawn is not at x = 7 and y = 0
            {
                if (e[0] == X + 1 && e[1] == Y - 1)//enPassantshit
                    r[X + 1, Y - 1, Z] = true;//enPassantshit
                c = BoardManager.Instance.Chessmans[X + 1, Y - 1, Z];//if a chesspiece is in front of unit
                if (c != null && c.isWhite)//if c is not empty and is a white piece
                {
                    r[X + 1, Y - 1, Z] = true;//is c is an enemy unit, this move is allowed
                }
            }
            //Forward
            if (Y != 0) //if pawn is not on y = 0
            {
                c = BoardManager.Instance.Chessmans[X, Y - 1, Z];//if a chesspiece is in front of unit
                if (c == null) //as long as there is no piece in front of unit
                {
                    r[X, Y - 1, Z] = true;//pawn can move forward
                }
            }
            //Forward on first move
            if (Y == 6)//if pawn hasn't moved yet
            {
                c = BoardManager.Instance.Chessmans[X, Y - 1, Z];//if a chesspiece is in front of unit
                c2 = BoardManager.Instance.Chessmans[X, Y - 2, Z];//if a chesspiece is two tiles in front of unit
                if (c == null && c2 == null)//if both spaces in front of pawn are empty
                {
                    r[X, Y - 2, Z] = true;//can move two spaces 
                }
            }
        }
        return r; //the possible moves of pawn unit are the tiles in r

    }
}
