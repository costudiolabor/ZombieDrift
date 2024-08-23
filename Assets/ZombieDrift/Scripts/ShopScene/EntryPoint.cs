using UnityEngine;
using Zenject;

namespace Shop {
    public class EntryPoint : MonoBehaviour {
        [SerializeField] private ShopView _shopView;
        [SerializeField] private Transform _carParent;

        private ShopPresenter _shopPresenter;

        [Inject]
        public void Construct(ShopPresenter shopPresenter) {
            _shopPresenter = shopPresenter;
            _shopPresenter.Initialize(_shopView, _carParent);
        }
    }
}