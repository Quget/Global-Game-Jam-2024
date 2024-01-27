using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrijpArmController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D GrijpArm;
    [SerializeField]
    private Rigidbody2D Right_Player;
    [SerializeField]
    private Transform PickupPoint;
    [SerializeField]
    public float speed = 0.05f;
    public Vector3 OffSet = new Vector3(0,-0.77f,0);
    
    private Vector2 StartPosition;
    private enum move { down, up, none };
    private move GrijpBeweging;
    private Vector2 NewPosition;
    public bool grabbing;
    private GameObject Object;
    private Rigidbody2D ItemRigidBody;
    
    

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
        if (grabbing) Object.transform.position = GrijpArm.transform.position + OffSet;
        if((GrijpArm.position == StartPosition) && grabbing)
        {
            Debug.Log("wegsmijten");
            ItemRigidBody = Object.GetComponent<Rigidbody2D>();
            ItemRigidBody.gravityScale = 1;
            
        }
        Vector2 v = new Vector2(6f, -3f);
        ItemRigidBody.AddForce(v, ForceMode2D.Impulse);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Itemlayer"))
        {
            //grab object
            grabbing = true;
            Object = collision.gameObject;
            Object.transform.position = Object.transform.position + OffSet;

        }
    }
   
}
