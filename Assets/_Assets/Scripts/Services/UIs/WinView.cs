using _Assets.Scripts.Gameplay;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class WinView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winText;
        [Inject] private TurnService _turnService;

        private void Start() => ShowWinText();

        private void ShowWinText()
        {
            winText.text = _turnService.CurrentTeam == TurnService.Team.O
                ? "O Won"
                : "X Won";
        }
    }
}