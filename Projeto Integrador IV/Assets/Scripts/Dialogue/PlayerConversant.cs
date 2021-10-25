using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Core;
using RPG.Quests;
using RPG.Saving;
using RPG.UI;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour, ISaveable
    {
        [SerializeField] string playerName;
        [SerializeField] Color playerColor;
        [SerializeField] Quest[] startingQuests;
        [SerializeField] Dialogue startingDialogue;
        Dialogue currentDialogue;

        DialogueNode currentNode = null;    //public
        AIConversant currentConversant = null; //public

        bool isChoosing = false;
        bool isTalking = false;
        bool finishedStartingDialogue = false;

        public event Action onConversationUpdated;

        private void Awake()
        {
            // foreach (var quest in startingQuests)
            // {
            //     FindObjectOfType<QuestGiver>().GiveDefaultQuest(quest);
            // }
            // FindObjectOfType<QuestCompletion>().CompleteDefaultObjective(test);
        }

        private void Start()
        {
            if (!finishedStartingDialogue)
            {
                FindObjectOfType<PlayerConversant>().StartDialogue(new AIConversant("AlmA", Color.white), startingDialogue);
                finishedStartingDialogue = true;
            }
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
            isTalking = true;
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();

            TriggerEnterAction();
            if (onConversationUpdated != null)
                onConversationUpdated.Invoke();
        }

        public void Quit()
        {
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

        public object CaptureState()
        {
            return finishedStartingDialogue;
        }

        public void RestoreState(object state)
        {
            this.finishedStartingDialogue = (bool)state;
        }
    }
}
