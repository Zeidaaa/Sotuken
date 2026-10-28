using System;

public struct EquipmentInstance
{
    private string instanceID;
    private int equipmentID;
    private int itemLevel;
    private int rarity;
    private NormalEffects normalEffects;

    public string InstanceID => instanceID;
    public int EquipmentID => equipmentID;
    public int ItemLevel => itemLevel;
    public int Rarity => rarity;
    public NormalEffects NormalEffects => normalEffects;

    public EquipmentInstance(int _equipmentID, int _itemLevel, int _rarity, NormalEffects _normalEffects)
    {
        instanceID = Guid.NewGuid().ToString();
        if (_equipmentID < EquipmentMasterData.CATEGORYCOLUMN) _equipmentID = 1001;
        equipmentID = _equipmentID;
        itemLevel = _itemLevel;
        rarity = _rarity;
        normalEffects = _normalEffects;
    }
}
