using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace RPG.Cutscenes
{
    public class CutsceneManager : MonoBehaviour
    {
        [SerializeField] VideoPlayer player;
        [SerializeField] AudioSource audioSource;
        [SerializeField] List<float> timestamps = new List<float>();

        bool isPlaying = false;

        AudioClip currentClip;
        public int jumpIndex = 0;
        public int clipIndex = -1;

        [System.Serializable]
        public struct soundTriggers
        {
            public int index;
            public AudioClip clip;
        }

        public soundTriggers[] triggers;

        public void StartCutscene()
        {
            player.loopPointReached += EndReached;

            isPlaying = true;
            player.Play();
            clipIndex++;

            PlaySFX();
        }

        private void Update()
        {
            Setup();
            CheckInput();
        }

        void EndReached(UnityEngine.Video.VideoPlayer vp)
        {
            vp.playbackSpeed = vp.playbackSpeed / 10.0F;
            SceneManager.LoadScene("Map");
        }

        private void Setup()
        {
            if (clipIndex > timestamps.Count - 1)
            {
                return;
            }

            if (clipIndex > -1 && player.time > timestamps[clipIndex] && isPlaying)
            {
                player.Pause();
                isPlaying = false;
            }
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SkipCutscene();
                PlaySFX();
            }
        }

        private void SkipCutscene()
        {
            if (!isPlaying)
            {
                jumpIndex++;
                clipIndex++;
                isPlaying = true;
                player.Play();
            }
            else
            {
                player.time = timestamps[jumpIndex];
                isPlaying = false;
                player.Pause();
            }
        }

        private void PlaySFX()
        {
            foreach (var trigger in triggers)
            {
                if (trigger.index == clipIndex)
                {
                    audioSource.clip = trigger.clip;
                    if (trigger.clip == currentClip)
                    {
                        return;
                    }
                    currentClip = trigger.clip;
                    audioSource.Play();
                }
                else if (trigger.index == 999)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}
