using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;

        // [SerializeField] Quest[] testQuest;

        private void Start()
        {
            // foreach (Quest quest in testQuest)
            // {
            //     QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            //     questList.AddQuest(quest);
            // }
        }

        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.AddQuest(quest);
        }
    }
}
