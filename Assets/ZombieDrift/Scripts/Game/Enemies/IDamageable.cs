using UnityEngine;

public interface IDamagable {
    Vector3 position { get; }
    void Damage();
}