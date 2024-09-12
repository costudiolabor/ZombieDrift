using System.Collections.Generic;
using System.Linq;

namespace Project {
	public interface ISaveLoad {
		public SaveData Load();
		public void Save(SaveData data);
		
	}
	
	public class SaveLoadSystem : ISaveLoad {
		private SaveData _data;

		public SaveData Load() {
			return _data ?? new SaveData() {
					currentCarIndex = 0,
					moneyCount = 200,
					purchasedCarsIndexes = new[] {0}
			};
		}

		public void Save(SaveData data) {
			_data = data;
		}
	}
}
