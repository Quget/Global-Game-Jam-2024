using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private Collider2D colider;
    private Rigidbody2D rbTarget;
    private bool release;
    public bool HoldByPlayer;

    // Start is called before the first frame update
    void Start()
    {
        release = false;
        HoldByPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Release(Rigidbody2D Target)
    {
        //Debug.Log("weg gesmeten55");
        rbTarget = Target;
        release = true;
    }

    private void FixedUpdate()
    {
        if(release)
        {

            Vector2 target = (Vector2)rigidBody.transform.position - rbTarget.position;
            rigidBody.gravityScale = 10;
            rigidBody.AddForce(target.normalized * -900f, ForceMode2D.Force);
            colider.isTrigger = false;
            release = false;
        }
    }
}
