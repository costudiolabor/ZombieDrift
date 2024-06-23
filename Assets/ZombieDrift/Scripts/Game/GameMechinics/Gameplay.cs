using System.Collections.Generic;
using UnityEngine;

public class Gameplay {
    
    private GameplayData _data;
    private List<Zombie> _zombies;
    
    public void Create(GameplayData data) {
        _data = data;
        
    }

    public void ClearCurrentData() {
        
    }
}
