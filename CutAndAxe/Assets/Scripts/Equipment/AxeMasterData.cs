using UnityEngine;

[CreateAssetMenu(fileName = "NewAxeData", menuName = "Equipment/Axe")]
public class AxeMasterData : EquipmentMasterData
{
    public override int CategoryPrefix => 1;

    [SerializeField]
    private float m_axeBaseAttack;
    [SerializeField]
    private float m_axeBaseAttackSpeed;
    [SerializeField]
    private float m_axeBaseAttackRange;

    public float AxeBaseAttack => m_axeBaseAttack;
    public float AxeBaseAttackSpeed => m_axeBaseAttackSpeed;
    public float AxeBaseAttackRange => m_axeBaseAttackRange;
}