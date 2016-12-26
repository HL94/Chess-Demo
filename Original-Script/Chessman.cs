using UnityEngine;
using System.Collections;

public abstract class Chessman : MonoBehaviour
    //abstract class that carries shared functions and scripts common to all chesspeices
{
    public int CurrentX { set; get; }//location of peice in x-axis
    public int CurrentY { set; get; }//location of piece in z(y)-axis
    public bool isWhite;//which color the piece is

    public void SetPosition(int x, int y)//set position of piece
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool [,] PossibleMove() //possible movements for pieces instance
    {
        return new bool[8,8]; //return an 8x8 array
    }
}
