using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

namespace RPG.Core
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] AudioMixer mixer;

        [SerializeField] TMP_Dropdown resolutionDropdown;
        [SerializeField] int targetFPS;

        Resolution[] resolutions;

        bool capFramerate = false;

        private void Start()
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            CapFramerate();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetVolume(float volume)
        {
            mixer.SetFloat("volume", volume);
        }

        public void CapFramerate()
        {
            capFramerate = !capFramerate;

            if (capFramerate)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = targetFPS;
            }
            else
            {
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = -1;
            }
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}
