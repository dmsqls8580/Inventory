using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private Button backButton;

    private List<UISlot> slots = new List<UISlot>();

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.MainMenu.gameObject.SetActive(true);
        });
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
            slot.OnClick += () =>
            {
                if (item.IsEquipped)
                {
                    GameManager.Instance.Player.UnEquip(item);
                }
                else
                {
                    GameManager.Instance.Player.Equip(item);
                }

                RefreshAllSlots(); // 모든 슬롯 UI 갱신
            };

            slots.Add(slot);
        }
    }

    private void RefreshAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.RefreshUI();
        }
    }
}

