using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Color topColor;


    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom;

    Vector2Int gridPos;

    const int gridSize = 10;

    public void Update()
    {
        //SetExploredColor(topColor); // Add back in if wanting neighbouring blocks coloured. WARNING - Overwrites start and finish colours.
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int
            (
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();  // #transform.find# searches the object's children for the string reference. Then get that child's mesh renderer.
        topMeshRenderer.material.color = color;                                             // find the child's color and set it to the color passed through when the method is called.
    }

    public void SetPathColor(Color color)
    {
        MeshRenderer blockMeshRenderer = transform.Find("Block_Friendly").GetComponent<MeshRenderer>();
        blockMeshRenderer.material.color = color;                                             
    }

    public void SetExploredColor(Color color)
    {
        if (isExplored == true)
        {
            MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
            topMeshRenderer.material.color = topColor;                                         
        }
        else 
        {
            //do nothing
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            if (isPlaceable)
            {
                print("Tower placed at " + gameObject.name);
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else 
            {
                print("Can't place here!");
            }
        }
        
    }

}
