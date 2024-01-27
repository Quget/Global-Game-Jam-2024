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
    public float speed = 5f;
    
    private Vector2 StartPosition;


    // Start is called before the first frame update
    void Start()
    {
        StartPosition = GrijpArm.position;
    }

    void MoveDown()
    {
       GrijpArm.MovePosition(PickupPoint.position);
    }

    void MoveUp()
    {
        GrijpArm.MovePosition(StartPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) MoveDown();
        if (Input.GetKeyDown(KeyCode.Alpha2)) MoveUp();
        
    } 
}
