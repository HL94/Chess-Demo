﻿using UnityEngine;
using System.Collections;

public class Queen : Chessman
{
    public override bool[,,] PossibleMove()//last chesspiece for original chess, cutting down on comments
    {
        bool[,,] r = new bool[8, 8, 7];

        Chessman c;
        int i, j, k;

        //copied from rook:
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

        //copied from bishop:
        //Forwardleft
        i = X;//set i to current x-axis location
        j = Y;//set j to current y-axis location
        k = Z;//set k to Current z-axis location
        while (true)
        {
            i--;//going left
            j++;//going forward
            if (i < 0 || j >= 8)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j, k];//if piece is on board
            if (c == null)//nothing on c
                r[i, j, k] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j, k] = true;//we can attack it

                break;
            }
        }

        //Forwardright
        i = X;//set i to current x-axis location
        j = Y;//set j to current y-axis location
        k = Z;//set k to Current z-axis location
        while (true)
        {
            i++;//going right
            j++;//going forward
            if (i >= 8 || j >= 8)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j, k];//if piece is on board
            if (c == null)//nothing on c
                r[i, j, k] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j, k] = true;//we can attack it

                break;
            }
        }

        //Backwardleft
        i = X;//set i to current x-axis location
        j = Y;//set j to current y-axis location
        k = Z;//set k to Current z-axis location
        while (true)
        {
            i--;//going left
            j--;//going backward
            if (i < 0 || j < 0)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j, k];//if piece is on board
            if (c == null)//nothing on c
                r[i, j, k] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j, k] = true;//we can attack it

                break;
            }
        }

        //Backwardright
        i = X;//set i to current x-axis location
        j = Y;//set j to current y-axis location
        k = Z;//set k to Current z-axis location
        while (true)
        {
            i++;//going right
            j--;//going backward
            if (i >= 8 || j < 0)//if unit is outside of board boundaries
                break;

            c = BoardManager.Instance.Chessmans[i, j, k];//if piece is on board
            if (c == null)//nothing on c
                r[i, j, k] = true;//we can move there
            else
            {
                if (isWhite != c.isWhite)//if c is opposing team
                    r[i, j, k] = true;//we can attack it

                break;
            }
        }

        return r;
    }
}