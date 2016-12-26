using UnityEngine;
using System.Collections;
using System.Collections.Generic;//for adding units to game

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; } //static board object used with boardhighlights to tell info to pieces about position
    private bool [,] allowedMoves { set; get; } //multidimensional bool array object used to direct allowed moves

    public Chessman[,] Chessmans { set; get; } //the main chessboard, selecting units by tile based on x and y coordinates
    //chessmans is the new multidimensional array (main chessboard)
    private Chessman selectedChessman; //variable that is the selected chess piece

    private const float TILE_SIZE = 1.0f; //Tile onject creation of size 1 unit
    private const float TILE_OFFSET = 0.5f; //Tile offset from center

    private int selectionX = -1; //current selection of tile in x axis
    private int selectionY = -1; //current selection of tile in y axis

    public List<GameObject> chessmanPrefabs;//list of chess pieces as set in Unity
    private List<GameObject> activeChessman;//list of active chess pieces on field

    private Material previousMat;//material tile for unit selection
    public Material selectedMat;//material that is selected on unit

    public int[] EnPassantMove { set; get; } //useless move but needed for fixing promotion glitch

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);//changes the facing direction of pieces

    public bool isWhiteTurn = true; //isWhiteTurn is initialized and set to true, starting team is white team

    private void Start()//generalized function to call all other functions in class
    {
        Instance = this;//fix for unit move glitch(pawn), instance is reference to tile that is highlighted
        SpawnAllChessmans();//call function to spawn all chesspieces
    }

    private void Update()//function update per frame
    {
        UpdateSelection ();//function calls method: UpdateSelection
        DrawChessboard ();//fuction calls method: DrawChessboard.

        //Input.GetMouseButtonDown(0) is left mouse button click instance
        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0) //is what the is mouse clicking a unit on board?
            {
                if(selectedChessman == null)//if we haven't selected anything yet
                {
                    //select the chessman
                    SelectChessman(selectionX, selectionY);//calls SelectChessman method

                }
                else
                {
                    //Move the chessman
                    MoveChessman(selectionX, selectionY);//calls MoveChessman method
                }
            }
        }
    } 

    private void SelectChessman(int x, int y) //method to select chess piece 
    {
        if (Chessmans[x, y] == null) //if there is no unit where clicked
            return;//return nothing

        if (Chessmans[x, y].isWhite != isWhiteTurn)//if trying to pick a black unit on white unit turn
            return;//return nothing

        bool hasAtleastOneMove = false;//create variable to check if unit is blocked or not

        allowedMoves = Chessmans[x, y].PossibleMove();//allowed moves only true if within the 8x8 array

        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMoves[i, j])//if unit has allowed moves
                    hasAtleastOneMove = true;//unit is not blocked!
        //the main part of method that determine which unit us selected:
        if (!hasAtleastOneMove)
            return;
        selectedChessman = Chessmans[x, y];//selected unit is referenced to unit on board
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material; //previousMat gets the material of selcted unit
        selectedMat.mainTexture = previousMat.mainTexture;//sets selectedMat to the texture of previousMat
        selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;//changes the material of unit to selectedMat
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves); //highlights the allowed moves/Tiles

    }

    private void MoveChessman(int x, int y) //method to move chesspiece after being clicked, where movement actually happens
    {
        if(allowedMoves[x, y]) //if selected unit has made a possible move:
        {
            //allow units to kill other pieces:
            Chessman c = Chessmans[x, y];//arbitrary chesspeice c is refered to board
            if (c != null && c.isWhite != isWhiteTurn)//is c is not-empty and is white team on black team turn
            {
                //capture (kill) a piece:

                //if it is the king:
                if (c.GetType() == typeof(King))//if c is the king unit of other team
                {
                    //end the game
                    EndGame();//ends the game function
                    return;
                }
                activeChessman.Remove(c.gameObject);//removes object c
                Destroy(c.gameObject);//destroys left over static mesh of c
            }
            //pawn promotion if pawn reaches other side of board, also some Enpassant stuff:
            if (x == EnPassantMove[0] && y == EnPassantMove[1])//destroy enemy unit using enpassantmove
            {
                if (isWhiteTurn)//white team
                {
                    c = Chessmans[x, y - 1];
                    activeChessman.Remove(c.gameObject);//removes object c
                    Destroy(c.gameObject);//destroys left over static mesh of c
                }
                else //black team
                {
                    c = Chessmans[x, y + 1];
                    activeChessman.Remove(c.gameObject);//removes object c
                    Destroy(c.gameObject);//destroys left over static mesh of c
                }
            }
            EnPassantMove[0] = -1;//initialize Enpassantmove at index 0
            EnPassantMove[1] = -1;//initialize enpassant move at index 1
            if (selectedChessman.GetType() == typeof(Pawn))//if selected unit is a pawn
            {
                if (y == 7)
                {
                activeChessman.Remove(selectedChessman.gameObject);//remove selected unit
                Destroy(selectedChessman.gameObject);//destroy pawn static mesh
                SpawnChessman(1, x, y);//spawn prefab of queen for white team
                selectedChessman = Chessmans[x, y]; //set chessman back
                }
                else if (y == 0)
                {
                activeChessman.Remove(selectedChessman.gameObject);//remove selected unit
                Destroy(selectedChessman.gameObject);//destroy pawn static mesh
                SpawnChessman(7, x, y);//spawn prefab of queen for black team
                selectedChessman = Chessmans[x, y]; //set chessman back
                }
                if (selectedChessman.CurrentY == 1 && y == 3)//enpassantmove for white pawn piece
                { 
                EnPassantMove[0] = x;
                EnPassantMove[1] = y - 1;
                 }
                else if (selectedChessman.CurrentY == 6 && y == 4)//enpassantmove for black pawn piece
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }
            }
            //allow basic movement of units 
            Chessmans [selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            //when moving piece, the piece is not on any tile while moving selected 
            selectedChessman.transform.position = GetTileCenter(x, y);//sets chesspiece to tile center at selected tile
            selectedChessman.SetPosition(x, y); //fixes glitch where one unit can not be moved more then once after highlight addition
            Chessmans [x, y] = selectedChessman; //place unit back at right spot in array, refers back to line in selectchessman method
            isWhiteTurn = !isWhiteTurn; //change player after each turn
        }

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;//sets material on unti back after deselecting

        BoardHighlights.Instance.Hidehighlights(); //hide highlights if wrong tile is selected
        selectedChessman = null; //if we made a non-possible move

    }

   /* private bool PreCheck(int x, int y, int [] kingPos) //check if king is in stalemate or not
    {
        //get moves that do not put the king in check
        //reset the precheckmoves
        for (int l = 0; l < 8; l++)
            for (int m = 0; m < 8; m++)
                PreCheckMoves[l, m] = false;

        //if we are not expecting a black space
        if (Chessmans[x, y] != null)
        {
            if (Chessmans[x, y].GetType() == typeof(King))
            {

            }
        }
    }*/

    private void UpdateSelection() //function to update selection of tiles
    {
        if (!Camera.main)//if there is no camera, return function
            return;

        RaycastHit hit;//variable for if object is 'hit' selected
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        //physics.raycast creates ray, variable is camera, origin of ray; input mouse - end of ray; 
        //out hit - set end point to raycast variable hit, 25.0f - length of ray; laymask - area where ray can interact
        {
            //Debug.Log(hit.point);//where the mouse hit happens
            selectionX = (int)hit.point.x;//set selectionx to x-axis of mouse hit
            selectionY = (int)hit.point.z;//set selectionY to z-axis of mouse hit
        }
        else
        {
            selectionX = -1;//reset to default
            selectionY = -1;//reset to default
        }
    }

    private void SpawnChessman(int index, int x, int y)//function to spawn chess pieces on tiles
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        //gameobject go is created to take unit of element at index, its position, and rotation, and set a GameObject
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();//get tile location on array
        Chessmans[x, y].SetPosition(x, y);//set position of piece on tile in array
        activeChessman.Add(go);//add piece to tile
    }

    private void SpawnAllChessmans()//function to spawn all units correctly
    {
        activeChessman = new List<GameObject>();//list of chesspieces set to a new list

        Chessmans = new Chessman[8, 8];//initialize Chessmans as an 8x8 array
        EnPassantMove = new int[2] { -1, -1 };//position on board, set; get; are initialized
        //"SpawnChessman('element number of unit', x position, 'y' position)
        //spawn the white team:
        SpawnChessman(0, 3, 0);//spawn chesspiece of element 0 (whiteking) at index position (3,0)
        SpawnChessman(1, 4, 0);//spawn white queen chesspiece at index position (4,0)
        SpawnChessman(2, 0, 0);//spawn white Rook at (0,0)
        SpawnChessman(2, 7, 0);//spawn white Rook at (7,0)
        SpawnChessman(3, 2, 0);//spawn white bishop at (2,0)
        SpawnChessman(3, 5, 0);//spawn white bishop at (5,0)
        SpawnChessman(4, 1, 0);//spawn white Knight at (1,0)
        SpawnChessman(4, 6, 0);//spawn white Knight at (6,0)
        for(int i = 0; i < 8; i++)//forloop to spawn white pawns from (0,1) to (7,1)
        {
            SpawnChessman(5, i, 1);
        }
        //spawn the black team:
        SpawnChessman(6, 4, 7);//spawn black king at (4,7)
        SpawnChessman(7, 3, 7);//spawn black queen at (3,7)
        SpawnChessman(8, 0, 7);//spawn black Rook at (0,7)
        SpawnChessman(8, 7, 7);//spawn black Rook at (7,7)
        SpawnChessman(9, 2, 7);//spawn black bishop at (2,7)
        SpawnChessman(9, 5, 7);//spawn black bishop at (5,7)
        SpawnChessman(10, 1, 7);//spawn black Knight at (1,7)
        SpawnChessman(10, 6, 7);//spawn black Knight at (6,7)
        for (int i = 0; i < 8; i++)//forloop to spawn black pawns from (0,6) to (7,6)
        {
            SpawnChessman(11, i, 6);
        }
    }

    private Vector3 GetTileCenter(int x, int y)//offset spawn point to middle of tile
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET; //set tile spawn to middle of tile in x-axis
        origin.z += (TILE_SIZE * y) + TILE_OFFSET; //set tile spawn to middle of tile in y-axis (z-axis)
        //origin is based on z since in Unity y-axis is height, not length
        return origin; //return value when function is called
    }

    private void DrawChessboard() //function to draw chessboard
    {
        Vector3 widthLine = Vector3.right * 8; //creates line facing right direction of length 8
        Vector3 lengthLine = Vector3.forward * 8; //creates line facing forward direction of length 8

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i; //for each i value, a new widthLine vector facing forward is created, start is defined as Vecotr 3
            Debug.DrawLine(start, start + widthLine); //for each i value, each new widthLine vector is moved 1 space forward
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j; //for each j value, a new lengthLine vector facing right is created, uses Vector 3 from definition above
                Debug.DrawLine(start, start + lengthLine); //for each j value, each new lengthLine vector is moved 1 space right
            }
        }

        //Draw the selected tile
        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
            Vector3.forward * selectionY + Vector3.right * selectionX,//start point
            Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1)//end point
            );

            Debug.DrawLine(
            Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,//adds the second slash on tile when clicked
            Vector3.forward * selectionY + Vector3.right * (selectionX + 1) 
            );
        }

    }

    private void EndGame()//end game function
    {
        if (isWhiteTurn)
            Debug.Log("White Team Wins!");
        else
            Debug.Log("Black Team Wins!");

        foreach (GameObject go in activeChessman)//destroys all units at end of game
            Destroy(go);//destroy action


        isWhiteTurn = true;//start a new game
        BoardHighlights.Instance.Hidehighlights();//deselect all units
        SpawnAllChessmans();//spawn new units for new game
    }

}
