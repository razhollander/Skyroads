using System.Collections.Generic;
using CoreDomain.Utils.Pools;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Enemies
{
    public class EnemiesCreator
    {
        private readonly BeeEnemiesPool _beeEnemiesPool;
        private readonly GuardEnemiesPool _guardEnemiesPool;
        private readonly List<IAssetFromBundlePool<EnemyView>> _enemiesPools = new();
        
        public EnemiesCreator(BeeEnemiesPool.Factory beeEnemiesPoolFactory, GuardEnemiesPool.Factory guardEnemiesPoolFactory)
        {
            _enemiesPools.Add(beeEnemiesPoolFactory.Create(new PoolData(10, 5)));
            _enemiesPools.Add(guardEnemiesPoolFactory.Create(new PoolData(20, 5)));

            foreach (var enemiesPool in _enemiesPools)
            {
                enemiesPool.InitPool();
            }
        }

        public EnemyView CreateEnemy(string enemyAssetName)
        {
            return _enemiesPools.Find(x => x.AssetName == enemyAssetName).Spawn();
        }
    }
}