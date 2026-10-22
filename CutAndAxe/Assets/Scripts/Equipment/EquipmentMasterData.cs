using UnityEngine;

public abstract class EquipmentMasterData : ScriptableObject
{
    [SerializeField]
    private int m_itemID;
    [SerializeField]
    private string m_equipmentName;
    [SerializeField]
    private Sprite m_icon;

    public static readonly int CATEGORYCOLUMN = 1000;
    public abstract int CategoryPrefix { get; }
    public int EquipmentID => (CategoryPrefix * CATEGORYCOLUMN) + m_itemID;

    public string EquipmentName => m_equipmentName;
    public Sprite Icon => m_icon;

#if UNITY_EDITOR
    protected virtual void Reset()
    {
        m_itemID = FindNextAvailableItemIDForCategory();
    }

    private int FindNextAvailableItemIDForCategory()
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:EquipmentMasterData");
        System.Collections.Generic.HashSet<int> usedIds = new System.Collections.Generic.HashSet<int>();

        foreach (string guid in guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            EquipmentMasterData asset = UnityEditor.AssetDatabase.LoadAssetAtPath<EquipmentMasterData>(path);

            if (asset != null && asset != this && asset.CategoryPrefix == this.CategoryPrefix)
            {
                usedIds.Add(asset.m_itemID);
            }
        }

        int nextId = 1;
        while (usedIds.Contains(nextId))
        {
            nextId++;
        }

        return nextId;
    }
#endif
}