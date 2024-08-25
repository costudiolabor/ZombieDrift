using Gameplay;

namespace Project {

	public class ProjectCache {

		//Scene chache
	//	public Map map { get; set; }
	////	public Zombie[] zombies { get; set; }
	//	public Car car { get; set; }
	//	public int mapIndex { get; set; }
	//	public int mapsCount { get; set; }
		//Scene chache
		public int stageIndex {
			get => saveData.stageIndex;
			set => saveData.stageIndex = value;
		}
		public int moneyCount {
			get => saveData.moneyCount;
			set => saveData.moneyCount = value;
		}
		public int selectedCarIndex {
			get => saveData.currentCarIndex;
			set => saveData.currentCarIndex = value;
		}

		public int[] purchasedCars {
			get => saveData.availableCars;
			set => saveData.availableCars = value;
		}

		public SaveData saveData { get; set; }
	}
}
