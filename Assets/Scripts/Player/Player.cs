using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    public static Player Instance; //Instantiating the MenuManager

    public int maxHealth = 100;
    public int currentHealth;

    public int maxOxygen = 10;
    public int currentOxygen;

    public HealthBar healthBar; //ÒýÓÃ½Å±¾HealthBar
    public OxygenBar oxygenBar;

    

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentOxygen = maxOxygen;
        oxygenBar.SetMaxOxygen(maxOxygen);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseOxygen(1);
        }

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void LoseOxygen(int lose)
    {
        currentOxygen -= lose;
        oxygenBar.SetOxygen(currentOxygen);
    }
}
