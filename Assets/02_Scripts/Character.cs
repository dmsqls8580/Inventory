using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string ID { get; private set; }
    public int Level { get; private set; }
    public int Gold { get; private set; }

    public Character(string id, int level, int gold)
    {
        ID = id;
        Level = level;
        Gold = gold;
    }
}

