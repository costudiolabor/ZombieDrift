using UnityEngine;

namespace Project {
    public static class Utils {
        public static void MoveToLayer(Transform[] transforms, LayerMask layerMask) {
            foreach (var transform in transforms)
                MoveToLayer(transform, layerMask);
        }

        public static void MoveToLayer(Transform transform, LayerMask layerMask) =>
            transform.gameObject.layer = (int)Mathf.Log(layerMask.value, 2);
    }
}