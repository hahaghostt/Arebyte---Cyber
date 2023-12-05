using System.Collections.Generic;
using UnityEngine;

namespace DS.Data.Save
{
    using Enumerations;

    [System.Serializable]
    public class DSNodeSaveData
    {
        [field: SerializeField] public string GUID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public List<DSChoiceSaveData> Choices { get; set; }
        [field: SerializeField] public string GroupGUID { get; set; }
        [field: SerializeField] public DSDialogueType DialogueType { get; set; }
        [field: SerializeField] public Texture2D Texture { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
    }
}