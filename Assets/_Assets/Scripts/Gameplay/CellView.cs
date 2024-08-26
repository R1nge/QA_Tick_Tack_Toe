using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Vector2Int position;
        [Inject] private TurnService _turnService;
        private TurnService.Team _currentTeam;

        private void Awake() => button.onClick.AddListener(MakeTurn);

        public void MakeTurn()
        {
            _turnService.MakeTurn(position.x, position.y);
            _currentTeam = _turnService.CurrentTeam;
            UpdateView();
        }

        private void UpdateView()
        {
            if (_currentTeam == TurnService.Team.X)
            {
                text.text = "X";
            }
            else if (_currentTeam == TurnService.Team.O)
            {
                text.text = "O";
            }
        }

        private void OnDestroy() => button.onClick.RemoveListener(MakeTurn);
    }
}