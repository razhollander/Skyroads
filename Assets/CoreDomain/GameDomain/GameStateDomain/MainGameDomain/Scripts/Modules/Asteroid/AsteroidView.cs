using System;
using System.Collections;
using System.Collections.Generic;
using CoreDomain.Utils.Pools;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IPoolable
{
    [SerializeField] private Renderer _renderer;
    public float RendererHeight => _renderer.bounds.size.y;
    public Action Despawn { get; set; }
    public void OnSpawned()
    {
        
    }

    public void OnDespawned()
    {
        gameObject.SetActive(false);
    }
}
