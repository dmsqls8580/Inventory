using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Image selected;
    [SerializeField] private Image equipMark; // ���� ���� ǥ��
    [SerializeField] private TMP_Text quantityText; // ������ ���� ǥ�ÿ�

    private Item item;
    private bool isSelected = false;

    public event Action<Item> OnClick; // Ŭ�� �� ������ ������ ����

    public void SetItem(Item newItem)
    {
        item = newItem;
        RefreshUI();
        SetSelected(false);  // �ʱ⿡�� ���� ���� ����
    }

    public void RefreshUI()
    {
        if (item == null)
        {
            icon.sprite = null;
            icon.enabled = false;
            equipMark.gameObject.SetActive(false);
            quantityText.text = "";
            selected.gameObject.SetActive(false);
            return;
        }

        icon.enabled = true;
        icon.sprite = item.Icon;
        equipMark.gameObject.SetActive(item.IsEquipped);

        // ���� ������ �������̸� ���� ǥ��, �ƴϸ� �� ���ڿ�
        if (item.itemData.isStackable)
        {
            quantityText.text = item.stackCount > 1 ? item.stackCount.ToString() : "";
        }
        else
        {
            quantityText.text = "";
        }
        selected.gameObject.SetActive(isSelected);
    }
    public void SetSelected(bool selectedState)
    {
        isSelected = selectedState;
        selected.gameObject.SetActive(isSelected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(item);
    }
    public Item GetItem()
    {
        return item;
    }
}
