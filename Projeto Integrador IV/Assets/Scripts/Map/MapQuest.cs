using RPG.Control;
using RPG.Quests;
using RPG.Saving;
using RPG.UI.Quests;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Map
{
    public class MapQuest : MonoBehaviour, ISaveable, IRaycastable
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Quest quest;
        [SerializeField] GameObject blockedTooltip;
        [SerializeField] GameObject unlockedTooltip;
        [SerializeField] GameObject completedTooltip;
        [SerializeField] Image image;
        [SerializeField] Sprite blockedStatus;
        [SerializeField] Sprite unlockedStatus;
        [SerializeField] Sprite completedStatus;
        [SerializeField] QuestStatus questStatus;
        [SerializeField] QuestTooltipSpawner spawner;


        public void LoadScene()
        {
            GameManager.Instance.Load(sceneToLoad);
        }
        private void OnEnable()
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            switch (questStatus)
            {
                case QuestStatus.Unlocked:
                    image.sprite = unlockedStatus;
                    break;
                case QuestStatus.Blocked:
                    image.sprite = blockedStatus;
                    break;
                case QuestStatus.Complete:
                    image.sprite = completedStatus;
                    break;
            }
            SetSpawnObject();
        }

        public enum QuestStatus
        {
            Unlocked,
            Blocked,
            Complete
        }

        public void SetIsQuestFinished()
        {
            setQuestStatus(QuestStatus.Complete);
            SetSpawnObject();
        }
        public void setQuestStatus(QuestStatus status)
        {
            questStatus = status;
            UpdateImage();
        }

        public string GetQuestName()
        {
            return quest.name;
        }

        public MapQuest GetMapQuest(string Newquest)
        {
            if (quest.name == Newquest)
            {
                return this;
            }
            return null;
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        private void Start()
        {
            SetSpawnObject();
        }

        private void SetSpawnObject()
        {
            GameObject currentTooltip = blockedTooltip;

            if (questStatus == QuestStatus.Blocked)
            {
                currentTooltip = blockedTooltip;
            }
            if (questStatus == QuestStatus.Unlocked)
            {
                currentTooltip = unlockedTooltip;
            }
            if (questStatus == QuestStatus.Complete)
            {
                currentTooltip = completedTooltip;
            }

            spawner.tooltipPrefab = currentTooltip;
        }

        public object CaptureState()
        {
            return questStatus;
        }

        public void RestoreState(object state)
        {
            this.questStatus = (QuestStatus)state;
        }

        public CursorType GetCursorType()
        {
            if (questStatus != QuestStatus.Blocked)
            {
                return CursorType.Combat;
            }
            return CursorType.UI;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (questStatus != QuestStatus.Blocked)
            {
                GetComponent<Button>().enabled = true;
                return true;
            }
            GetComponent<Button>().enabled = false;
            return false;
        }
    }
}
