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
    public int maxStack = 1; // 기본 1 (장착 아이템은 1, 스택 아이템은 64 등)

    public bool isStackable = false; // 스택 가능한 아이템인지 여부
    public bool isEquipable = false; // 장착 가능한 아이템인지 여부

    [Header("Equipment Stats (if applicable)")]
    public float bonusAtk;
    public float bonusDef;
    public float bonusHp;
    public float bonusCri;
}
