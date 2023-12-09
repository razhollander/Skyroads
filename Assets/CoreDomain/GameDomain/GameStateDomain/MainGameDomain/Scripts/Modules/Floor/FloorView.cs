using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private Material _floorMaterial;
    private Vector2 _currentTextureOffset=Vector2.zero;
    private float _floorTextureScale;
    private void Awake()
    {
        _floorMaterial = _renderer.sharedMaterial;
    }

    public void ChangeOffset(float offsetDelta)
    {
        _currentTextureOffset = CalcNewOffset(offsetDelta);
        _floorMaterial.mainTextureOffset = _currentTextureOffset;
    }

    private Vector2 CalcNewOffset(float offsetDelta)
    {
        var newOffset = offsetDelta + _currentTextureOffset.y;

        if (newOffset > 1f)
            newOffset %= 1f;

        return new Vector2(0, newOffset);
    }
}
