using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 1f;
    private Rigidbody2D rb;
    private float turnDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
    {

        //Get Inputs from player
        //For turing
        if(Input.GetAxis("Horizontal") != 0)
            turnDir = Input.GetAxis("Horizontal") * -1;
        else
            turnDir = 0f;

        rb.AddTorque(turnDir * turnSpeed);
        
        //For moving forward
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * moveSpeed);
        } 

    }

}
