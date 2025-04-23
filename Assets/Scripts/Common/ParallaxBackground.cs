using UnityEngine;

namespace NinjaFSM.Common
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Transform _camera;
        public float parallaxEffectMultiplier;
        public bool lockY = true;

        private Vector3 _lastCameraPosition;

        void Start()
        {
            if (_camera == null) _camera = Camera.main.transform;

            _lastCameraPosition = _camera.position;
        }

        void LateUpdate()
        {
            Vector3 deltaMovement = _camera.position - _lastCameraPosition;
            float xMove = deltaMovement.x * parallaxEffectMultiplier;
            transform.position += new Vector3(xMove, 0f, 0f);
            _lastCameraPosition = _camera.position;
        }
    }
}