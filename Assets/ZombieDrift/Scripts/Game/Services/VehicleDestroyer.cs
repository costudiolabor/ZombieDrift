using Cysharp.Threading.Tasks;
using UnityEngine;

public class VehicleDestroyer {
    private const string CAR_LAYER_NAME = "Car";
    private const float EXPLOSION_POWER = 300;
    private const float EXPLOSION_RADIUS = 1;
    private const float OVERLAP_RADIUS = 0.7f;
    private Car _car;

    public void SetCar(Car car) {
        _car = car;
    }

    public async void Destroy(Vector3 hitPoint) {
        Object.Destroy(_car.body);
        await UniTask.DelayFrame(1);
        LayerMask mask = LayerMask.GetMask(CAR_LAYER_NAME);
        Collider[] colliders = new Collider[15];
        var size = Physics.OverlapSphereNonAlloc(hitPoint, OVERLAP_RADIUS, colliders, mask);

        for (var i = 0; i < size; i++) {
            var rigidbody = colliders[i].gameObject.AddComponent<Rigidbody>();
            rigidbody.AddExplosionForce(EXPLOSION_POWER, hitPoint, EXPLOSION_RADIUS, 1.0F);
        }
    }
}