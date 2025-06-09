using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.MainMenu.gameObject.SetActive(true);
        });
    }
}

