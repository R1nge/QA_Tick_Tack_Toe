using System;
using System.Threading.Tasks;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Web;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentTeam;
        [SerializeField] private CellView[] cellViews;
        [SerializeField] private TextMeshProUGUI timerText;
        private float _time = 0f;
        private readonly float _updateTime = 0.1f;
        [Inject] private TurnService _turnService;
        [Inject] private WebRequestsService _webRequestsService;

        private async void Start()
        {
            _turnService.OnTurnCompleted += ShowCurrentTeam;

            for (int i = 0; i < cellViews.Length; i++)
            {
                _turnService.AddCell(cellViews[i]);
            }

            ShowCurrentTeam();
        }

        private async void Update()
        {
            if (_time <= 0)
            {
                _time = _updateTime;
                await Sync();
            }
            else
            {
                _time -= Time.deltaTime;
            }

            timerText.text = $"Time before sync: " + _time.ToString("0.00");
        }

        private async Task Sync()
        {
           var board = await _webRequestsService.GetBoard();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cellViews[i * 3 + j].SetTeam(board[i][j]);
                    _turnService.SetTeam(i, j, board[i][j]);
                }
            }

            var team = await _webRequestsService.GetLastTeam();
            _turnService.SetTeam(team);
            ShowCurrentTeam();
            _turnService.CheckWinOrDraw();
        }

        private void ShowCurrentTeam() => currentTeam.text = "Current Team: " + _turnService.CurrentTeam;
    }
}