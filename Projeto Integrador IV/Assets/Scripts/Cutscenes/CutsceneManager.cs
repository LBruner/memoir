using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<float> timestamps = new List<float>();

    bool isPlaying = false;

    int jumpIndex = 0;
    int clipIndex = -1;

    [System.Serializable]
    public struct soundTriggers
    {
        public int index;
        public AudioClip clip;
    }

    public soundTriggers[] triggers;

    private void Start()
    {
        player.loopPointReached += StopPlaying();
    }

    private VideoPlayer.EventHandler StopPlaying()
    {
        Debug.Log("End");
        // Destroy(gameObject);
        return null;
    }

    private void Update()
    {
        Setup();
        CheckInput();
    }

    private void PlaySFX()
    {
        Debug.Log("L");
        foreach (var trigger in triggers)
        {
            Debug.Log(trigger.index);
            if (trigger.index == clipIndex)
            {
                Debug.Log("OI");
                audioSource.clip = trigger.clip;
                audioSource.Play();
            }
            else if (trigger.index == 999)
            {
                audioSource.Stop();
            }
        }
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCutscene();
            PlaySFX();
        }

        if (Input.GetKeyDown(KeyCode.M))
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

    private void StartCutscene()
    {
        isPlaying = true;
        player.Play();
        clipIndex++;
    }
    // [SerializeField] AudioSource audioSource;
    // [SerializeField] List<VideoClip> videoClipList;

    // private List<VideoPlayer> videoPlayerList;
    // private int videoIndex = 0;
    // int nextIndex = -1;

    // [System.Serializable]
    // public struct soundTriggers
    // {
    //     public int index;
    //     public AudioClip clip;
    // }

    // public soundTriggers[] triggers;

    // private void Update()
    // {
    //     CheckInput();
    // }

    // private void CheckInput()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         StartCoroutine(playVideo());
    //     }
    //     if (Input.GetKeyDown(KeyCode.V))
    //     {
    //         SetupNextVideo();
    //     }
    // }

    // private void SetupNextVideo()
    // {
    //     if (IsPrepared())
    //     {
    //         videoIndex++;
    //         StartCoroutine(playVideo(false));
    //         //TODO// if (videoIndex == videoPlayerList.Count - 1)
    //         //     //
    //     }
    // }

    // IEnumerator playVideo(bool firstRun = true)
    // {
    //     if (videoClipList == null || videoClipList.Count <= 0)
    //     {
    //         yield break;
    //     }

    //     foreach (var trigger in triggers)
    //     {
    //         if (trigger.index == videoIndex)
    //         {
    //             audioSource.clip = trigger.clip;
    //             audioSource.Play();
    //         }
    //     }

    //     if (firstRun)
    //     {
    //         SetupCutscene();
    //     }

    //     if (videoIndex >= videoPlayerList.Count)
    //         yield break;

    //     videoPlayerList[videoIndex].Prepare();

    //     while (!videoPlayerList[videoIndex].isPrepared)
    //     {
    //         yield return null;
    //     }

    //     videoPlayerList[videoIndex].Play();
    //     nextIndex = videoIndex + 1;

    //     if (nextIndex > videoPlayerList.Count)
    //         yield return null;

    //     while (videoPlayerList[videoIndex].isPlaying)
    //     {
    //         if (nextIndex <= videoPlayerList.Count - 1)
    //             videoPlayerList[nextIndex].Prepare();

    //         yield return null;
    //     }

    //     while (!IsPrepared())
    //     {
    //         yield return null;
    //     }
    // }

    // private void SetupCutscene()
    // {
    //     videoPlayerList = new List<VideoPlayer>();
    //     for (int i = 0; i < videoClipList.Count; i++)
    //     {
    //         GameObject vidHolder = new GameObject("VP" + i);
    //         vidHolder.transform.SetParent(transform);

    //         VideoPlayer videoPlayer = vidHolder.AddComponent<VideoPlayer>();
    //         videoPlayerList.Add(videoPlayer);

    //         AudioSource audioSource = vidHolder.AddComponent<AudioSource>();

    //         videoPlayer.playOnAwake = false;
    //         audioSource.playOnAwake = false;

    //         videoPlayer.source = VideoSource.VideoClip;
    //         videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
    //         videoPlayer.targetCamera = Camera.main;

    //         videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

    //         videoPlayer.EnableAudioTrack(0, true);
    //         videoPlayer.SetTargetAudioSource(0, audioSource);

    //         videoPlayer.clip = videoClipList[i];
    //     }
    // }

    // private bool IsPrepared()
    // {
    //     Debug.Log(videoPlayerList);
    //     Debug.Log(nextIndex);
    //     if (nextIndex > videoPlayerList.Count - 1)
    //         return false;
    //     return videoPlayerList[nextIndex].isPrepared;
    // }
}
