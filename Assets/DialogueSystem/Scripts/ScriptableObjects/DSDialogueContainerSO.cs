using System.Collections.Generic;
using UnityEngine;

namespace DS.ScriptableObjects
{
    public class DSDialogueContainerSO : ScriptableObject
    {
        [field: SerializeField] public string FileName { get; set; }
        [field: SerializeField] public SerializableDictionary<DSDialogueGroupSO, List<DSDialogueSO>> DialogueGroups { get; set; }
        [field: SerializeField] public List<DSDialogueSO> UngroupedDialogues { get; set; }

        public void Initialize(string fileName)
        {
            FileName = fileName;

            DialogueGroups = new SerializableDictionary<DSDialogueGroupSO, List<DSDialogueSO>>();
            UngroupedDialogues = new List<DSDialogueSO>();
        }

        public List<string> GetDialogueGroupNames()
        {
            List<string> dialogueGroupNames = new List<string>();

            foreach (DSDialogueGroupSO dialogueGroup in DialogueGroups.Keys)
            {
                dialogueGroupNames.Add(dialogueGroup.GroupName);
            }
            return dialogueGroupNames;
        }

        public List<string> GetGroupedDialogueNodeNames(DSDialogueGroupSO dialogueGroup, bool startingDialoguesOnly = false)
        {
            List<DSDialogueSO> groupedNodes = DialogueGroups[dialogueGroup];

            List<string> groupedNodeNames = new List<string>();

            foreach(DSDialogueSO groupedNode in groupedNodes)
            {
                if (startingDialoguesOnly && !groupedNode.IsStartingDialogue)
                {
                    continue;
                }
                groupedNodeNames.Add(groupedNode.DialogueName);
            }

            return groupedNodeNames;
        }

        public List<string> GetUngroupedDialogueNodeNames(bool startingDialoguesOnly = false)
        {
            List<string> ungroupedDialogueNodeNames = new List<string>();
            foreach(DSDialogueSO ungroupedNode in UngroupedDialogues)
            {
                if (startingDialoguesOnly && !ungroupedNode.IsStartingDialogue)
                {
                    continue;
                }
                ungroupedDialogueNodeNames.Add(ungroupedNode.DialogueName);
            }
            return ungroupedDialogueNodeNames;
        }

        public string GetNodeGroupName(DSDialogueSO nodeSO)
        {
            foreach (KeyValuePair<DSDialogueGroupSO, List<DSDialogueSO>> dialogueGroup in DialogueGroups)
            {
                foreach(DSDialogueSO checkDialogue in dialogueGroup.Value)
                {
                    if (checkDialogue == nodeSO)
                    {
                        return dialogueGroup.Key.GroupName;
                    }
                }
            }
            return null;
        }
    }
}
