using Unity.AI.Navigation;
using UnityEngine;

public class AddComponents : MonoBehaviour {
    [SerializeField] private Transform parent;
    [SerializeField] private bool overrideArea = true;
    [SerializeField] private int areaType = 1;
    private void OnDrawGizmos() {
        if (parent == null) return;
        int countChild = parent.childCount;

        for (int i = 0; i < countChild; i++) {
            GameObject child = parent.GetChild(i).gameObject;
                if (!child.TryGetComponent(out Obstacle obstacle))
                    child.AddComponent<Obstacle>();
                
                if (!child.TryGetComponent(out NavMeshModifier navMeshModifier)) {
                    NavMeshModifier temp;
                    temp = child.AddComponent<NavMeshModifier>();
                    temp.overrideArea = overrideArea;
                    temp.area = areaType;
                }
        }
    }
}
