using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Quests;
using RPG.Saving;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField] List<string> availableQuests = new List<string>();
    [SerializeField] List<string> completedQuests = new List<string>();

    private void OnEnable()
    {
        UpdateMenuQuest();
    }

    private void Start()
    {
        UpdateMenuQuest();
    }

    public void CompleteQuest(string questName)
    {
        if (completedQuests.Contains(questName)) return;
        completedQuests.Add(questName);
    }

    public void UpdateMenuQuest()
    {
        Menu menu = FindObjectOfType<Menu>();

        if (menu != null)
        {
            menu.UpdateQuests(availableQuests);

            foreach (MenuQuest quest in FindObjectsOfType<MenuQuest>())
            {
                foreach (string item in completedQuests)
                {
                    if (quest.GetQuestName() == item)
                    {
                        quest.SetIsQuestFinished();
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(LoadNextScene(1));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(LoadNextScene(0));
        }
    }

    IEnumerator LoadNextScene(int index)
    {
        Fader fader = FindObjectOfType<Fader>();

        yield return fader.FadeOut(.5f);

        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

        wrapper.Save();

        yield return SceneManager.LoadSceneAsync(index);
        wrapper.Save();

        yield return new WaitForSeconds(.5f);
        fader.FadeIn(.5f);

        UpdateMenuQuest();
    }

    public void AddQuest(string quest)
    {
        if (!availableQuests.Contains(quest))
            availableQuests.Add(quest);
    }

    [System.Serializable]
    struct QuestSaveData
    {
        public List<string> savedQuests;
        public List<string> savedCompletedQuests;
    }

    public object CaptureState()
    {
        QuestSaveData data = new QuestSaveData();
        data.savedQuests = this.availableQuests;
        data.savedCompletedQuests = this.completedQuests;
        return data;
    }

    public void RestoreState(object state)
    {
        QuestSaveData data = (QuestSaveData)state;
        List<string> savedCompletedQuests = state as List<string>;

        if (data.savedQuests == null && data.savedCompletedQuests == null) return;

        availableQuests.Clear();
        foreach (string objectState in data.savedQuests)
        {
            availableQuests.Add(objectState);
        }

        completedQuests.Clear();
        foreach (string objectState in data.savedCompletedQuests)
        {
            completedQuests.Add(objectState);
        }
    }
}
