using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{

    [SerializeField] private int startingBalance = 150;
    [SerializeField] private TextMeshProUGUI displayGold;
    [SerializeField] private int currentBalance;
    public int CurrentBalance { get => currentBalance; set => currentBalance = value; }

    private void Start()
    {
        currentBalance = startingBalance;
        UpdateBalanceDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBalanceDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBalanceDisplay();
        if (currentBalance < 0)
        {
            LoseGame();
        }
    }

    private void UpdateBalanceDisplay()
    {
        if (displayGold == null) { return; }
        displayGold.text = "Gold: " + currentBalance.ToString();
    }

    private static void LoseGame()
    {
        FindObjectOfType<SceneLoader>().LoadGameOverScene();
    }
}
