using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UIDrawState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public UIDrawState(UIFactory uiFactory) => _uiFactory = uiFactory;
        
        public async UniTask Enter() => _ui = _uiFactory.CreateUI(UIStateType.Draw);

        public async UniTask Exit() => Object.Destroy(_ui);
    }
}