using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door Instance; //Instantiating the Door.cs

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;

    public void showDoor1()
    {
        Debug.Log("close");
        door1.SetActive(true);
    }

    public void hideDoor1()
    {
        Debug.Log("open");
        door1.SetActive(false);
    }
}
