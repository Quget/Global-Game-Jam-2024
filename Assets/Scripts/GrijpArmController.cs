using Spine;
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

    [SerializeField]
    private Transform itemHolder;
    
    private GameObject item;

    [SerializeField]
    private BoxCollider2D TargetZone;

    [SerializeField]
    private float startY = 10;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float targetY;

    [SerializeField]
    private float force = 750;
    
    private Vector2 StartPosition;
    private enum move { down, up, none };
    private move GrijpBeweging;
    private Vector2 NewPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = GrijpArm.position;
        GrijpBeweging = move.none;
        NewPosition = StartPosition;
        
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
        
        //if((GrijpArm.position == StartPosition) && grabbing)
        
        if(GrijpArm.gameObject.transform.localPosition.y >= startY && GrijpBeweging == move.up)
		{
            animator.SetTrigger("Open");

			Debug.Log("wegsmijten");
            if (item != null)
            {
                // && grabbing
                var itemRigedBody = item.GetComponent<Rigidbody2D>();
				itemRigedBody.bodyType = RigidbodyType2D.Dynamic;

				//item.GetComponent<Collider2D>().enabled = true;
				StartCoroutine(DelayEnableCollision(item.GetComponent<Collider2D>()));
                Vector2 target = Vector2.down +new Vector2(Random.Range(-0.1f, 0.65f), 0);
                itemRigedBody.AddForce(target * force, ForceMode2D.Force);
				item.transform.parent = null;
				item = null;
			}
            GrijpBeweging = move.none;

		}

		if (GrijpArm.gameObject.transform.localPosition.y <= targetY && GrijpBeweging == move.down)
        {
			animator.SetTrigger("Close");
			MoveUp();
            if (item != null)
            {
				item.transform.parent = itemHolder;
				item.transform.localPosition = Vector2.zero;
			}
        }
	}

    private IEnumerator DelayEnableCollision(Collider2D collider2D)
	{
        yield return new WaitForSeconds(0.75f);
		collider2D.enabled = true;
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

	//private void OnTriggerEnter2D(Collider2D collision)
    private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.layer == LayerMask.NameToLayer("Itemlayer")
            && item == null
            && collision.gameObject.transform.parent == null)
		{
			item = collision.gameObject;
			var itemRigidbody = item.GetComponent<Rigidbody2D>();
			itemRigidbody.bodyType = RigidbodyType2D.Kinematic;
			itemRigidbody.velocity = Vector2.zero;
            itemRigidbody.angularVelocity = 0;
            collision.enabled = false;
			MoveDown();
        }
    }
}
