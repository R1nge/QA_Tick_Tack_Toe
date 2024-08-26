using System;
using _Assets.Scripts.Services.UIs.StateMachine.States;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly UIFactory _uiFactory;

        private UIStatesFactory(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public IAsyncState CreateState(UIStateType uiStateType, UIStateMachine uiStateMachine)
        {
            switch (uiStateType)
            {
                case UIStateType.Loading:
                    return new UILoadingState(_uiFactory, uiStateMachine);
                case UIStateType.Game:
                    return new UIGameState(_uiFactory);
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }
        }
    }
}