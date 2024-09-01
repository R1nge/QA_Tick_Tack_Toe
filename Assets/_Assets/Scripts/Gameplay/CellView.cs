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

        public async void MakeTurn()
        {
            if (_currentTeam != TurnService.Team.None)
            {
                Debug.LogWarning($"Already has a team on the cell with position {position.x} {position.y}");
                return;
            }

            await _turnService.MakeTurn(position.x, position.y);
            UpdateView();
        }
        
        public void SetTeam(TurnService.Team team)
        {
            _currentTeam = team;
            UpdateView();
        }

        public void UpdateView()
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