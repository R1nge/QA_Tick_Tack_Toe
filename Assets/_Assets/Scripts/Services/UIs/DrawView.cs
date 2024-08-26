using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class DrawView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI drawText;
        
        private void Start() => ShowDrawText();
        
        private void ShowDrawText() => drawText.text = "Draw";
    }
}