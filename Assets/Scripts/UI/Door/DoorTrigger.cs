using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private GameObject door;
    private bool isTriggered;

    public void Start()
    {
        door = GameObject.Find("first door");
    }
    public void Update()
    {
        if(isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                door.SetActive(!door.activeInHierarchy);
            }
        }
    }
    public void OnTriggerEnter(Collider other)     //Show button when triggered
    {
        UIManager.Instance.ShowDoorButton();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideDoorButton();
        isTriggered = false;
    }


}
