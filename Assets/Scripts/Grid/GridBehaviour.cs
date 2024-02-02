using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    [SerializeField]
    int _rows = 10;
    [SerializeField]
    int _columns = 10;
    [SerializeField]
    int _scale = 1;
    [SerializeField]
    GameObject _gridPrefab;

    Vector3 _leftBottomLocation = Vector3.zero;

    [SerializeField]
    GameObject[,] _gridArray;
    [SerializeField]
    int _startX = 0;
    [SerializeField]
    int _startY = 0;
    [SerializeField]
    int _endX = 2;
    [SerializeField]
    int _endY = 2;

    [SerializeField]
    List<GameObject> _path = new List<GameObject>();


    private void Start()
    {
        _gridArray = new GameObject[_columns, _rows];
        GenerateGrid();
    }

    public void SetRoute()
    {
        SetDistance();
        SetPath();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                GameObject obj = Instantiate(_gridPrefab, new Vector3(_leftBottomLocation.x + _scale * i, _leftBottomLocation.y, _leftBottomLocation.z + _scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().X = i;
                obj.GetComponent<GridStat>().Y = j;
                _gridArray[i, j] = obj;
            }
        }
    }

    void InitialSetup()
    {
        foreach (GameObject obj in _gridArray)
        {
            obj.GetComponent<GridStat>().Visited = -1;
        }
        _gridArray[_startX, _startY].GetComponent<GridStat>().Visited = 0;
    }

    bool TestDirection(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if (y + 1 < _rows && _gridArray[x, y + 1] && _gridArray[x, y + 1].GetComponent<GridStat>().Visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 2:
                if (x + 1 < _columns && _gridArray[x + 1, y] && _gridArray[x + 1, y].GetComponent<GridStat>().Visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 3:
                if (y - 1 > -1 && _gridArray[x, y - 1] && _gridArray[x, y - 1].GetComponent<GridStat>().Visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 4:
                if (x - 1 > -1 && _gridArray[x - 1, y] && _gridArray[x - 1, y].GetComponent<GridStat>().Visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }

    void SetVisited(int x, int y, int step)
    {
        if (_gridArray[x, y])
        {
            _gridArray[x, y].GetComponent<GridStat>().Visited = step;
        }
    }

    void SetDistance()
    {
        InitialSetup();

        int x = _startX;
        int y = _startY;
        int[] testArray = new int[_rows * _columns];

        for (int step = 1; step < _rows * _columns; step++)
        {
            foreach (GameObject obj in _gridArray)
            {
                if (obj && obj.GetComponent<GridStat>().Visited == step - 1)
                {
                    TestFourDirections(obj.GetComponent<GridStat>().X, obj.GetComponent<GridStat>().Y, step);
                }
            }
        }
    }

    void TestFourDirections(int x, int y, int step)
    {
        if (TestDirection(x, y, -4, 1))
        {
            SetVisited(x, y + 1, step);
        }
        if (TestDirection(x, y, -1, 2))
        {
            SetVisited(x + 1, y, step);
        }
        if (TestDirection(x, y, -1, 3))
        {
            SetVisited(x, y - 1, step);
        }
        if (TestDirection(x, y, -1, 4))
        {
            SetVisited(x - 1, y, step);
        }
    }

    void SetPath()
    {
        int step;
        int x = _endX;
        int y = _endY;
        List<GameObject> tempList = new List<GameObject>();
        _path.Clear();

        if (_gridArray[_endX, _endY] && _gridArray[_endX, _endY].GetComponent<GridStat>().Visited > 0)
        {
            _path.Add(_gridArray[x, y]);
            step = _gridArray[x, y].GetComponent<GridStat>().Visited = 1;
        }
        else
        {
            return;
        }

        for (int i = step; i > -1; step--)
        {
            if (TestDirection(x, y, step, 1))
            {
                tempList.Add(_gridArray[x, y + 1]);
            }
            if (TestDirection(x, y, step, 2))
            {
                tempList.Add(_gridArray[x + 1, y]);
            }
            if (TestDirection(x, y, step, 3))
            {
                tempList.Add(_gridArray[x, y - 1]);
            }
            if (TestDirection(x, y, step, 4))
            {
                tempList.Add(_gridArray[x - 1, y]);
            }

            GameObject tempObj = FindClosest(_gridArray[_endX, _endY].transform, tempList);
            _path.Add(tempObj);
            x = tempObj.GetComponent<GridStat>().X;
            y = tempObj.GetComponent<GridStat>().Y;
            tempList.Clear();
        }
    }

    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = _scale * _rows * _columns;
        int indexNumber = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }
}