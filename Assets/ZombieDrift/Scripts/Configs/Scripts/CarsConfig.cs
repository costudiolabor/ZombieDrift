using UnityEngine;

namespace Gameplay {

	[CreateAssetMenu(menuName = "Configs/CarsConfig", fileName = "CarsConfig", order = 0)]
	public class CarsConfig : ScriptableObject {
		[SerializeField] private LayerMask _shopNotPurchasedLayerMask, _shopPurchasedLayerMask;
		[SerializeField] private CarData[] _cars;
		public LayerMask shopPurchasedLayerMask => _shopPurchasedLayerMask;
		public LayerMask shopNotPurchasedLayerMask => _shopNotPurchasedLayerMask;
		public CarData[] cars => _cars;
		public int count => cars.Length;

	}
}
