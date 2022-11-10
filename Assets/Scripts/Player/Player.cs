using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


[RequireComponent(typeof(BasicCombatant))]
public class Player : MonoBehaviour
{
    public static Player Instance; //Instantiating the MenuManager

    public bool IsDead { get => myComb.HitPoint == 0; }

    public double CurrentHP => myComb.HitPoint;
    public double MaxHP => myComb.MaxHitPoint;

    public int maxOxygen = 10;     //Maximum Oxygen value
    public int currentOxygen;      //Current Oxygen value

    public OxygenBar oxygenBar; //Reference script OxygenBar.cs

    private BasicCombatant myComb;

    void Awake()
    {
        myComb = GetComponent<BasicCombatant>();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = maxOxygen - 1;
        //currentOxygen = maxOxygen;
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
        CombatSystem.AddCombatAct(myComb, myComb, new DamageDealer { RawValue = damage }, "Self damage test");
        //UIManager.Instance.SetHealth((int)myComb.HitPoint);   //Synchronised bar values
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
