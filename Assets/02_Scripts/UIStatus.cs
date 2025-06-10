using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private TMP_Text ATKText;
    [SerializeField] private TMP_Text DEFText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text CRIText;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not set. Please ensure GameManager is initialized before UIStatus.");
            return;
        }
        Character player = GameManager.Instance.Player;
        SetCharacterInfo(player);
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.MainMenu.gameObject.SetActive(true);
        });
    }

    public void SetCharacterInfo(Character character)
    {
        
    }
}

