using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string Action;
        [SerializeField] UnityEvent[] actions;

        public void Trigger(string ActionToTrigger)
        {
            if (ActionToTrigger == Action)
            {
                foreach (var trigger in actions)
                {
                    trigger?.Invoke();
                }
            }
        }
    }
}
