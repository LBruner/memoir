using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] string conversantName;
        [SerializeField] Color conversantColor;
        [SerializeField] Sprite enemyImage;
        [SerializeField] Dialogue dialogue = null;

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

        public Sprite GetImage()
        {
            return enemyImage;
        }
    }
}
