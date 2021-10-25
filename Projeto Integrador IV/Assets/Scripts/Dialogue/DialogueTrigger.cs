using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string Action;
        [SerializeField] UnityEvent action;

        public void Trigger(string ActionToTrigger)
        {
            if (ActionToTrigger == Action)
            {
                action?.Invoke();
                GameManager.Instance.UpdateMenuQuest();
            }
        }
    }
}
