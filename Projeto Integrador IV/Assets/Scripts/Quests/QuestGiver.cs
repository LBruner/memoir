using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;

        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();

            FindObjectOfType<GameManager>().AddQuest(quest.name);
            questList.AddQuest(quest);
        }

        public void UpdateUI()
        {
            FindObjectOfType<GameManager>().UpdateMenuQuest();
        }
    }
}
