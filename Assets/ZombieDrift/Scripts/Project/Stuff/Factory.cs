using UnityEngine;
using Zenject;

public class Factory {
	private readonly DiContainer _diContainer;

	public Factory(DiContainer diContainer) {
		_diContainer = diContainer;
	}

	public T CreateAndBind<T>(string path) where T : Object =>
		_diContainer.InstantiatePrefabResourceForComponent<T>(path);

	public T CreateAndBind<T>(T prefab, Transform parent = null, Vector3 position = default, Quaternion rotation = default) where T : Object =>
		_diContainer.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);


	public T Create<T>(string path) where T : Object {
		var prefab = Resources.Load<T>(path);
		return Create<T>(prefab);
	}

	public T Create<T>(T prefab, Transform parent = null, Vector3 position = default, Quaternion rotation = default) where T : Object =>
		Object.Instantiate(prefab, position, rotation, parent);

}