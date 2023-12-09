using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public interface IPlayerSpaceshipModule
    {
        void CreatePlayerSpaceship();
        void Dispose();
        public void SetSpaceShipMoveDirection(float xDirection);
    }
}