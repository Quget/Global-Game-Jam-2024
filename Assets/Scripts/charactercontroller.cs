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
    public float speed = -0.47f;

    [SerializeField]
    public Vector3 ItemOffset = new Vector3(-0.67f, 0.3f, 0);

    [SerializeField]
    private ItemController itemcontroller;

    [SerializeField]
    private bool WalkStraight;

    [SerializeField]
    private float LoopHoekX = 0.35f;

    [SerializeField]
    private float LoopHoekY = 0.65f;

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
                else 
                {
                    ReleaseItem();
                }

                break;
            case PlayerType.right:
                if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;
                if (Input.GetKey(KeyCode.DownArrow)) direction.y = -1;
                if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;
                if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
                if (Input.GetKey(KeyCode.RightControl))
                {
                    HoldItem = true;
                    //Debug.Log("holditem right");
                }
                else
                {
                    ReleaseItem();
                }
                break;
            default:
                //
                break;
        }

        //kijkrichting van het hoofdje instellen
        if (direction.x < 0)
        {
            LookDirectionX = 0;
            LookDirectionY = -1;
        }
        if (direction.y > 0)
        {
            LookDirectionX = -1;
            LookDirectionY = 0;
        }
        if (direction.x > 0)
        {
            LookDirectionX = 0;
            LookDirectionY = 0;
        }
        if(direction.y < 0)
        {
            LookDirectionX = 1;
            LookDirectionY = -1;
        }
        //schuin lopen instellen als dit vinkje aan staat
        if (!WalkStraight)
        {
            //if (direction.x != 0)
            //{
            //    direction.x += LoopHoekX; 
            //}
            //if (direction.y != 0)
            //{
            //    direction.y += LoopHoekY; 
            //}
            //if ((direction.x != 0) || (direction.y != 0))
            {
                //bool yAangepast = false;
                //if (direction.x < 0)
                //{
                //    direction.y = -1 + LoopHoekX;
                //    yAangepast = true;
                //}

                //if (direction.x > 0)
                //{
                //    direction.y = 1 - LoopHoekX;
                //    yAangepast = true;
                //}
                //if (direction.y < 0 && !yAangepast)
                //{
                //    direction.x = 1 + LoopHoekY;
                //}
                //if (direction.y > 0 && !yAangepast)
                //{
                //    direction.x = -1 - LoopHoekY;
                //}
            }
        }

        rigidBody.MovePosition(rigidBody.position + direction * Time.deltaTime * speed);
        
        //loop animatie starten
        if (direction.x != 0 || direction.y != 0) DirectionsMovement.SetFloat("Movement", 1);
        //loopanimatie stoppen
        else DirectionsMovement.SetFloat("Movement", 0);
        direction.x = 0;
        direction.y = 0;

        void ReleaseItem()
        {
            if (item != null)
            {
                item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                DirectionsMovement.SetTrigger("Throw");

				item.transform.parent = null;
                item.transform.position = item.transform.position + (new Vector3(LookDirectionX, LookDirectionY,0) * 0.75f);
				item = null;
			}
            HoldItem = false;
        }
    }

private void OnCollisionStay2D(Collision2D collision)
{
        if (collision.gameObject.layer == LayerMask.NameToLayer("Itemlayer") 
            && HoldItem 
            && item == null)
        {

            //Debug.Log("beetpakken");
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
