using System;
using System.Collections.Generic;
using Garage;

public class GarageModel {
	public event Action SelectedChangedNotify;

	public int count => _items.Count;
	public int moneyCount { get; set; }
	public int selectedIndex { get; set; }
	private readonly List<GarageItem> _items = new();
	private int _currentIndex;
	public GarageItem selected;
	
	public void AddData(GarageItem garageItem) {
		_items.Add(garageItem);
	}

	public int currentIndex {
		get => _currentIndex;
		private set {
			_currentIndex = value;
			SelectedChangedNotify?.Invoke();
		}
	}

	public void MoveNext() {
		var nextIndex = _currentIndex + 1;
		var isIndexValid = CheckIndexIsValid(nextIndex);
		if (!isIndexValid)
			return;
		Select(nextIndex);
	}

	public void MovePrevious() {
		var previousIndex = _currentIndex - 1;
		var isIndexValid = CheckIndexIsValid(previousIndex);
		if (!isIndexValid)
			return;
		Select(previousIndex);
	}
	public void Select(int index) =>
			selectedIndex = index;

	private bool CheckIndexIsValid(int index)
		=> index >= 0 && index < _items.Count;
}
