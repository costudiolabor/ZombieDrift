using UnityEngine;

namespace Garage {
    public class RotatablePodium {
        private const float ROTATION_SPEED_IN_DEGREES_PER_UPDATE = -0.3f;
        public Transform spawnParent => _parent;
        private Transform _parent;

        public void Initialize(Transform parentTransform) =>
            _parent = parentTransform;

        public void RotateAround() =>
            _parent.transform.rotation *= Quaternion.AngleAxis(ROTATION_SPEED_IN_DEGREES_PER_UPDATE, Vector3.up);
    }
}