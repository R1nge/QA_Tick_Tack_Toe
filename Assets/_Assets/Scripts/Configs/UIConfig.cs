using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject loadingUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject drawUI;
        public GameObject LoadingUI => loadingUI;
        public GameObject GameUI => gameUI;
        public GameObject WinUI => winUI;
        public GameObject DrawUI => drawUI;
    }
}