using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.UI;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] string conversantName;
        [SerializeField] Color conversantColor;
        [SerializeField] Dialogue dialogue = null;

        DialogueUI ui;

        private void Awake()
        {
            ui = FindObjectOfType<DialogueUI>();
        }

        public AIConversant(string name, Color color)
        {
            conversantName = name;
            conversantColor = color;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                PlayerConversant conversant = FindObjectOfType<PlayerConversant>();
                if (conversant != null)
                    conversant.StartDialogue(this, dialogue);
            }
            return true;
        }

        public string GetName()
        {
            return conversantName;
        }

        public Dialogue GetDialogue()
        {
            return dialogue;
        }

        public Color GetColor()
        {
            return conversantColor;
        }

        public void SetQuitButtonState(bool value)
        {
            DialogueUI.Instance.SetQuitButtonState(value);
        }

        public void SetActive(GameObject go)
        {
            go.SetActive(!go.activeSelf);
        }

        public void FadeAudio(float target)
        {
            StartCoroutine(FadeAudioSource.StartFade(GameObject.FindWithTag("Audio").GetComponent<AudioSource>(), 0.4f, target));
        }
    }
}
