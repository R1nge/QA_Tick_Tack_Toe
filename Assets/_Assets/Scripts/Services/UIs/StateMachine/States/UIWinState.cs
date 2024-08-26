using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UIWinState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UIWinState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter() => _ui = _uiFactory.CreateUI(UIStateType.Win);

        public async UniTask Exit() => Object.Destroy(_ui);
    }
}