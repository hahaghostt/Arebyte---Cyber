using UnityEngine;

namespace DS.Data.Save
{
    [System.Serializable]
    public class DSGroupSaveData
    {
        [field: SerializeField] public string GUID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
    }
}