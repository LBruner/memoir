using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using RPG.UI.Quests;
using UnityEngine;

public class MenuQuest : MonoBehaviour, ISaveable
{
    [SerializeField] string questName;
    [SerializeField] GameObject finishedQuestTooltip;
    [SerializeField] GameObject unfinishedQuestTooltip;

    [SerializeField] bool isQuestFinished = false;

    public void SetIsQuestFinished()
    {
        isQuestFinished = true;
        SetSpawnObject();
    }

    public string GetQuestName()
    {
        return questName;
    }

    private void Start()
    {
        SetSpawnObject();
    }

    private void SetSpawnObject()
    {
        GameObject currentTooltip;
        if (isQuestFinished)
        {
            currentTooltip = finishedQuestTooltip;
        }
        else
        {
            currentTooltip = unfinishedQuestTooltip;
        }

        GetComponent<QuestTooltipSpawner>().tooltipPrefab = currentTooltip;
    }

    public object CaptureState()
    {
        return isQuestFinished;
    }

    public void RestoreState(object state)
    {
        bool isFinished = (bool)state;

        isQuestFinished = isFinished;
    }
}
