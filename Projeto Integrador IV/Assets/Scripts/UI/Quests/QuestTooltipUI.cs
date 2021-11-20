using System;
using RPG.Quests;
using TMPro;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI locationTitle;
        [SerializeField] TextMeshProUGUI completedObjectives;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] TextMeshProUGUI rewardText;

        bool noCardsLeft = false;

        public void Setup(QuestStatus status)
        {
            Debug.Log("OI");
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();

            foreach (Transform item in objectiveContainer)
            {
                Destroy(item.gameObject);
            }

            foreach (var objective in quest.GetObjectives())
            {
                GameObject prefab = objectiveIncompletePrefab;
                if (status.IsObjectiveComplete(objective.reference))
                {
                    prefab = objectivePrefab;
                }

                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;
            }
            rewardText.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetRewards())
            {
                if (reward.items != null)
                {
                    foreach (var item in reward.items)
                    {
                        if (rewardText != "")
                        {
                            rewardText += ", ";
                        }
                        rewardText += item.ItemName;
                    }
                }
            }

            if (rewardText == "")
            {
                rewardText = "No reward.";
            }
            rewardText += ", New cards";
            rewardText += ".";
            return rewardText;
        }

        public void SetupMenu(string locationName, int questNumber, int completedQuests)
        {
            if (locationTitle == null) { return; }
            locationTitle.text = locationName;
            completedObjectives.text = String.Format("Completed {0} of {1} side quests", completedQuests, questNumber);
        }
    }
}
