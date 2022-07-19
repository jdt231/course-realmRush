using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 10;
    [SerializeField] Text healthText;
    [SerializeField] Text enemyText;


    void Start()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = baseHealth.ToString();
    }

    public void ReduceBaseHealth()
    {
        if (baseHealth > 1)
        {
            baseHealth --;
            UpdateHealthText();
            print("Base damaged!");
        }
        else 
        {
            baseHealth--;
            UpdateHealthText();
            print("Base destroyed, GAME OVER!");
        }

    }
}
