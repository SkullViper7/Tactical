using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGrid : MonoBehaviour
{
    [SerializeField]
    Material _baseMat;
    [SerializeField]
    Material _highlightMat;

    private void OnMouseOver()
    {
        GetComponent<MeshRenderer>().material = _highlightMat;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = _baseMat;
    }
}
