using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGrid : MonoBehaviour
{
    [SerializeField]
    Material _baseMat;
    [SerializeField]
    Material _highlightMat;

    GameObject _targetGrid;

    PathFinding _pathFindingScript;

    List<PathNode> _path;

    [SerializeField]
    GridObject _targetCharacter;

    private void Start()
    {
        _targetGrid = GameObject.Find("Grid");
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
    }

    private void OnMouseOver()
    {
        GetComponent<MeshRenderer>().material = _highlightMat;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = _baseMat;
    }
}
