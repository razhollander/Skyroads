using System;
using System.Collections;
using System.Collections.Generic;
using CoreDomain.Utils.Pools;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IPoolable
{
    public Action Despawn { get; set; }
    public void OnSpawned()
    {
        
    }

    public void OnDespawned()
    {
        gameObject.SetActive(false);
    }
}
