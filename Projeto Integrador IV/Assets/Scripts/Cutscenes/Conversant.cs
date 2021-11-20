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
        // bool hasSpoken = false;
        int timesEnabled = -1;
        [SerializeField] int enabledOn = 0;
        private void Start()
        {
            player = FindObjectOfType<PlayerConversant>();
            SetHasSpoken();

            if (AIConversant != null && timesEnabled == enabledOn)
            {
                Debug.Log(enabledOn);
                Debug.Log(timesEnabled);
                AIConversant conversant = AIConversant.GetComponentInChildren<AIConversant>();
                player.StartDialogue(conversant, conversant.GetDialogue());
                // SetHasSpoken();

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
            timesEnabled++;
            // hasSpoken = true;
        }

        public object CaptureState()
        {
            return timesEnabled;
            // return hasSpoken;
        }

        public void RestoreState(object state)
        {
            timesEnabled = (int)state;
            // hasSpoken = (bool)state;
        }
    }
}
