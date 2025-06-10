using UnityEngine;

public class Item
{
    public ItemData itemData { get; private set; }
    public int stackCount { get; private set; }
    public bool IsEquipped { get; private set; }

    // itemData에서 이름과 아이콘을 가져오기
    public string Name => itemData.name;
    public Sprite Icon => itemData.icon;

    public Item(ItemData data, int count = 1, bool isEquipped = false)
    {
        itemData = data;
        stackCount = count;
        IsEquipped = isEquipped;
    }

    public void Equip()
    {
        IsEquipped = true;
    }

    public void UnEquip()
    {
        IsEquipped = false;
    }

    public void AddStack(int amount)
    {
        stackCount += amount;
    }

    public void RemoveStack(int amount)
    {
        stackCount = Mathf.Max(stackCount - amount, 0);
    }
}
