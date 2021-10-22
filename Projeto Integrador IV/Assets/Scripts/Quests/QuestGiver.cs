using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;

        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();

            GameManager.Instance.AddQuest(quest.name);
            questList.AddQuest(quest);
        }

        public void UpdateUI()
        {
            GameManager.Instance.UpdateMenuQuest();
        }
    }
}
