using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TestingRepeat : MonoBehaviour {

    public Web web;

    public void Update()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        if (web.heyhey == true)
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Item(Clone)")
                {
                    obj.SetActive(false);
                    Debug.Log("Bye");
                }
            }
        }
    }
}
