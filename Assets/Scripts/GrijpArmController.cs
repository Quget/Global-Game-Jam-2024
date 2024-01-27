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
    [SerializeField]
    private Transform itemHolder;private GameObject item;
    [SerializeField]
    private BoxCollider2D TargetZone;
    public Vector3 OffSet = new Vector3(0,-0.77f,0);
    
    private Vector2 StartPosition;
    private enum move { down, up, none };
    private move GrijpBeweging;
    private Vector2 NewPosition;
    private bool grabbing;
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
        TargetZone.enabled = false;
    }

    public void MoveUp()
    {
        GrijpBeweging = move.up;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
            MoveDown();

        if (Input.GetKeyDown(KeyCode.Alpha2)) 
            MoveUp();
        
        if((GrijpArm.position == StartPosition) && grabbing)
        {
            Debug.Log("wegsmijten");
            if (item != null)
            {
                
                item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                item.transform.parent = null;
                item = null;
            }
            grabbing = false;

        }
       
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

    private void OnCollisionStay2d(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bovenkant"))
            {
            //weggooi code aanroepen

            //
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            item.transform.parent = null;
            item = null;
            //
            TargetZone.enabled= true;
            GrijpBeweging = move.none;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Itemlayer")
            && item == null)
        {
            //Debug.Log("beetpakken door grabber");
            item = collision.gameObject;
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            item.transform.parent = itemHolder;
            item.transform.localPosition = Vector2.zero;
            MoveUp();
        }
    }
}
