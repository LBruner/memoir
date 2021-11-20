using RPG.Quests;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        [SerializeField] string locationName;
        [SerializeField] bool isMenu = false;
        [SerializeField] Quest quest;
        [SerializeField] Quest[] locationQuests;

        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            if (isMenu)
            {
                int questNumber = locationQuests.Length;
                int completedQuests = 0;

                foreach (var quest in GameManager.Instance.GetCompletedQuests())
                {
                    Debug.Log("M");
                    if (locationQuests.Length <= 0) return;
                    foreach (var lc in locationQuests)
                    {
                        if (quest == lc.GetTitle())
                        {
                            completedQuests++;
                        }
                    }
                }

                tooltip.GetComponent<QuestTooltipUI>().SetupMenu(locationName, questNumber, completedQuests);
            }
            else
            {
                Debug.Log("O");
                QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
                tooltip.GetComponent<QuestTooltipUI>().Setup(status);
            }
        }
    }
}

