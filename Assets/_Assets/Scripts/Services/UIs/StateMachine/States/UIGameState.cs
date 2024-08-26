using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UIGameState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UIGameState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter() => _ui = _uiFactory.CreateUI(UIStateType.Game);

        public async UniTask Exit() => Object.Destroy(_ui);
    }
}