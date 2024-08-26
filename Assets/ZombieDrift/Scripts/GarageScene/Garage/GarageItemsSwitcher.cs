using System;
using System.Collections.Generic;

namespace Garage {
	public class GarageItemsSwitcher {
		public event Action BeforeSelectEvent, SelectedChangedEvent;
		public GarageItem selected => _garageData[_selectedIndex];

		public int selectedIndex {
			get => _selectedIndex;
			private set {
				_selectedIndex = value;
				SelectedChangedEvent?.Invoke();
			}
		}

		private GarageItem[] _garageData;
		private int _selectedIndex;

		public void SetData(GarageItem[] garageData) {
			_garageData = garageData;
		}

		public void Select(int index) {
			BeforeSelectEvent?.Invoke();
			selectedIndex = index;
		}

		public void MoveNext() {
			var nextIndex = _selectedIndex + 1;
			var isIndexValid = CheckIndexIsValid(nextIndex);
			if (!isIndexValid)
				return;
			Select(nextIndex);
		}

		public void MovePrevious() {
			var previousIndex = _selectedIndex - 1;
			var isIndexValid = CheckIndexIsValid(previousIndex);
			if (!isIndexValid)
				return;
			Select(previousIndex);
		}

		private bool CheckIndexIsValid(int index)
			=> index >= 0 && index < _garageData.Length;
	}
}
