using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Image equipMark; // 장착 여부 표시
    [SerializeField] private TMP_Text nameText;

    private Item item;

    public event Action OnClick;

    public void SetItem(Item newItem)
    {
        item = newItem;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (item == null) return;

        icon.sprite = item.Icon;
        nameText.text = item.Name;
        equipMark.gameObject.SetActive(item.IsEquipped);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
