using System;
using BoneGame.System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoneGame.System.Sound
{
    public class BoneSoundManager : SingletonMonoBehaviour<BoneSoundManager>
    {
        [SerializeField] private AudioSource BGMSource;
        [SerializeField] private AudioSource SESource;

        protected override bool IsDontDestroyOnLoad()
        {
            return false;
        }

        public void PlayBGM(AudioClip clip)
        {
            BGMSource.clip = clip;
            BGMSource.Play();
        }

        public void PlaySE(AudioClip clip)
        {
            SESource.PlayOneShot(clip);
        }

        public async UniTask PlayBGM(string address)
        {
            AudioClip clip = await ResourceLoader.Load<AudioClip>(address);
            PlayBGM(clip);
        }

        public async UniTask PlaySE(string address)
        {
            AudioClip clip = await ResourceLoader.Load<AudioClip>(address);
            PlaySE(clip);
        }

        public void StopBGM()
        {
            BGMSource.Stop();
        }

        public void StopSE()
        {
            SESource.Stop();
        }
    }
}