
using System;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public bool IsTriggered { get; private set; }
    public bool reuseable = false;
    public Action myCallback = null;
    private void OnTriggerEnter(Collider other)
    {
        IsTriggered = true;
        if (myCallback != null) myCallback();
        if (!reuseable) gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        IsTriggered = false;
    }
}