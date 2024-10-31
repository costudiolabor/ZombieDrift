namespace Project {
	public record ProgressData() {
		public int stageIndex;
		public int moneyCount;
		public int currentCarIndex;
		public int[] purchasedCarsIndexes = {0};
	}
}
