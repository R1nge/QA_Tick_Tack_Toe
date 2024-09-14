using System;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class DrawView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI drawText;
        [SerializeField] private Button restartButton;
        [Inject] private WebRequestsService _webRequestsService;
        [Inject] private TurnService _turnService;
        [Inject] private GameStateMachine _stateMachine; 

        private void Awake() => restartButton.onClick.AddListener(ResetBoard);

        private async void ResetBoard()
        {
            await _webRequestsService.ResetBoard();
            _turnService.ResetBoard();
            _stateMachine.SwitchState(GameStateType.Init);
        }

        private void Start() => ShowDrawText();

        private void ShowDrawText() => drawText.text = "Draw";
    }
}