using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI conversantName;
        [SerializeField] TextMeshProUGUI AIText;
        PlayerConversant playerConversant;
        [SerializeField] Button nextButton;
        [SerializeField] Button quitButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] float typingSpeed = 0.1f;

        [SerializeField] GameObject popupDialogue;
        [SerializeField] GameObject popupParent;

        bool enableTypingEffect = false;
        int currentLetterIndex = 0;
        string oldConversantName = "";

        private void Start()
        {
            playerConversant = GameObject.FindWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() =>
            {
                SpawnDialoguePopups(playerConversant.GetText());

                playerConversant.Next();
            });

            quitButton.onClick.AddListener(() =>
            {
                foreach (Transform child in popupParent.GetComponentInChildren<Transform>())
                {
                    Destroy(child.gameObject);
                }
                playerConversant.Quit();
            });

            UpdateUI();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                return;
            }

            conversantName.text = playerConversant.GetCurrentConversantName();
            AIResponse.SetActive(!playerConversant.IsChosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChosing());

            if (playerConversant.IsChosing())
            {
                AIText.text = "";
                BuildChoiceList();
            }
            else
            {
                StartCoroutine(UpdateText(playerConversant));

                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        IEnumerator UpdateText(PlayerConversant conversant)
        {
            AIText.text = "";
            currentLetterIndex = 0;
            enableTypingEffect = false;

            string fullText = playerConversant.GetText();

            for (int i = 0; i < fullText.Length; i++)
            {
                currentLetterIndex++;

                if (currentLetterIndex == fullText.Length / 3)
                    enableTypingEffect = true;

                if (enableTypingEffect)
                {
                    AIText.text = fullText.Substring(0, i);
                    yield return new WaitForSeconds(typingSpeed);
                }
                else
                {
                    AIText.text = fullText.Substring(0, i);
                }
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);

                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();

                textComp.text = choice.GetText();

                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    SpawnDialoguePopups(choice.GetText());

                    playerConversant.SelectChoice(choice);
                });
            }
        }

        private void SpawnDialoguePopups(string textToDisplay)
        {
            Vector3 spawnPos = new Vector2(transform.position.x, transform.position.y + GetComponent<RectTransform>().rect.height / 2);
            GameObject popup = Instantiate(popupDialogue, popupParent.transform);

            popup.GetComponent<DialoguePopup>().SetUpPopup(textToDisplay, playerConversant.GetCurrentConversantName());
        }
    }
}
