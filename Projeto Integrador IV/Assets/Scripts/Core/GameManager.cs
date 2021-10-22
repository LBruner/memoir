using System.Collections;
using System.Collections.Generic;
using RPG.Map;
using RPG.Saving;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField] List<string> allQuests = new List<string>();
    [SerializeField] List<string> availableQuests = new List<string>();
    [SerializeField] List<string> completedQuests = new List<string>();

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        UpdateMenuQuest();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(LoadNextScene(1));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Load(0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateMenuQuest();
        }
    }

    public void UpdateMenuQuest()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
        {
            SetQuestStatus();
        }
    }

    private void SetQuestStatus()
    {
        foreach (var quest in availableQuests)
        {
            if (allQuests.Contains(quest))
            {
                foreach (var item in FindObjectsOfType<MapQuest>())
                {
                    if (item.GetMapQuest(quest) != null)
                    {
                        item.setQuestStatus(MapQuest.QuestStatus.Unlocked);
                    }
                }
            }
            else
            {
                foreach (var item in FindObjectsOfType<MapQuest>())
                {
                    if (item.GetMapQuest(quest) != null)
                    {
                        item.setQuestStatus(MapQuest.QuestStatus.Blocked);
                    }
                }
            }
        }

        foreach (MapQuest quest in FindObjectsOfType<MapQuest>())
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

    public void Load(int index)
    {
        StartCoroutine(LoadNextScene(index));
    }

    public IEnumerator LoadNextScene(int index)
    {
        Fader fader = FindObjectOfType<Fader>();

        yield return fader.FadeOut(.5f);

        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

        wrapper.Save();

        yield return SceneManager.LoadSceneAsync(index);

        wrapper.Save();

        fader.FadeIn(.5f);

        UpdateMenuQuest();
    }

    public void AddQuest(string quest)
    {
        if (!availableQuests.Contains(quest))
        {
            availableQuests.Add(quest);
            UpdateMenuQuest();
        }
    }

    public void CompleteQuest(string questName)
    {
        if (completedQuests.Contains(questName)) return;
        completedQuests.Add(questName);
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
