using UnityEngine;
using RPG.SceneManagement;
using System.Collections;
using RPG.Control;
using UnityEngine.UI;
using System;
using RPG.Saving;
using RPG.Core;

namespace RPG.Dialogue
{
    public class Mover : MonoBehaviour, IRaycastable
    {
        GameObject thisCam;
        [SerializeField] GameObject destinationCam;

        private void Start()
        {
            if (destinationCam == null)
            {
                gameObject.SetActive(false);
                return;
            }

            thisCam = GetComponentInParent<Camera>().gameObject;
        }

        public void SwitchCamera()
        {
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(.3f);


            thisCam.GetComponent<MoverSavingHelper>().SetWasEnabled(false);
            destinationCam.GetComponent<MoverSavingHelper>().SetWasEnabled(true);

            destinationCam.gameObject.SetActive(true);
            GameManager.Instance.UpdateMenuQuest();
            FindObjectOfType<SavingWrapper>().Save();
            thisCam.gameObject.SetActive(false);

            yield return fader.FadeIn(.9f);
        }

        public CursorType GetCursorType()
        {
            return CursorType.Movement;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            return true;
        }
    }
}
