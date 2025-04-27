using UnityEngine;
using TMPro;

public class CurrencyHandler : GenericMonoSingleton<CurrencyHandler>
{
    [SerializeField] private TMP_Text coinText;
    private int coin;

    private void Start()
    {
       UpdateCoinText(coin);
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
        if (coin == 0 || itemCost > coin)
        {
            return;
        }

        coin -= itemCost;
        UpdateCoinText(coin);
    }
    private void UpdateCoinText(int val)
    {
        coinText.text = val.ToString();
    }
}
