using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabberTriggerScript : MonoBehaviour
{
    [SerializeField]
    UnityEvent onTriggerEnter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        onTriggerEnter.Invoke();
    }
}
