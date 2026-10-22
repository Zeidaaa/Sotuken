using UnityEngine;

[CreateAssetMenu(fileName = "NewBagMasterData", menuName = "Equipment/Bag")]
public class BagMasterData : EquipmentMasterData
{
    public override int CategoryPrefix => 3;

    [SerializeField]
    private float m_bagBaseCollectRange;

    public float BagBaseCollectRange => m_bagBaseCollectRange;
}
