using TMPro;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipView : MonoBehaviour
    {
        [SerializeField] private Transform _rendererTransform;
        [SerializeField] private Rigidbody _rigidbody;

        public void RotateOnZAxis(float zRotation)
        {
            _rendererTransform.rotation = Quaternion.Euler(0,0,zRotation);
        }

        public void SetVelocity(float xVelocity)
        {
            _rigidbody.velocity = new Vector2(xVelocity, 0);
        }
    }
}