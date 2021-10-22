using RPG.SceneManagement;
using UnityEngine;

namespace RPG.Map
{
    public class MapSave : MonoBehaviour
    {
        [SerializeField] GameObject saveMessage;
        [SerializeField] AudioClip saveClip;

        SavingWrapper wrapper;

        private void Start()
        {
            wrapper = FindObjectOfType<SavingWrapper>();
        }
        public void Save()
        {
            AudioSource.PlayClipAtPoint(saveClip, Camera.main.transform.position);
            saveMessage.GetComponent<Animator>().SetTrigger("TriggerMessage");
            wrapper.Save();
        }

        public void Load()
        {
            wrapper.Load();
        }
    }
}
