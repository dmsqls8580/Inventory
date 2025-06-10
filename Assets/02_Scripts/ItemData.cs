using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Material
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Game/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxStack = 1; // �⺻ 1 (���� �������� 1, ���� �������� 64 ��)

    public bool isStackable = false; // ���� ������ ���������� ����
    public bool isEquipable = false; // ���� ������ ���������� ����

    [Header("Equipment Stats (if applicable)")]
    public float bonusAtk;
    public float bonusDef;
    public float bonusHp;
    public float bonusCri;
}
