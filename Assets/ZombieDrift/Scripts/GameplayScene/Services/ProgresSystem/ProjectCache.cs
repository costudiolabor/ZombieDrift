namespace Project {
	public class ProjectCache {
		//private HashSet<int>  
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
