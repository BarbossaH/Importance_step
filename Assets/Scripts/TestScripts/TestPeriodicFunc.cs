using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPeriodicFunc : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake called");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
    }

    void Start()
    {
        Debug.Log("Start called");
    }

    void OnDisable()
    {
        Debug.Log("OnDisable called");
    }
}
