using System;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Gameplay
{
    public class TurnService
    {
        private readonly UIStateMachine _uiStateMachine;
        public Team CurrentTeam => _currentTeam;
        private Team _currentTeam = Team.O;
        private readonly Team[,] _board = new Team[3, 3];
        
        public event Action OnTurnCompleted; 

        private TurnService(UIStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }

        private void SwitchTeam()
        {
            if (_currentTeam == Team.O)
            {
                _currentTeam = Team.X;
            }
            else
            {
                _currentTeam = Team.O;
            }
        }

        public Team MakeTurn(int x, int y)
        {
            var currentTeam = _currentTeam;
            _board[x, y] = _currentTeam;
            SwitchTeam();
            OnTurnCompleted?.Invoke();

            if (CalculateWinner())
            {
                _uiStateMachine.SwitchState(UIStateType.Win).Forget();
            }
            else if (IsDraw())
            {
                _uiStateMachine.SwitchState(UIStateType.Draw).Forget();
            }

            return currentTeam;
        }

        private bool CalculateWinner()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2] && _board[i, 0] != Team.None)
                {
                    return true;
                }
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (_board[0, i] == _board[1, i] && _board[1, i] == _board[2, i] && _board[0, i] != Team.None)
                {
                    return true;
                }
            }

            // Check diagonals
            if (_board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2] && _board[0, 0] != Team.None)
            {
                return true;
            }

            if (_board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0] && _board[0, 2] != Team.None)
            {
                return true;
            }

            return false;
        }
        
        private bool IsDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_board[i, j] == Team.None)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public enum Team
        {
            None,
            X,
            O
        }
    }
}