using System.Collections.Generic;
using System.Linq;

public class ProgressService {
   public int stageIndex => _saveData.stageIndex;
   public int moneyCount => _saveData.moneyCount;
   public int currentCarIndex => _saveData.currentCarIndex;
   public int[] availableCars => _saveData.availableCars;
   
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
   
   public void LoadFormCloud() {
      _saveData = new SaveData();
   }

   public void SaveToCloud (){
      
   }
}
