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

    [SerializeField] private Button equipButton;  // ���� ��ư
    [SerializeField] private Button useButton;  // ��� ��ư

    private List<UISlot> slots = new List<UISlot>();
    private Item selectedItem;  // ���� ���õ� ������
    private UISlot selectedSlot = null;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.MainMenu.gameObject.SetActive(true);
        });

        equipButton.onClick.AddListener(OnEquipButtonClicked);
        equipButton.gameObject.SetActive(false); // �ʱ⿣ ��Ȱ��ȭ

        useButton.onClick.AddListener(OnUseButtonClicked);
        useButton.gameObject.SetActive(false);
    }

    public void InitInventoryUI(List<Item> items)
    {
        // ���� ���� ����
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

        UpdateInventoryCountText(items.Count); // �κ��丮 ���� �ؽ�Ʈ ����
    }

    private void OnSlotClicked(Item item)
    {
        // ���õ� ������ ����
        selectedItem = item;

        // ���õ� ���� ǥ�� ����
        // ���� ���� ���� ���� ���� ����
        if (selectedSlot != null)
            selectedSlot.SetSelected(false);

        // �� ���� ���� ã�� (items�� ���� ����Ʈ�� �ε����� 1:1 ��Ī�� ���)
        selectedSlot = slots.Find(slot => slot.GetItem() == item);

        if (selectedSlot != null)
            selectedSlot.SetSelected(true);

        // ����/��� ��ư Ȱ��ȭ ���� �Ǵ�
        if (item != null)
        {
            equipButton.gameObject.SetActive(item.itemData.isEquipable);
            useButton.gameObject.SetActive(item.itemData.itemType == ItemType.Consumable);

            equipButton.GetComponentInChildren<TMP_Text>().text = item.IsEquipped ? "�����ϱ�" : "�����ϱ�";
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

        // ���� ��ư �ؽ�Ʈ ����
        equipButton.GetComponentInChildren<TMP_Text>().text = selectedItem.IsEquipped ? "�����ϱ�" : "�����ϱ�";

        // ���� UI ����
        UIManager.Instance.Status.SetCharacterInfo(GameManager.Instance.Player);
    }
    private void OnUseButtonClicked()
    {
        if (selectedItem != null && selectedItem.itemData.itemType == ItemType.Consumable)
        {
            GameManager.Instance.Player.Use(selectedItem);
            InitInventoryUI(GameManager.Instance.Player.Inventory);
            UIManager.Instance.Status.SetCharacterInfo(GameManager.Instance.Player); // ���� ����
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
