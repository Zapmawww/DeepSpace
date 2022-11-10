
using UnityEngine;

class StoryTrigger : MonoBehaviour
{
    public bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        gameObject.SetActive(false);
    }
}