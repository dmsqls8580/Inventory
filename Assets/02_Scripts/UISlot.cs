using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Image selected;
    [SerializeField] private Image equipMark; // 장착 여부 표시
    [SerializeField] private TMP_Text quantityText; // 아이템 개수 표시용

    private Item item;
    private bool isSelected = false;

    public event Action<Item> OnClick; // 클릭 시 아이템 정보를 전달

    public void SetItem(Item newItem)
    {
        item = newItem;
        RefreshUI();
        SetSelected(false);  // 초기에는 선택 해제 상태
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

        // 스택 가능한 아이템이면 개수 표시, 아니면 빈 문자열
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
