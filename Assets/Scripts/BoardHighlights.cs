using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { set; get; } //created an Instance that can be accessed by other classes

    public GameObject hightlightPrefab; //object that is base for highlight tile selection
    private List<GameObject> highlights; //object containing prefab higlights created

    private void Start()
    {
        Instance = this; //sets the instance to this, instance is the reference to tile that is highlighted for other classes
        highlights = new List<GameObject>(); //initialized highlights at game start
    }

    private GameObject GetHighlightObject() //function to create a list of highlighted objects
    {
        GameObject go = highlights.Find(g => !g.activeSelf);
        // go is set to an object 'g' in the list 'highlights that is not selfactive
        if (go == null)
        {
            go = Instantiate(hightlightPrefab);//if go is empty, create a highlightPrefab
            highlights.Add(go); //add this newly created variable to list go
        }

        return go; //return list after finding or creating objects in go

    }

    public void HighlightAllowedMoves(bool [,,] moves) //multidimensional array of possible movements for highlighted pieces
    {
        for (int i=0; i<8; i++)
            for (int j=0; j<8; j++)
                for(int k = 0; k<35; k++)
                    if (moves[i, j, k]) //if moves are within boundaries of limits for i and j and k
                    {
                        GameObject go = GetHighlightObject(); //get non active object in our list or create a new one
                        go.SetActive(true); //non active object is set to active
                        go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f); //move object position to tile that is highlighted
                        // '+0.5f' is the offset to place the highlights on center of tiles, instead of it's origin corners
                    }
    }

    public void Hidehighlights()//hide highlights when not using unit
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }

}
