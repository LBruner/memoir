using RPG.Quests;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        [SerializeField] bool isMenu = false;
        [SerializeField] Quest quest;

        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            if (isMenu)
                tooltip.GetComponent<QuestTooltipUI>().SetupMenu(quest);
            else
            {
                QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
                tooltip.GetComponent<QuestTooltipUI>().Setup(status);
            }
        }
    }
}
