using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string Action;
        [SerializeField] UnityEvent onTrigger;

        public void Trigger(string ActionToTrigger)
        {
            if (ActionToTrigger == Action)
            {
                onTrigger?.Invoke();
            }
        }
    }
}
