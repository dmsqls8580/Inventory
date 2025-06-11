using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text inventoryCountText;
    [SerializeField] private int maxInventoryCapacity = 150;

    [SerializeField] private Transform slotParent;
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private Button backButton;

    [SerializeField] private Button equipButton;  // 장착 버튼
    [SerializeField] private Button useButton;  // 사용 버튼

    private List<UISlot> slots = new List<UISlot>();
    private Item selectedItem;  // 현재 선택된 아이템
    private UISlot selectedSlot = null;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.MainMenu.gameObject.SetActive(true);
        });

        equipButton.onClick.AddListener(OnEquipButtonClicked);
        equipButton.gameObject.SetActive(false); // 초기엔 비활성화

        useButton.onClick.AddListener(OnUseButtonClicked);
        useButton.gameObject.SetActive(false);
    }

    public void InitInventoryUI(List<Item> items)
    {
        // 기존 슬롯 제거
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        foreach (var item in items)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.SetItem(item);

            slot.OnClick += OnSlotClicked;

            slots.Add(slot);
        }

        selectedItem = null;
        selectedSlot = null;
        equipButton.gameObject.SetActive(false);

        UpdateInventoryCountText(items.Count); // 인벤토리 수량 텍스트 갱신
    }

    private void OnSlotClicked(Item item)
    {
        // 선택된 아이템 설정
        selectedItem = item;

        // 선택된 슬롯 표시 관리
        // 먼저 기존 선택 슬롯 선택 해제
        if (selectedSlot != null)
            selectedSlot.SetSelected(false);

        // 새 선택 슬롯 찾기 (items와 슬롯 리스트의 인덱스가 1:1 매칭인 경우)
        selectedSlot = slots.Find(slot => slot.GetItem() == item);

        if (selectedSlot != null)
            selectedSlot.SetSelected(true);

        // 장착/사용 버튼 활성화 여부 판단
        if (item != null)
        {
            equipButton.gameObject.SetActive(item.itemData.isEquipable);
            useButton.gameObject.SetActive(item.itemData.itemType == ItemType.Consumable);

            equipButton.GetComponentInChildren<TMP_Text>().text = item.IsEquipped ? "해제하기" : "장착하기";
        }
        else
        {
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
    }


    private void OnEquipButtonClicked()
    {
        if (selectedItem == null) return;

        if (selectedItem.IsEquipped)
            GameManager.Instance.Player.UnEquip(selectedItem);
        else
            GameManager.Instance.Player.Equip(selectedItem);

        RefreshAllSlots();

        // 장착 버튼 텍스트 갱신
        equipButton.GetComponentInChildren<TMP_Text>().text = selectedItem.IsEquipped ? "해제하기" : "장착하기";

        // 스탯 UI 갱신
        UIManager.Instance.Status.SetCharacterInfo(GameManager.Instance.Player);
    }
    private void OnUseButtonClicked()
    {
        if (selectedItem != null && selectedItem.itemData.itemType == ItemType.Consumable)
        {
            GameManager.Instance.Player.Use(selectedItem);
            InitInventoryUI(GameManager.Instance.Player.Inventory);
            UIManager.Instance.Status.SetCharacterInfo(GameManager.Instance.Player); // 스탯 갱신
        }
    }

    private void RefreshAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.RefreshUI();
        }
    }

    private void UpdateInventoryCountText(int currentCount)
    {
        inventoryCountText.text = $"Inventory {currentCount}/{maxInventoryCapacity}";
    }
}
