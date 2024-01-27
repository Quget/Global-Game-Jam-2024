using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDelivery : MonoBehaviour
{
    public float conveyorSpeed = 2f;

    void OnTriggerStay2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(conveyorSpeed, 0f);
    }
}
