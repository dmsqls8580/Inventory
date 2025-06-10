using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Character
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int Gold { get; private set; }
    public float Atk { get; private set; }
    public float Def { get; private set; }
    public float Hp { get; private set; }
    public float Cri { get; private set; }

    public List<Item> Inventory { get; private set; } = new List<Item>();

    public Character(string name, int level, int gold, int ATK, int DEF, int HP, int CRI)
    {
        Name = name;
        Level = level;
        Gold = gold;
        Atk = ATK;
        Def = DEF;
        Hp = HP;
        Cri = CRI;
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    public void Equip(Item item)
    {
        if (Inventory.Contains(item))
        {
            item.Equip();
        }
    }

    public void UnEquip(Item item)
    {
        if (Inventory.Contains(item))
        {
            item.UnEquip();
        }
    }
}
