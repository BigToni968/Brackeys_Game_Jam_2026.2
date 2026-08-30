using System.Collections.Generic;
using Game.Abstraction;
using UnityEngine.UI;
using Game.Static;
using UnityEngine;
using Ink.Runtime;
using TMPro;

namespace Game.UI
{
    public class UI_Dialogue : MonoView
    {
        [Header("Ink Settings")] 
        [SerializeField] private TextAsset inkJSONAsset;

        [Header("UI References")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Canvas self;
        [SerializeField] private TextMeshProUGUI speakerNameText;
        [SerializeField] private TextMeshProUGUI dialogueMainText;
        [SerializeField] private Transform choicesArea;
        [SerializeField] private GameObject choiceButtonPrefab;

        private List<GameObject> currentChoiceButtons = new List<GameObject>();
        private Story currentStory;

        public void StartDialogue(string knotName)
        {
            currentStory ??= new(inkJSONAsset.text);

            currentStory.ChoosePathString(knotName);
            
            Show();
            ContinueStory();
        }

        public override void Show()
        {
            self.enabled = true;
            Singleton.Instance.IsPause = true;
        }

        public override void Hide()
        {
            self.enabled = false;
            Singleton.Instance.IsPause = false;
        }

        private void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                ParseAndDisplayText(currentStory.ContinueMaximally());
                DisplayChoices();
                return;
            }
            
            Hide();
        }

        private void ParseAndDisplayText(string rawText)
        {
            rawText = rawText.Trim();
            if (string.IsNullOrEmpty(rawText)) return;

            int colonIndex = rawText.IndexOf(":");

            if (colonIndex != -1)
            {
                var speakerName = rawText.Substring(0, colonIndex).Trim();
                var dialogueLine = rawText.Substring(colonIndex + 1).Trim();

                speakerNameText.text = speakerName.ToUpper();
                speakerNameText.transform.parent.gameObject.SetActive(true);
                dialogueMainText.text = "«" + dialogueLine + "»";
            }
            else
            {
                speakerNameText.transform.parent.gameObject.SetActive(false);
                dialogueMainText.text = rawText;
            }
        }

        private void DisplayChoices()
        {
            foreach (var btn in currentChoiceButtons)
                Destroy(btn);

            currentChoiceButtons.Clear();

            List<Choice> currentChoices = currentStory.currentChoices;

            if (currentChoices.Count == 0)
                CreateChoiceButton("Continue...", -1);
            else
                foreach (var choice in currentChoices)
                    CreateChoiceButton(choice.text, choice.index);
        }

        private void CreateChoiceButton(string text, int choiceIndex)
        {
            var buttonObj = Instantiate(choiceButtonPrefab, choicesArea);
            buttonObj.SetActive(true);
            currentChoiceButtons.Add(buttonObj);

            var tmpText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            tmpText.text = text;

            var btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }

        private void OnChoiceSelected(int choiceIndex)
        {
            if (choiceIndex != -1)
                currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }
}