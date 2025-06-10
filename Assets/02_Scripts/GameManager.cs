using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Character Player { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        Player = new Character("용감한 정복자", 7, 20000, 10, 8, 100, 3);

        // 더미 스프라이트는 적절한 아이콘으로 대체해주세요
        Sprite dummyIcon1 = Resources.Load<Sprite>("Icons/axe");
        Sprite dummyIcon2 = Resources.Load<Sprite>("Icons/bottle");

        var item1 = new Item("도끼", dummyIcon1, false);
        var item2 = new Item("물통", dummyIcon2, false);
        var item3 = new Item("식량", dummyIcon2, false);

        Player.AddItem(item1);
        Player.AddItem(item2);
        Player.AddItem(item3);

        UIManager.Instance.MainMenu.SetCharacterInfo(Player);
        UIManager.Instance.Status.SetCharacterInfo(Player);
        UIManager.Instance.Inventory.InitInventoryUI(Player.Inventory);
    }
}
