using UnityEngine;

public class PlayCache {
    public GameplayData gameplayData;

    public void DestroyGameObjectsAndClear() {
        var zombies = gameplayData.zombies;
        var car = gameplayData.car;
        var map = gameplayData.map;
        
        foreach (var zombie in zombies)
            Object.Destroy(zombie.gameObject);

        Object.Destroy(car.gameObject);
        Object.Destroy(map.gameObject);
        gameplayData = null;
    }
    
    
}