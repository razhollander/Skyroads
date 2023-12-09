using TMPro;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipView : MonoBehaviour
    {
        [SerializeField] private Transform _rendererTransform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rotationLerpFactor = 5f;
        private float _currentZRotation = 0;
        
        private void RotateOnZAxis(float zRotation)
        {
            _rendererTransform.rotation = Quaternion.Euler(0,0,zRotation);
        }

        public void SetVelocity(float xVelocity)
        {
            _rigidbody.velocity = new Vector2(xVelocity, 0);
        }

        public void LerpToRotation(float rotation)
        {
            var newZRotation = Mathf.Lerp(_currentZRotation, rotation, _rotationLerpFactor * Time.deltaTime);
            _currentZRotation = newZRotation;
            RotateOnZAxis(newZRotation);
        }
    }
}