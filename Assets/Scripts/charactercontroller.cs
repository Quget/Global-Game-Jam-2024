using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private Animator DirectionsMovement;
    [SerializeField]
    public float speed = 5f;

    public enum PlayerType { left,right};
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
                else HoldItem = false;
               
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
                else HoldItem = false;
                break;
                    default:
                //
                break;
        }

        rigidBody.MovePosition(rigidBody.position + direction * Time.deltaTime * speed);
        if(direction.x != 0) LookDirectionX = direction.x;
        if (direction.y != 0) LookDirectionY = direction.y;
        //loop animatie starten
        if (direction.x != 0 || direction.y != 0) DirectionsMovement.SetFloat("Movement", 1);
        //loopanimatie stoppen
        else DirectionsMovement.SetFloat("Movement", 0);
        direction.x = 0;
        direction.y = 0;
    }
}
