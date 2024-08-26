using System.Collections.Generic;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStateMachine : GenericAsyncStateMachine<GameStateType, IAsyncState>
    {
        private GameStateMachine(GameStatesFactory gameStatesFactory)
        {
            States = new Dictionary<GameStateType, IAsyncState>
            {
                { GameStateType.Init, gameStatesFactory.CreateAsyncState(GameStateType.Init, this) },
                { GameStateType.Game, gameStatesFactory.CreateAsyncState(GameStateType.Game, this) }
            };
        }
    }
}