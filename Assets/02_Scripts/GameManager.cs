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
        Player = new Character("�밨�� ������", 7, 20000, 10, 8, 100, 3);

        // ���� ��������Ʈ�� ������ ���������� ��ü���ּ���
        Sprite dummyIcon1 = Resources.Load<Sprite>("Icons/axe");
        Sprite dummyIcon2 = Resources.Load<Sprite>("Icons/bottle");

        var item1 = new Item("����", dummyIcon1, false);
        var item2 = new Item("����", dummyIcon2, false);
        var item3 = new Item("�ķ�", dummyIcon2, false);

        Player.AddItem(item1);
        Player.AddItem(item2);
        Player.AddItem(item3);

        UIManager.Instance.MainMenu.SetCharacterInfo(Player);
        UIManager.Instance.Status.SetCharacterInfo(Player);
        UIManager.Instance.Inventory.InitInventoryUI(Player.Inventory);
    }
}
