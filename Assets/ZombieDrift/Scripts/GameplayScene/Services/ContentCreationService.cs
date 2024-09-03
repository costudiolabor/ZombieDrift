using System;
using System.Collections.Generic;
using Project;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay {
    public class ContentCreationService {
        private readonly Factory _factory;
        private readonly StagesConfig _stagesConfig;
        private readonly ZombiesConfig _zombiesConfig;
        private readonly CarsConfig _carsConfig;

        public ContentCreationService(Factory factory, StagesConfig stagesConfig, ZombiesConfig zombiesConfig, CarsConfig carsConfig) {
            _factory = factory;
            _stagesConfig = stagesConfig;
            _zombiesConfig = zombiesConfig;
            _carsConfig = carsConfig;
        }

        public Map CreateMap(int stageIndex, int mapIndex, Transform parent = null) {
            if (stageIndex > _stagesConfig.count - 1)
                throw new Exception($"Stage with index {stageIndex} out of bounds levelConfig");

            var stage = _stagesConfig.stages[stageIndex];

            if (mapIndex > stage.maps.Length - 1)
                throw new Exception($"Stage with index {stageIndex} out of bounds levelConfig");

            var mapPrefab = stage.maps[mapIndex];
            var mapRotation = mapPrefab.transform.rotation;
            return _factory.Create<Map>(mapPrefab, parent, Vector3.zero, mapRotation);
        }

        public Car CreateCar(int carIndex, Transform point, Transform parent = null) {
            if (carIndex > _carsConfig.cars.Length - 1)
                throw new Exception($"Stage with index {carIndex} out of bounds levelConfig");

            var carPrefab = _carsConfig.cars[carIndex].car;
            return _factory.CreateAndBind<Car>(carPrefab, parent, point.position, point.rotation);
        }

        public Zombie[] CreateZombies(Transform[] points, Transform parent = null) {
            var zombies = new List<Zombie>();

            foreach (var point in points) {
                var randomIndex = Random.Range(0, _zombiesConfig.count);
                var zombie = _factory.Create(_zombiesConfig.zombies[randomIndex], parent, point.position, point.rotation);
                zombie.speedInterval = _zombiesConfig.speedIntervalMinMax;
                zombies.Add(zombie);
            }

            return zombies.ToArray();
        }
    }
}