using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioClip[] sixtyNine;
        [SerializeField] private AudioClip[] holy;
        [SerializeField] private AudioClip[] devil;
        [SerializeField] private AudioClip[] leet;

        public AudioClip Get69(int index = -1)
        {
            if (index == -1)
            {
                return sixtyNine[Random.Range(0, sixtyNine.Length)];
            }

            return sixtyNine[index];
        }

        public AudioClip GetHoly(int index = -1)
        {
            if (index == -1)
            {
                return holy[Random.Range(0, holy.Length)];
            }

            return holy[index];
        }

        public AudioClip GetDevil(int index = -1)
        {
            if (index == -1)
            {
                return devil[Random.Range(0, devil.Length)];
            }

            return devil[index];
        }

        public AudioClip GetLeet(int index = -1)
        {
            if (index == -1)
            {
                return leet[Random.Range(0, leet.Length)];
            }

            return leet[index];
        }
    }
}