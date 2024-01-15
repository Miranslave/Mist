using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{
    public int SortingOrder = 100;

    public Renderer vfxRenderer;

    public string layer;
    // Start is called before the first frame update
    private void OnValidate()
    {
        vfxRenderer = GetComponent<Renderer>();
        if (vfxRenderer)
        {
            vfxRenderer.sortingOrder = SortingOrder;
            vfxRenderer.sortingLayerName = layer;
        }
    }
}
