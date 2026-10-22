using UnityEngine;

[CreateAssetMenu(fileName = "NewShoesData", menuName = "Equipment/Shoes")]
public class ShoesMasterData : EquipmentMasterData
{
    public override int CategoryPrefix => 2;

    [SerializeField]
    private float m_shoesBaseMoveSpeed;

    public float ShoesBaseMoveSpeed => m_shoesBaseMoveSpeed;
}