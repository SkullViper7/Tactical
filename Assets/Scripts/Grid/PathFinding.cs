using System;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int Pos_X;
    public int Pos_Y;

    public float GValue;
    public float HValue;

    public PathNode ParentNode;

    public float FValue
    {
        get { return GValue + HValue; }
    }

    // Constructor for the PathNode class that initializes the Pos_X and Pos_Y properties with the provided x and y positions
    public PathNode(int xPos, int yPos)
    {
        Pos_X = xPos;
        Pos_Y = yPos;
    }
}

[RequireComponent(typeof(Grid))]
public class PathFinding : MonoBehaviour
{
    Grid _gridMap;

    PathNode[,] _pathNodes;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Initializes the grid map and path nodes. If the grid map is null, it is obtained from the component. 
    /// Then, it creates a 2D array of path nodes based on the grid map's dimensions and populates it with new PathNode instances.
    /// </summary>
    void Init()
    {
        // If the _gridMap is null, obtain the Grid component and assign it to _gridMap
        if (_gridMap == null)
        {
            _gridMap = GetComponent<Grid>();
        }

        // Create a 2D array of PathNode instances with dimensions based on the grid map's Length and Width
        _pathNodes = new PathNode[_gridMap.Length, _gridMap.Width];

        // Populate the _pathNodes array with new PathNode instances using the indices as the x and y positions for each PathNode
        for (int i = 0; i < _gridMap.Length; i++)
        {
            for (int j = 0; j < _gridMap.Width; j++)
            {
                _pathNodes[i, j] = new PathNode(i, j);
            }
        }
    }

    /// <summary>
    /// Finds the path from the start node to the end node using the A* algorithm.
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="startY"></param>
    /// <param name="endX"></param>
    /// <param name="endY"></param>
    /// <returns></returns>
    public List<PathNode> FindPath(int startX,  int startY, int endX, int endY)
    {
        // Define start and end nodes based on the specified positions in the _pathNodes array
        PathNode startNode = _pathNodes[startX, startY];
        PathNode endNode = _pathNodes[endX, endY];

        // Initialize openList and closedList as new lists of PathNodes, and add the startNode to openList
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        openList.Add(startNode);

        // Perform A* pathfinding algorithm
        while (openList.Count > 0)
        {
            // Find the node with the lowest FValue in the openList
            PathNode currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                // Update currentNode if a node with a lower FValue is found
                if (currentNode.FValue > openList[i].FValue)
                {
                    currentNode = openList[i];
                }

                // If the FValue is equal, prioritize the node with a lower HValue
                if (currentNode.FValue == openList[i].FValue && currentNode.HValue > openList[i].HValue)
                {
                    currentNode = openList[i];
                }
            }

            // Remove currentNode from openList and add it to closedList
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // If the currentNode is the endNode, retrace and return the path
            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            // Find the neighboring nodes of the currentNode
            List<PathNode> neighbourNodes = new List<PathNode>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (Math.Abs(i) + Math.Abs(j) > 1)
                    {
                        continue;
                    }
                    if (!_gridMap.CheckBoundary(currentNode.Pos_X + i, currentNode.Pos_Y + j))
                    {
                        continue;
                    }
                    neighbourNodes.Add(_pathNodes[currentNode.Pos_X + i, currentNode.Pos_Y + j]);
                }
            }

            // Evaluate each neighbor and update their G, H, and ParentNode if necessary
            // Add the neighbor to openList if it's not already there
            for (int i = 0; i < neighbourNodes.Count; i++)
            {
                if (closedList.Contains(neighbourNodes[i]))
                {
                    continue;
                }
                if (!_gridMap.CheckWalkable(neighbourNodes[i].Pos_X, neighbourNodes[i].Pos_Y))
                {
                    continue;
                }
                float movementCost = currentNode.GValue + CalculateDistance(currentNode, neighbourNodes[i]);
                if (!openList.Contains(neighbourNodes[i]) || movementCost < neighbourNodes[i].GValue)
                {
                    neighbourNodes[i].GValue = movementCost;
                    neighbourNodes[i].HValue = CalculateDistance(neighbourNodes[i], endNode);
                    neighbourNodes[i].ParentNode = currentNode;
                    if (!openList.Contains(neighbourNodes[i]))
                    {
                        openList.Add(neighbourNodes[i]);
                    }
                }
            }
        }

        // If no path is found, return null
        return null;
    }

    /// <summary>
    /// Calculates the distance between two nodes.
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    int CalculateDistance(PathNode currentNode, PathNode target)
    {
        // Calculate the distance between currentNode and the target node
        int distX = Mathf.Abs(currentNode.Pos_X - target.Pos_X);
        int distY = Mathf.Abs(currentNode.Pos_Y - target.Pos_Y);

        // Determine the diagonal distance as the minimum of (distX, distY) and the straight distance as the difference between the two distances
        // Return the computed heuristic value based on the A* pathfinding algorithm
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }

    /// <summary>
    /// Retraces the path from the endNode to the startNode.
    /// </summary>
    /// <param name="startNode"></param>
    /// <param name="endNode"></param>
    /// <returns></returns>
    List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        // Initialize an empty list to store the nodes in the calculated path
        List<PathNode> path = new List<PathNode>();

        // Set the currentNode to be the endNode
        PathNode currentNode = endNode;

        // Traverse through the nodes from end to start, adding each node to the path list
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        // Reverse the order of the nodes in the path list to start from the startNode
        path.Reverse();

        // Return the completed path
        return path;
    }
}
