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
            int questNumber = locationQuests.Length;
            int completedQuests = 0;

            foreach (var quest in GameManager.Instance.GetCompletedQuests())
            {
                foreach (var lc in locationQuests)
                {
                    if (quest == lc.GetTitle())
                    {
                        completedQuests++;
                    }
                }
            }

            if (isMenu)
                tooltip.GetComponent<QuestTooltipUI>().SetupMenu(locationName, questNumber, completedQuests);
            else
            {
                QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
                tooltip.GetComponent<QuestTooltipUI>().Setup(status);
            }
        }
    }
}
