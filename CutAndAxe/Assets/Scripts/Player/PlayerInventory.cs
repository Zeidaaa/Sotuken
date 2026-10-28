using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<EquipmentInstance> m_equipmentInventory = new List<EquipmentInstance>();

    private EquipmentInstance m_equippedAxe;
    private EquipmentInstance m_equippedShoes;
    private EquipmentInstance m_equippedBag;

    public void AddEquipment(EquipmentInstance equipment)
    {
        m_equipmentInventory.Add(equipment);
    }

    public void RemoveItem(string instanceID)
    {
        m_equipmentInventory.RemoveAll(item => item.InstanceID == instanceID);
    }

    public void EquipAxe(string instanceID)
    {
        EquipmentInstance? targetItem = m_equipmentInventory.Find(item => item.InstanceID == instanceID);

        if (targetItem.HasValue) m_equippedAxe = targetItem.Value;
        else Debug.LogError("指定された装備が見つかりません。");
    }

    public void EquipShoes(string instanceID)
    {
        EquipmentInstance? targetItem = m_equipmentInventory.Find(item => item.InstanceID == instanceID);

        if (targetItem.HasValue) m_equippedShoes = targetItem.Value;
        else Debug.LogError("指定された装備が見つかりません。");
    }

    public void EquipBag(string instanceID)
    {
        EquipmentInstance? targetItem = m_equipmentInventory.Find(item => item.InstanceID == instanceID);

        if (targetItem.HasValue) m_equippedBag = targetItem.Value;
        else Debug.LogError("指定された装備が見つかりません。");
    }
}
