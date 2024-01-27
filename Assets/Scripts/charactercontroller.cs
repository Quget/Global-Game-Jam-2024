using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    public float speed = 5f;

    public enum PlayerType { left,right};
    public PlayerType playerType;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerType)
        {
            case PlayerType.left:
                if (Input.GetKey(KeyCode.W)) direction.y = 1;
                if (Input.GetKey(KeyCode.S)) direction.y = -1;
                if (Input.GetKey(KeyCode.A)) direction.x = -1;
                if (Input.GetKey(KeyCode.D)) direction.x = 1;
                break;
            case PlayerType.right:
                if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;
                if (Input.GetKey(KeyCode.DownArrow)) direction.y = -1;
                if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;
                if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
                break;
                    default:
                //
                break;
        }

        //Vector2 move = new Vector2(Input.GetAxis("Horizontal"),  Input.GetAxis("Vertical"));
        rigidBody.MovePosition(rigidBody.position + direction * Time.deltaTime * speed);
        //characterController.Move(direction * Time.deltaTime * speed);
        direction.x = 0;
        direction.y = 0;
    }
}
