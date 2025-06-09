using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIMainMenu mainMenuUI;
    [SerializeField] private UIStatus statusUI;
    [SerializeField] private UIInventory inventoryUI;

    public UIMainMenu MainMenu => mainMenuUI;
    public UIStatus Status => statusUI;
    public UIInventory Inventory => inventoryUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        mainMenuUI.gameObject.SetActive(true);
        statusUI.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(false);
    }
}

