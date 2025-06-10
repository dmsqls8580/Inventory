using System.Collections.Generic;

public class Character
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int Gold { get; private set; }

    private float baseAtk;
    private float baseDef;
    private float baseHp;
    private float baseCri;

    public float Atk => baseAtk + GetEquipBonusAtk();
    public float Def => baseDef + GetEquipBonusDef();
    public float Hp => baseHp + GetEquipBonusHp();
    public float Cri => baseCri + GetEquipBonusCri();

    public List<Item> Inventory { get; private set; } = new List<Item>();
    public List<Item> EquippedItems { get; private set; } = new List<Item>();

    public Character(string name, int level, int gold, int ATK, int DEF, int HP, int CRI)
    {
        Name = name;
        Level = level;
        Gold = gold;
        baseAtk = ATK;
        baseDef = DEF;
        baseHp = HP;
        baseCri = CRI;
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    public void Equip(Item item)
    {
        if (Inventory.Contains(item) && !EquippedItems.Contains(item))
        {
            item.Equip();
            EquippedItems.Add(item);
        }
    }

    public void UnEquip(Item item)
    {
        if (EquippedItems.Contains(item))
        {
            item.UnEquip();
            EquippedItems.Remove(item);
        }
    }

    private float GetEquipBonusAtk()
    {
        float bonus = 0;
        foreach (var item in EquippedItems)
        {
            bonus += item.itemData.bonusAtk; // itemData에 보너스 스탯 추가 필요
        }
        return bonus;
    }

    private float GetEquipBonusDef()
    {
        float bonus = 0;
        foreach (var item in EquippedItems)
        {
            bonus += item.itemData.bonusDef;
        }
        return bonus;
    }

    private float GetEquipBonusHp()
    {
        float bonus = 0;
        foreach (var item in EquippedItems)
        {
            bonus += item.itemData.bonusHp;
        }
        return bonus;
    }

    private float GetEquipBonusCri()
    {
        float bonus = 0;
        foreach (var item in EquippedItems)
        {
            bonus += item.itemData.bonusCri;
        }
        return bonus;
    }
}
