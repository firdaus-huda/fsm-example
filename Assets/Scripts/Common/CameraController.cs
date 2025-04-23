using UnityEngine;

namespace NinjaFSM.Common
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothTime = 0.2f;

        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;

        private Vector3 _velocity = Vector3.zero;

        void LateUpdate()
        {
            if (target == null) return;

            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
        }
    }
}