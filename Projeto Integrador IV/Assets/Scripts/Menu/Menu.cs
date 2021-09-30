using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] List<GameObject> quests = new List<GameObject>();

    internal void UpdateQuests(List<string> availableQuests)
    {
        foreach (var item in quests)
        {
            foreach (var quest in availableQuests)
            {
                if (item.GetComponent<MenuQuest>().GetQuestName() == quest)
                {
                    item.SetActive(true);
                }
            }
        }
    }
}
