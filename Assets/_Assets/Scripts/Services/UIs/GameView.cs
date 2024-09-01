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

            var board = await _webRequestsService.GetBoard();
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cellViews[i * 3 + j].SetTeam(board[i][j]);
                }
            }
        }

        private void ShowCurrentTeam() => currentTeam.text = "Current Team: " + _turnService.CurrentTeam;
    }
}