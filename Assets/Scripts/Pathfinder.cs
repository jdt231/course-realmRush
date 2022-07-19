using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{   
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();     // Our dictionary
    Queue<Waypoint> queue = new Queue<Waypoint>();                                      //Our Pathfinding Queue

    bool isRunning = true;
    bool pathStored = false;

    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =                                                           // So when we use this array to search, it will check up, then right, then down, and then left.
        {
            Vector2Int.up,      //(0,1)
            Vector2Int.right,   //(1,0)
            Vector2Int.down,    //(0,-1)
            Vector2Int.left     //(-1,0)
        };

    public List<Waypoint> GetPath()
    {
        if (!pathStored)
        {
            CalculatePath();
            pathStored = true;
        }
        else { }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();           //adds the waypoints (each of our blocks) to the grid dictionary
        ColorStartAndEnd();     //setting the colour of the top quad for both the start and finish blocks to different colours.
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }

    void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
        waypoint.SetPathColor(Color.red);
    }
    
    private void BreadthFirstSearch()                             // queue.Enqueue - adds to end of queue.   queue.Dequeue - returns the front of the queue.
    {
        queue.Enqueue(startWaypoint);                   //puts the start waypoint into the queue
        
        while (queue.Count > 0 && isRunning)            // while there is something in the queue (which there now is due to the above line) and hasn't found the end.
        {
            searchCenter = queue.Dequeue();             //stores our starting point in searchCenter and removes it from the queue.
            //print("Searching from: " + searchCenter); // TODO remove log
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;             // mark the waypoint as already explored so that it doesn't get re-added to the queue later.
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            //print("Searching from end node, therefore stopping"); // TODO remove log
            isRunning = false;
        }
    }
    
    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        
        foreach (Vector2Int direction in directions)                                        // array contains directions: left, right, up and down.
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;        // take the starting/current block co-ordinates and add the movement vectors to it. See "Example 1" at end of page.
            if (grid.ContainsKey(neighbourCoordinates))                                     // if the co-ordinates exist in the list (if there is no block on the level then they wont)
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];                            // search the grid (dictionary) for the block at this co-ordinate and store it in "neighbour"
        if (neighbour.isExplored || queue.Contains(neighbour))                      // if this block has already previously been added to the queue.
        { 
        // do nothing
        }
        else
        {
            queue.Enqueue(neighbour);                                               // Add the neighbouring block to the search queue.
            //print("Queueing " + neighbour); //TODO - remove when finished.
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void LoadBlocks()                                   // the "grid" is our dictionary we set up
    {
        var waypoints = FindObjectsOfType<Waypoint>();          //create array called "waypoints" and store everything of type *Waypoint* i.e our blocks.
        foreach (Waypoint waypoint in waypoints)                // for every (type *Waypoint* store as name "waypoint") found within our "waypoints" array
        {
            var gridPos = waypoint.GetGridPos();                // storing the blocks position in "gridPos"
            if (grid.ContainsKey(gridPos))                      // checking if the current waypoint position is already held in the dictionary (if the dictionary (grid) already "contains" that set of co-ordinates (gridPos))
            {
                Debug.LogWarning("Skipping Overlapping block" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);                    // add the Vector2 position of the waypoint(gridPos) and the waypoint value itself to the dictionary (if not already in dictionary)
            }
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }




    // Example 1
    // If the starting block is at position 1,1 and you want to check to the right, adding 1,0 to this makes it 2,1.
    // 2,1 being the co-ordinates of the block/waypoint directly to the right of the current one (1,1)
}
