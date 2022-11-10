using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOutside : MonoBehaviour
{
    /// <summary>
    /// Chose a scene that expected to switch
    /// </summary>
    public Scene scene;
    /// <summary>
    /// The gameObject contains player collider
    /// </summary>
    public GameObject playerObj;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObj)
            SceneManager.LoadScene(scene.name);
    }
}
