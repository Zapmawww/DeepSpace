using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    public static Player Instance; //Instantiating the MenuManager

    public int maxHealth = 100;    //Maximum health value
    public int currentHealth;      //Current health value
     
    public int maxOxygen = 10;     //Maximum Oxygen value
    public int currentOxygen;      //Current Oxygen value

    public HealthBar healthBar; //Reference script HealthBar.cs
    public OxygenBar oxygenBar; //Reference script OxygenBar.cs



    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;     //Synchronised bar values
        healthBar.SetMaxHealth(maxHealth);

        currentOxygen = maxOxygen;
        oxygenBar.SetMaxOxygen(maxOxygen);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))  //Cheat button for testing, and reduce health values
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.L)) //Cheat button for testing, and reduce oxygen values
        {
            LoseOxygen(1);
        }

    }

    void TakeDamage(int damage)
    {  
        currentHealth -= damage; //reduce health values

        if (currentHealth < 0)    //Avoid reducing to a negative number
        {
            currentHealth = 0;
        }

        healthBar.SetHealth(currentHealth);   //Synchronised bar values
    }

    void LoseOxygen(int lose)
    {
        currentOxygen -= lose;   //reduce oxygen values

        if (currentOxygen < 0)    //Avoid reducing to a negative number
        {
            currentOxygen = 0;
        }

        oxygenBar.SetOxygen(currentOxygen);   //Synchronised bar values
    }
}
