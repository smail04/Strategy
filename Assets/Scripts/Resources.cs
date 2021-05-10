using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    private static int money = 40;
    private static Text moneyText;
    public static int Money { get => money; set { money = value; UpdateMoneyText(); } }

    public static void UpdateMoneyText()
    {
        if (moneyText is null)
        {
            moneyText = GameObject.FindGameObjectWithTag("MoneyAmountText").GetComponent<Text>();
        }
        moneyText.text = "Money: " + money.ToString();
    }
}
