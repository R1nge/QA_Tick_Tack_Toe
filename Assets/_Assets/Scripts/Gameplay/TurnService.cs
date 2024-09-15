using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Web;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class TurnService
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly WebRequestsService _webRequestsService;
        public Team CurrentTeam => _currentTeam;
        private Team _currentTeam = Team.O;
        private readonly Team[,] _board = new Team[3, 3];
        private readonly List<CellView> _cells = new List<CellView>();

        public event Action OnTurnCompleted;

        private TurnService(UIStateMachine uiStateMachine, WebRequestsService webRequestsService)
        {
            _uiStateMachine = uiStateMachine;
            _webRequestsService = webRequestsService;
        }

        public void AddCell(CellView cellView)
        {
            _cells.Add(cellView);
        }

        public void RemoveCell(CellView cellView)
        {
            _cells.Remove(cellView);
        }

        public void ResetBoard()
        {
            _cells.Clear();
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

        public void SetTeam(Team team)
        {
            _currentTeam = team;
        }

        public void SetTeam(int x, int y, Team team)
        {
            _board[x, y] = team;
        }

        public async Task MakeTurn(int x, int y)
        {
            if (!CheckWinOrDraw())
            {
                //SwitchTeam();
                OnTurnCompleted?.Invoke();

                await _webRequestsService.MakeTurn(x, y);
                await UpdateBoard();
            }
        }

        private async Task UpdateBoard()
        {
            var board = await _webRequestsService.GetBoard();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _cells[i * 3 + j].SetTeam(board[i][j]);
                }
            }

            CheckWinOrDraw();
        }

        public bool CheckWinOrDraw()
        {
            if (CalculateWinner())
            {
                _uiStateMachine.SwitchState(UIStateType.Win).Forget();
                return true;
            }
            else if (IsDraw())
            {
                _uiStateMachine.SwitchState(UIStateType.Draw).Forget();
                return true;
            }

            return false;
        }

        public bool CalculateWinner()
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

        public bool IsDraw()
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

        public enum Team : byte
        {
            None = 0,
            O = 1,
            X = 2
        }
    }
}