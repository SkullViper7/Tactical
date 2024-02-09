using System;
using System.Collections;
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

    void Init()
    {
        if (_gridMap == null)
        {
            _gridMap = GetComponent<Grid>();
        }

        _pathNodes = new PathNode[_gridMap.Length, _gridMap.Width];

        for (int i = 0; i < _gridMap.Length; i++)
        {
            for (int j = 0; j < _gridMap.Width; j++)
            {
                _pathNodes[i, j] = new PathNode(i, j);
            }
        }
    }

    public List<PathNode> FindPath(int startX,  int startY, int endX, int endY)
    {
        PathNode startNode = _pathNodes[startX, startY];
        PathNode endNode = _pathNodes[endX, endY];

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];

            for (int i = 0; i < openList.Count; i++)
            {
                if (currentNode.FValue > openList[i].FValue)
                {
                    currentNode = openList[i];
                }

                if (currentNode.FValue == openList[i].FValue && currentNode.HValue > openList[i].HValue)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            List<PathNode> neighbourNodes = new List<PathNode>();

            for (int i = -1; i < 2;  i++)
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

        return null;
    }

    int CalculateDistance(PathNode currentNode, PathNode target)
    {
        int distX = Mathf.Abs(currentNode.Pos_X - target.Pos_X);
        int distY = Mathf.Abs(currentNode.Pos_Y - target.Pos_Y);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distX + 10 * (distY - distX);
    }

    List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        path.Reverse();

        return path;
    }
}
