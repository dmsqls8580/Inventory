using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not set. Please ensure GameManager is initialized before UIMainMenu.");
            return;
        }
    }

    private void Start()
    {
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    public void OpenStatus()
    {
        gameObject.SetActive(false);
        UIManager.Instance.Status.gameObject.SetActive(true);
    }

    public void OpenInventory()
    {
        gameObject.SetActive(false);
        UIManager.Instance.Inventory.gameObject.SetActive(true);
    }

    public void SetCharacterInfo(Character character)
    {
        nameText.text = $"{character.Name}";
        levelText.text = $"Lv.{character.Level}";
        goldText.text = $"{character.Gold}";
    }
}

