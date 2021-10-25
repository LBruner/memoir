using UnityEngine;
using RPG.SceneManagement;
using System.Collections;
using RPG.Control;

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
            yield return fader.FadeOut(.5f);
            destinationCam.SetActive(true);
            thisCam.SetActive(false);
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
