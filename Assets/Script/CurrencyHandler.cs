using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyHandler : MonoBehaviour
{
    private static CurrencyHandler instance;
    public static CurrencyHandler Instance { get { return instance; } }
    [SerializeField] TMP_Text coinText;
    int coin;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            UpdateCoinText(coin);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCoin()
    {
        return coin;
    } 
    public void AddCoin(int val)
    {
        coin += val;
        UpdateCoinText(coin);
    }
    public void SpentCoin(int itemCost)
    {
        if (coin == 0 || itemCost > coin) return;

        coin -= itemCost;
        UpdateCoinText(coin);
    }
    private void UpdateCoinText(int val)
    {
        coinText.text = val.ToString();
    }
}
