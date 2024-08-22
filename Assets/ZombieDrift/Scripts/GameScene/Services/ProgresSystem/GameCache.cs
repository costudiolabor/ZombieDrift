using Gameplay;

namespace Project {

    public class GameCache {
        public Map map { get; set; }
        public Zombie[] zombies { get; set; }
        public Car car { get; set; }
        public int stageIndex { get; set; }
        public int moneyCount { get; set; }
        public int currentCarIndex { get; set; }
        public int[] availableCars { get; set; } = { 0 };
        public int mapIndex { get; set; }
        public int mapsCount { get; set; }

        public SaveData saveData1 {
            get {
                return new SaveData() {
                    stageIndex = stageIndex,
                    moneyCount = moneyCount,
                    currentCarIndex = currentCarIndex,
                    availableCars = availableCars
                };
            }
            set {
                stageIndex = value.stageIndex;
                moneyCount = value.moneyCount;
                currentCarIndex = value.currentCarIndex;
                availableCars = value.availableCars;
            }
        }
    }
}