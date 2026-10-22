using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentDataBase", menuName = "EquipmentDataBase")]
public class EquipmentDataBase : ScriptableObject
{
    [SerializeField]
    private List<EquipmentMasterData> masterDatas;

    private Dictionary<int, EquipmentMasterData> equipmentDict;

    private void Initialize()
    {
        if (equipmentDict != null) return;

        equipmentDict = new Dictionary<int, EquipmentMasterData>();
        foreach (var data in masterDatas)
        {
            if (data != null && !equipmentDict.ContainsKey(data.EquipmentID))
            {
                equipmentDict.Add(data.EquipmentID, data);
            }
        }
    }

    public EquipmentMasterData Get(int equipmentID)
    {
        Initialize();
        equipmentDict.TryGetValue(equipmentID, out var data);
        return data;
    }
}
