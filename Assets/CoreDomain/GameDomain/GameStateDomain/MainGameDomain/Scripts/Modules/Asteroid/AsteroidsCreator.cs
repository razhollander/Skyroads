using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreDomain.Utils.Pools;

public class AsteroidsCreator
{
    private AsteroidsPool _asteroidsPool;

    public AsteroidsCreator(AsteroidsPool.Factory asteroidsPool)
    {
        _asteroidsPool = asteroidsPool.Create(new PoolData(10, 5));
        _asteroidsPool.InitPool();
    }

    public AsteroidView CreateEnemy(string enemyAssetName)
    {
        return _asteroidsPool.Spawn();
    }
}
