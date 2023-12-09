﻿using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public interface IPlayerSpaceshipModule
    {
        public Transform PlayerSpaceShipTransform { get; }
        void CreatePlayerSpaceship();
        void Dispose();
        public void SetSpaceShipMoveDirection(float xDirection);
    }
}