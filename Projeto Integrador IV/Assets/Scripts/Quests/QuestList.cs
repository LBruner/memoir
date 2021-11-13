using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        List<QuestStatus> statuses = new List<QuestStatus>();

        public event Action onUpdate;

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest))
            {
                return;
            }
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            onUpdate?.Invoke();
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            if (status.IsComplete())
            {
                GiveRewards(quest);
            }
            if (status.IsObjectiveComplete(objective))
            {
                onUpdate?.Invoke();
                return;
            }
            status.CompleteObjective(objective);

            onUpdate?.Invoke();
        }

        private bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }
        private void GiveRewards(Quest quest)
        {
            foreach (var reward in quest.GetRewards())
            {
                if (reward.cards != null)
                {
                    foreach (var card in reward.cards)
                    {
                        FindObjectOfType<PlayerDeck>().AddCard(card);
                        Player.Instance.CardSaveManager.SavePlayerDeck(Player.Instance.Deck);
                    }
                }

                if (reward.items != null)
                {
                    foreach (var item in reward.items)
                    {
                        Player.Instance.Equip(item);
                    }
                }
            }
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();

            foreach (QuestStatus status in statuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null)
            {
                return;
            }
            statuses.Clear();
            foreach (object objectState in stateList)
            {
                statuses.Add(new QuestStatus(objectState));
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "HasntQuest":
                    return !HasQuest(Quest.GetByName(parameters[0]));
            }
            return null;
        }
    }
}
