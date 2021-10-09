using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<string> availableQuests = new List<string>();

    private void Start()
    {
        Menu menu = FindObjectOfType<Menu>();

        if (menu != null)
        {
            Debug.Log("FOJ");
            menu.UpdateQuests(availableQuests);
        }
    }
}
