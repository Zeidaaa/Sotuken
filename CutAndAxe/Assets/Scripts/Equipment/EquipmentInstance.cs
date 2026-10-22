using UnityEngine;

public struct EquipmentInstance
{
    private int equipmentID;
    private int itemLevel;
    private int rarity;
    private NormalEffects normalEffects;

    public int EquipmentID => equipmentID;
    public int ItemLevel => itemLevel;
    public int Rarity => rarity;
    public NormalEffects NormalEffects => normalEffects;

    public EquipmentInstance(int _equipmentID, int _itemLevel, int _rarity, NormalEffects _normalEffects)
    {
        if (_equipmentID < EquipmentMasterData.CATEGORYCOLUMN) _equipmentID = 1001;
        equipmentID = _equipmentID;
        itemLevel = _itemLevel;
        rarity = _rarity;
        normalEffects = _normalEffects;
    }
}
