namespace Project {
	public class MoneyWallet {
		private readonly ProjectCache _projectCache;

		public int count => _projectCache.moneyCount;

		public MoneyWallet(ProjectCache projectCache) =>
				_projectCache = projectCache;

		public void AddCoins(int value = 1) =>
				_projectCache.moneyCount += value;

		public void SpendCoin(int value) {
			var moneyCountAfterSpend = _projectCache.moneyCount - value;
			if (moneyCountAfterSpend > 0)
				_projectCache.moneyCount = moneyCountAfterSpend;
		}
	}
}
