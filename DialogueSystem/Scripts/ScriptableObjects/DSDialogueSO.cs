using System.Collections.Generic;
using UnityEngine;

namespace DS.ScriptableObjects
{
    using Enumerations;
    using Data;
    public class DSDialogueSO : ScriptableObject
    {
        [field: SerializeField] public Texture2D Texture { get; set; }
        [field: SerializeField] public string DialogueName { get; set; }
        [field: SerializeField] [field: TextArea()] public string Text { get; set; }
        [field: SerializeField] public List<DSDialogueChoiceData> Choices { get; set; }
        [field: SerializeField] public DSDialogueType DialogueType { get; set; }
        [field: SerializeField] public bool IsStartingDialogue { get; set; }

        public void Initialize(Texture2D texture, string dialogueName, string text, List<DSDialogueChoiceData>  choices, DSDialogueType dialogueType, bool isStartingDialogue)
        {
            Texture = texture;
            DialogueName = dialogueName;
            Text = text;
            Choices = choices;
            DialogueType = dialogueType;
            IsStartingDialogue = isStartingDialogue;
        }

        public DSDialogueSO GetChoice(int index, out string choiceName)
        {
            DSDialogueChoiceData selectedChoice = Choices[index];

            choiceName = selectedChoice.Text;
            return selectedChoice.NextDialogue;
        }

        public DSDialogueSO GetChoice(int index)
        {
            return GetChoice(index, out _);
        }

        public string[] GetChoicesAsStringArray()
        {
            string[] choices = new string[Choices.Count];
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i] = Choices[i].Text;
            }

            return choices;
        }
    }
}
