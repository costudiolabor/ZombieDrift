using System.Collections.Generic;
using System.Linq;

namespace Project {
    public class ProjectCache {
        public int stageIndex {
            get => _stageIndex;
            set => _stageIndex = value;
        }

        public int moneyCount {
            get => _moneyCount;
            set => _moneyCount = value;
        }

        public int selectedCarIndex {
            get => _currentCarIndex;
            set => _currentCarIndex = value;
        }

        public HashSet<int> purchasedCars => _purchasedCars;

        private HashSet<int> _purchasedCars;
        private int _stageIndex, _moneyCount, _currentCarIndex;

        public SaveData saveData {
            get => new() {
                purchasedCarsIndexes = _purchasedCars.ToArray(),
                currentCarIndex = _currentCarIndex,
                moneyCount = _moneyCount,
                stageIndex = _stageIndex
            };
            set {
                _purchasedCars = new HashSet<int>(value.purchasedCarsIndexes);
                _currentCarIndex = value.currentCarIndex;
                _stageIndex = value.stageIndex;
                _moneyCount = value.moneyCount;
            }
        }
    }
}