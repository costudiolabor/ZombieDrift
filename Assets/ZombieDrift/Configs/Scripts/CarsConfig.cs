using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CarsConfig", fileName = "CarsConfig", order = 0)]
public class CarsConfig : ScriptableObject {
    [SerializeField] private Car[] _cars;

    public Car[] cars => _cars;

    public int count => cars.Length;

}