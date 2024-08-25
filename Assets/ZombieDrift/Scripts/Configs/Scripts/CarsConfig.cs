using UnityEngine;

namespace Gameplay {

    [CreateAssetMenu(menuName = "Configs/CarsConfig", fileName = "CarsConfig", order = 0)]
    public class CarsConfig : ScriptableObject {
        [SerializeField] private LayerMask _shopDarkLayerMask;
        [SerializeField] private CarData[] _cars;
        public LayerMask shopDarkLayerMask => _shopDarkLayerMask;
        public CarData[] cars => _cars;

        public int count => cars.Length;

    }
}