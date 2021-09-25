using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] string conversantName;
        [SerializeField] Dialogue dialogue = null;

        bool isClose = false;

        public CursorType GetCursorType()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            if (Vector3.Distance(player.position, this.transform.position) < 5f)
            {
                isClose = true;
            }
            else
            {
                isClose = false;
            }

            if (isClose)
                return CursorType.Dialogue;
            else
            {
                return CursorType.FarConversant;
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            if (Input.GetMouseButton(0) && isClose)
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}
