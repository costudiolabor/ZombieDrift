using UnityEngine;
using Zenject;

namespace Shop {
    public class EntryPoint : MonoBehaviour, IInitializable {
        [SerializeField] private ShopView _shopView;

        private ShopPresenter _shopPresenter;

        [Inject]
        public void Construct(ShopPresenter shopPresenter) {
            _shopPresenter = shopPresenter;
            _shopPresenter.Initialize(_shopView);
        }

        public void Initialize() =>
            _shopPresenter.Show();
        
    }
}