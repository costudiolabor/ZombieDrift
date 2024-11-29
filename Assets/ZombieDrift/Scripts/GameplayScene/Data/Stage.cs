using System;
using UnityEngine;

namespace Gameplay {
	[Serializable]
	public class Stage {
		public float mapsRotationAxisY;
		public Vector3 mapLightDirection;
		public Color lightColor = Color.white;
		public Map[] maps;
		public int count => maps.Length;
	}
}
