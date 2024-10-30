using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project {
	public interface ISaveLoad {
		public SaveData Load();
		public void Save(SaveData data);

	}
	public class SaveLoadSystem : ISaveLoad {
		private const string MUTE_KEY = "isMute";

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

		public bool LoadMuteStateFromPrefs() {
			if (!PlayerPrefs.HasKey(MUTE_KEY))
				return false;
			return PlayerPrefs.GetInt(MUTE_KEY) > 0;
		}

		public void SaveMuteStateFromPrefs(bool isMute) {
			var isMuteInt = isMute ? 1 : 0;
			PlayerPrefs.SetInt(MUTE_KEY, isMuteInt);
		}
	}
}
