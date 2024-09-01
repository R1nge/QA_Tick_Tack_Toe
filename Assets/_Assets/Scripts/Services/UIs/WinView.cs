using System;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class WinView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private Button restartButton;
        [Inject] private TurnService _turnService;
        [Inject] private WebRequestsService _webRequestsService;

        private void Awake() => restartButton.onClick.AddListener(ResetBoard);

        private void Start() => ShowWinText();

        private async void ResetBoard()
        {
            await _webRequestsService.ResetBoard();
            _turnService.ResetBoard();
        }

        private void ShowWinText()
        {
            winText.text = _turnService.CurrentTeam == TurnService.Team.O
                ? "O Won"
                : "X Won";
        }

        private void OnDestroy() => restartButton.onClick.RemoveListener(ResetBoard);
    }
}