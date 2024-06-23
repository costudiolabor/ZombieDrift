
public record GameplayData(Map map, Zombie[] zombies, Car car);
public record SaveData () {
    public int stageIndex;
    public int moneyCount;
    public int currentCarIndex;
    public int[] availableCars = {0};
}
