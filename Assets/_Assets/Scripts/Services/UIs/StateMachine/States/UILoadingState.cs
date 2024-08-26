using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UILoadingState : IAsyncState
    {
        private readonly UIFactory _uiFactory;
        private readonly UIStateMachine _uiStateMachine;
        private GameObject _ui;

        public UILoadingState(UIFactory uiFactory, UIStateMachine uiStateMachine)
        {
            _uiFactory = uiFactory;
            _uiStateMachine = uiStateMachine;
        }

        public async UniTask Enter()
        {
            _ui = _uiFactory.CreateUI(UIStateType.Loading);
        }

        public async UniTask Exit()
        {
            Object.Destroy(_ui);
        }
    }
}