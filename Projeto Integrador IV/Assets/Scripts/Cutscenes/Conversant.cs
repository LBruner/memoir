using UnityEngine;
using RPG.Dialogue;
using RPG.Saving;
using RPG.UI;

namespace RPG.Dialogue
{
    public class Conversant : MonoBehaviour, ISaveable
    {
        [SerializeField] GameObject AIConversant = null;
        [SerializeField] bool canRun = true;

        PlayerConversant player;
        bool hasSpoken = false;

        private void Start()
        {
            player = FindObjectOfType<PlayerConversant>();

            if (AIConversant != null && !hasSpoken)
            {
                AIConversant conversant = AIConversant.GetComponentInChildren<AIConversant>();
                player.StartDialogue(conversant, conversant.GetDialogue());
                SetHasSpoken();

                if (!canRun)
                    FindObjectOfType<DialogueUI>().SetQuitButtonState(false);
            }
        }

        public void EnableButtons()
        {
            FindObjectOfType<DialogueUI>().SetQuitButtonState(true);
        }

        public void SetHasSpoken()
        {
            hasSpoken = true;
        }

        public object CaptureState()
        {
            return hasSpoken;
        }

        public void RestoreState(object state)
        {
            hasSpoken = (bool)state;
        }
    }
}
