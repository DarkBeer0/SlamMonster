using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Transform DefaultFloor;
    public Transform DefaultRoof;

    public float speed = 5f;
    private float movement = 0f;
    private float floorY = 0, roofY = 0;
    private bool status = true; // down - true, up - false
    
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (rigidBody == null)
            Debug.Log("No rigidbody");

        if (DefaultFloor != null)
            floorY = DefaultFloor.position.y + DefaultFloor.localScale.y / 2 + rigidBody.transform.localScale.y / 2;

        if (DefaultRoof != null)
            roofY = DefaultRoof.position.y - DefaultRoof.localScale.y / 2 - rigidBody.transform.localScale.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            status = !status;

        /*movement = Input.GetAxis("Horizontal");
        if (movement > 0f)
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        else if (movement < 0f)
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        else
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);  */
    }

    private void FixedUpdate()
    {
        if (status) { 
            rigidBody.position = new Vector2(rigidBody.position.x, floorY);
            rigidBody.transform.position += transform.right * speed * Time.deltaTime;
        }
        else { 
            rigidBody.position = new Vector2(rigidBody.position.x, roofY);
            rigidBody.transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private static void PlaceObjectInLevel(Transform obj, Transform level, float offSetX, bool isFloor = true)
    {

    }
}
