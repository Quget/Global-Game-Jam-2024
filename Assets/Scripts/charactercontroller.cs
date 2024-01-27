using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    private GameObject item;

    [SerializeField]
    private Transform itemHolder;

    [SerializeField]
    private Animator DirectionsMovement;
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public Vector3 ItemOffset = new Vector3(-0.67f, 0.3f, 0);
    [SerializeField]
    private ItemController itemcontroller;

    public enum PlayerType { left, right };
    public PlayerType playerType;
    public bool HoldItem;
    public float LookDirectionX;
    public float LookDirectionY;
    
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2();
        HoldItem = false;

    }

    private void Update()
    {
        DirectionsMovement.SetFloat("DirectionX", LookDirectionX);
        DirectionsMovement.SetFloat("DirectionY", LookDirectionY);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (playerType)
        {
            case PlayerType.left:
                if (Input.GetKey(KeyCode.W)) direction.y = 1;
                if (Input.GetKey(KeyCode.S)) direction.y = -1;
                if (Input.GetKey(KeyCode.A)) direction.x = -1;
                if (Input.GetKey(KeyCode.D)) direction.x = 1;
                if (Input.GetKey(KeyCode.Space))
                {
                    HoldItem = true;
                    //Debug.Log("holditem left");

                }
                else if(item != null)
                {
                    
                    item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    DirectionsMovement.SetTrigger("Throw");
                    HoldItem = false;
                    item.transform.parent = null;
                    item = null;
                }

                break;
            case PlayerType.right:
                if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;
                if (Input.GetKey(KeyCode.DownArrow)) direction.y = -1;
                if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;
                if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
                if (Input.GetKey(KeyCode.RightShift))
                {
                    HoldItem = true;
                    //Debug.Log("holditem right");
                }
                else 
                {
                    if (item != null)
                    {
                        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        DirectionsMovement.SetTrigger("Throw");
                        
                        item.transform.parent = null;
                        item = null;
                    }
                    HoldItem = false;
                }
                break;
            default:
                //
                break;
        }

        rigidBody.MovePosition(rigidBody.position + direction * Time.deltaTime * speed);
        if (direction.x != 0) LookDirectionX = direction.x;
        if (direction.y != 0) LookDirectionY = direction.y;
        //loop animatie starten
        if (direction.x != 0 || direction.y != 0) DirectionsMovement.SetFloat("Movement", 1);
        //loopanimatie stoppen
        else DirectionsMovement.SetFloat("Movement", 0);
        direction.x = 0;
        direction.y = 0;
    }

private void OnCollisionStay2D(Collision2D collision)
{
        if (collision.gameObject.layer == LayerMask.NameToLayer("Itemlayer") 
            && HoldItem 
            && item == null)
        {

            Debug.Log("beetpakken");
            
            item = collision.gameObject;
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            item.transform.parent = itemHolder;
            item.transform.localPosition = Vector2.zero;
            DirectionsMovement.SetTrigger("Hold");
            

            // item.
            //
            //item.transform.position = item.transform.position + ItemOffset;
        }
    }
}
