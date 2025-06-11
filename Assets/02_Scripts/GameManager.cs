using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Character Player { get; private set; }

    [System.Serializable]
    public class InitialItemEntry
    {
        public ItemData itemData;
        public int quantity = 1;
    }

    [Header("초기 아이템 리스트")]
    [SerializeField] private List<InitialItemEntry> startingItems = new List<InitialItemEntry>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetData();
    }

    private void SetData()
    {
        // 캐릭터 초기화
        Player = new Character("Chad", 7, 20000, 10, 8, 100, 3);

        // 인스펙터에서 등록한 초기 아이템들을 인벤토리에 추가
        foreach (var entry in startingItems)
        {
            var item = new Item(entry.itemData, entry.quantity);
            Player.AddItem(item);
        }

        // UI 연결
        UIManager.Instance.MainMenu.SetCharacterInfo(Player);
        UIManager.Instance.Status.SetCharacterInfo(Player);
        UIManager.Instance.Inventory.InitInventoryUI(Player.Inventory);
    }
}
