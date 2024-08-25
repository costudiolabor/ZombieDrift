using System;
using UnityEngine;

namespace Gameplay {
	[Serializable]
	public class CarData {
		public Car car;
		public int price;
		[HideInInspector]
		public bool isSelected;
		[HideInInspector]
		public bool isPurchased;
	}
}
