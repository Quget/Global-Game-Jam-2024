using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;
    private Rigidbody2D rbTarget;
    private bool release;

    // Start is called before the first frame update
    void Start()
    {
        release = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Release(Rigidbody2D Target)
    {
        Debug.Log("weg gesmeten55");
        rbTarget = Target;
        release = true;
    }

    private void FixedUpdate()
    {
        if(release)
        {

            Vector2 target = (Vector2)rigidBody.transform.position - rbTarget.position;
            Debug.Log(target.normalized + ":" + target);
            rigidBody.AddForce(target.normalized * 750f, ForceMode2D.Force);
            //release = false;
        }
    }
}
