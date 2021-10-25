﻿using System;
using RPG.Quests;
using TMPro;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] TextMeshProUGUI rewardText;

        public void Setup(QuestStatus status)
        {
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
                if (rewardText != "")
                {
                    rewardText += ", ";
                }
                rewardText += reward.name;
            }
            if (rewardText == "")
            {
                rewardText = "No reward.";
            }
            rewardText += ".";
            return rewardText;
        }

        public void SetupMenu(Quest quest)
        {
            if (title != null)
                title.text = quest.GetTitle();

            if (objectiveContainer != null)
            {
                foreach (Transform item in objectiveContainer)
                {
                    Destroy(item.gameObject);
                }

                foreach (var objective in quest.GetObjectives())
                {
                    GameObject prefab = objectiveIncompletePrefab;

                    GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                    TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                    objectiveText.text = objective.description;
                }
            }
            if (rewardText != null)
            {
                rewardText.text = GetRewardText(quest);
            }
        }
    }
}
