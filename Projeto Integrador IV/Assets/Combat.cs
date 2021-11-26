using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;
public class Combat : MonoBehaviour
{
    public void Fight()
    {
        FindObjectOfType<SavingWrapper>().Save();
        SceneManager.LoadScene(5);
    }
    public void Fight2()
    {
        FindObjectOfType<SavingWrapper>().Save();
        SceneManager.LoadScene(6);
    }
    public void Fight3()
    {
        FindObjectOfType<SavingWrapper>().Save();
        SceneManager.LoadScene(7);
    }
}
