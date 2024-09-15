using UnityEngine;

namespace _Assets.Scripts.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private AudioConfig audioConfig;
        public UIConfig UIConfig => uiConfig;
        public AudioConfig AudioConfig => audioConfig;
    }
}