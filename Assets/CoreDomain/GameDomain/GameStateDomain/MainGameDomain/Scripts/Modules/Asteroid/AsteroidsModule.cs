using System.Collections;
using System.Collections.Generic;
using CoreDomain.Utils.Pools;
using UnityEngine;

public class AsteroidsModule
{
    private AsteroidsPool.Factory _asteroidsPool;
    private AsteroidsCreator _asteroidsCreator;
    
    public AsteroidsModule(AsteroidsPool.Factory asteroidsPool)
    {
        _asteroidsCreator = new AsteroidsCreator(asteroidsPool);
    }
}
