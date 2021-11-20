using System.Collections;
using System.Collections.Generic;
using RPG.Dialogue;
using UnityEngine;

namespace RPG.Quests
{
    public class AutoPlayTriggers : MonoBehaviour
    {
        private void Start()
        {
            foreach (DialogueTrigger trigger in GetComponentsInChildren<DialogueTrigger>())
            {
                trigger.GetTrigger()?.Invoke();
            }
        }
    }
}
