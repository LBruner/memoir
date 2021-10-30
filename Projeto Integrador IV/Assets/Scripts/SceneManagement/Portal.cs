using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Transition(sceneToLoad));
        }

        [SerializeField] int sceneToLoad = 1;
        [SerializeField] float fadeWaitTime = 1f;

        public IEnumerator Transition(int sceneToLoad)
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not defined");
            }

            DontDestroyOnLoad(this.gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(.5f);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            wrapper.Load();

            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(.5f);

            Destroy(gameObject);
        }
    }
}
