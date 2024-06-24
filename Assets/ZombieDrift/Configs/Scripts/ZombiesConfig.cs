using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ZombiesConfig", fileName = "ZombiesConfig", order = 0)]
public class ZombiesConfig : ScriptableObject {
    [SerializeField] private Zombie[] _zombies;
   
    public Zombie[] zombies => _zombies;

    public int count => zombies.Length;

}
