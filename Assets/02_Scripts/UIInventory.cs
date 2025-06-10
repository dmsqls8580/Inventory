using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private Button backButton;

    [SerializeField] private Button equipButton;  // ���� ��ư (�ν����Ϳ��� ����)

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

        // ���� ��ư Ȱ��ȭ ���� �Ǵ�
        if (item != null && item.itemData.isEquipable)
        {
            equipButton.gameObject.SetActive(true);
            equipButton.GetComponentInChildren<TMP_Text>().text = item.IsEquipped ? "�����ϱ�" : "�����ϱ�";
        }
        else
        {
            equipButton.gameObject.SetActive(false);
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

    private void RefreshAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.RefreshUI();
        }
    }
}
