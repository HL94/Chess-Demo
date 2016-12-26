using UnityEngine;
using System.Collections;

public abstract class Chessman : MonoBehaviour
    //abstract class that carries shared functions and scripts common to all chesspeices
{
    public int X { set; get; }//location of peice in x-axis
    public int Y { set; get; }//location of piece in z(y)-axis
    public int Z { get; set; }//location of piece in y(z) - axis
    public bool isWhite;//which color the piece is

    public void SetPosition(int x, int y, int z) //set position of piece
    {
        X = x;
        Y = y;
        Z = z;
    }

    public virtual bool [,,] PossibleMove() //possible movements for pieces instance
    {
        return new bool[8,8,35]; //return an 8x8 array
    }
}
