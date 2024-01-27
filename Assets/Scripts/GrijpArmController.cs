using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrijpArmController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D GrijpArm;
    [SerializeField]
    private Transform PickupPoint;
    [SerializeField]
    public float speed = 0.05f;
    
    private Vector2 StartPosition;
    private enum move { down, up, none };
    private move GrijpBeweging;
    private Vector2 NewPosition;
    public bool grabbing;
    private GameObject Object;
    

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = GrijpArm.position;
        GrijpBeweging = move.none;
        NewPosition = StartPosition;
        grabbing = false;
    }

    public void MoveDown()
    {
        GrijpBeweging = move.down;
    }

    public void MoveUp()
    {
        GrijpBeweging = move.up;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) MoveDown();
        if (Input.GetKeyDown(KeyCode.Alpha2)) MoveUp();
        if (grabbing) Object.transform.position = GrijpArm.position;
        
    }
    private void FixedUpdate()
    {
        if (GrijpBeweging == move.down)
        {
            NewPosition += new Vector2(0, -0.1f);
            GrijpArm.MovePosition(NewPosition);
        }
        
        if (GrijpBeweging == move.up)
        {
            NewPosition += new Vector2(0, +0.1f);
            GrijpArm.MovePosition(NewPosition);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GrijpBeweging = move.none;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "object")
        {
            //grab object
            grabbing = true;
            Object = collision.gameObject;

        }
    }
   
}
