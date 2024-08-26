using _Assets.Scripts.Gameplay;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentTeam;
        [Inject] private TurnService _turnService;
        private void Start()
        {
            _turnService.OnTurnCompleted += ShowCurrentTeam;
            ShowCurrentTeam();
        }

        private void ShowCurrentTeam() => currentTeam.text = "Current Team: " + _turnService.CurrentTeam;
    }
}