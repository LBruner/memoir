using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Core;
using RPG.Quests;
using RPG.Saving;
using RPG.UI;
using UnityEngine.SceneManagement;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;
        [SerializeField] Color playerColor;
        [SerializeField] Sprite playerImage;
        [SerializeField] Dialogue startingDialogue;
        Dialogue currentDialogue;

        DialogueNode currentNode = null;    //public
        AIConversant currentConversant = null; //public

        bool isChoosing = false;
        bool isTalking = false;

        public event Action onConversationUpdated;
        public List<Mover> movers = new List<Mover>();

        private void Awake()
        {
            // foreach (var quest in startingQuests)
            // {
            //     FindObjectOfType<QuestGiver>().GiveDefaultQuest(quest);
            // }
            // FindObjectOfType<QuestCompletion>().CompleteDefaultObjective(test);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
        private void OnEnable()
        {
            CancelInvoke();
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            SetMoveArrowState(false);

            isTalking = true;
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();

            TriggerEnterAction();
            if (onConversationUpdated != null)
                onConversationUpdated.Invoke();
        }

        public void SetMoveArrowState(bool state)
        {
            if (state)
            {
                if (movers.Count <= 0) return;

                foreach (Mover arrow in movers)
                {
                    arrow.gameObject.SetActive(true);
                }

                movers.Clear();
            }
            else
            {
                foreach (Mover arrow in FindObjectsOfType<Mover>())
                {
                    movers.Add(arrow);
                    arrow.gameObject.SetActive(false);
                }
            }
        }

        public void Quit()
        {
            SetMoveArrowState(true);
            FindObjectOfType<DialogueUI>().DisableDialogueImages();

            currentDialogue = null;
            TriggerExitAction();
            currentNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated?.Invoke();
            isTalking = false;
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public void SetIsChoosing(bool state)
        {
            isChoosing = state;
        }

        public bool IsTalking()
        {
            return isTalking;
        }

        public string GetText()
        {
            if (currentNode == null)
                return "";

            return currentNode.GetText();
        }

        public Sprite GetPlayerImage()
        {
            return playerImage;
        }

        public Sprite GetEnemyImage()
        {
            return currentConversant.GetImage();
        }

        public string GetCurrentConversantName()
        {
            if (isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetName();
            }
        }

        public Color GetCurrentConversantColor()
        {

            if (isChoosing)
            {
                return playerColor;
            }
            else
            {
                return currentConversant.GetColor();
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }

        public void Next()
        {
            int numPlayerResponses = FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count();
            onConversationUpdated?.Invoke();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated?.Invoke();
                return;
            }

            DialogueNode[] children = FilterOnCondition(currentDialogue.GetAIChildren(currentNode)).ToArray();

            int randomIndex = UnityEngine.Random.Range(0, children.Count());

            TriggerExitAction();
            currentNode = children[randomIndex];
            TriggerEnterAction();
            onConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                    yield return node;
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") { return; };

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
            foreach (DialogueTrigger trigger in currentConversant.GetComponentsInChildren<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
