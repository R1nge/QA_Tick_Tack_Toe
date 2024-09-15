using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.Web;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private WebRequestsService _webRequestsService;

        private void Start()
        {
            _webRequestsService.OnMeme += Play;
        }

        public void Play(MemeType memeType)
        {
            AudioClip meme = null;
            switch (memeType)
            {
                case MemeType.SixtyNine:
                    meme = _configProvider.AudioConfig.Get69();
                    break;
                case MemeType.Holy:
                    meme = _configProvider.AudioConfig.GetHoly();
                    break;
                case MemeType.Devil:
                    meme = _configProvider.AudioConfig.GetDevil();
                    break;
                case MemeType.Leet:
                    meme = _configProvider.AudioConfig.GetLeet();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(memeType), memeType, null);
            }

            audioSource.PlayOneShot(meme);
        }

        private void OnDestroy()
        {
            _webRequestsService.OnMeme -= Play;
        }

        public enum MemeType : byte
        {
            SixtyNine = 0,
            Holy = 1,
            Devil = 2,
            Leet = 3
        }
    }
}