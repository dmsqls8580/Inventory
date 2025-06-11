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

    public float Atk => baseAtk + GetEquipBonus(i => i.itemData.bonusAtk);
    public float Def => baseDef + GetEquipBonus(i => i.itemData.bonusDef);
    public float Hp => baseHp + GetEquipBonus(i => i.itemData.bonusHp);
    public float Cri => baseCri + GetEquipBonus(i => i.itemData.bonusCri);

    public Item EquippedWeapon { get; private set; }
    public Item EquippedArmor { get; private set; }
    public Item EquippedAccessory { get; private set; }

    public List<Item> Inventory { get; private set; } = new List<Item>();

    public Character(string name, int level, int gold, int atk, int def, int hp, int cri)
    {
        Name = name;
        Level = level;
        Gold = gold;
        baseAtk = atk;
        baseDef = def;
        baseHp = hp;
        baseCri = cri;
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    public void Equip(Item item)
    {
        if (!Inventory.Contains(item)) return;
        if (!item.itemData.isEquipable) return;

        switch (item.itemData.equipType)
        {
            case EquipType.Weapon:
                if (EquippedWeapon != null) EquippedWeapon.UnEquip();
                EquippedWeapon = item;
                break;
            case EquipType.Armor:
                if (EquippedArmor != null) EquippedArmor.UnEquip();
                EquippedArmor = item;
                break;
            case EquipType.Accessory:
                if (EquippedAccessory != null) EquippedAccessory.UnEquip();
                EquippedAccessory = item;
                break;
        }

        item.Equip();
    }

    public void UnEquip(Item item)
    {
        if (item == EquippedWeapon) { EquippedWeapon.UnEquip(); EquippedWeapon = null; }
        else if (item == EquippedArmor) { EquippedArmor.UnEquip(); EquippedArmor = null; }
        else if (item == EquippedAccessory) { EquippedAccessory.UnEquip(); EquippedAccessory = null; }
    }

    private float GetEquipBonus(System.Func<Item, float> statSelector)
    {
        float bonus = 0;

        if (EquippedWeapon != null) bonus += statSelector(EquippedWeapon);
        if (EquippedArmor != null) bonus += statSelector(EquippedArmor);
        if (EquippedAccessory != null) bonus += statSelector(EquippedAccessory);

        return bonus;
    }
}
