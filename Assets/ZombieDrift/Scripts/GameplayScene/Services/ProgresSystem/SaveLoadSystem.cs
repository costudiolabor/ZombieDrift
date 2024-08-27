using System.Collections.Generic;
using System.Linq;

namespace Project {

    public class SaveLoadSystem {
        private SaveData _data;

        /*public SaveData data {
            get => _saveData;
            set => _saveData = value;
        }
        public int mapIndex { get; set; }
        public int stageIndex => _saveData.stageIndex;
        public int currentCarIndex => _saveData.currentCarIndex;
    
        private SaveData _saveData;
    
        public void IncreaseStageIndex() {
            _saveData.stageIndex++;
        }
    
        public void IncreaseMoney(int count) {
            _saveData.moneyCount += count;
        }
    
        public void DecreaseMoney(int count) {
            var moneyLeft = _saveData.moneyCount - count;
            _saveData.moneyCount = moneyLeft > 0 ? moneyLeft : 0;
        }
    
        public void AddAvailableCarIndex(int index) {
            var indexesHash = new HashSet<int>(_saveData.availableCars) { index };
            _saveData.availableCars = indexesHash.ToArray();
        }
        // отдельным классом сохранять*/
        public SaveData Load() {
            return _data ?? new SaveData() {
                currentCarIndex = 1,
                moneyCount = 34500,
                purchasedCarsIndexes = new []{1}
            };
        }

        public void Save(SaveData data) {
            _data = data;
        }
    }
}