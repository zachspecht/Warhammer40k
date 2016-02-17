using UnityEngine;
using System.Collections;

//Basic class to control starship movement in the x, y, and z directions.
public class StarshipMovement : MonoBehaviour {

    //static variables
    public static readonly Vector3 SPEED = new Vector3(5.0f, 5.0f, 5.0f);

    //variables
    private float speed;
    private float rotationSpeed;
    private float inputX;
    private Vector3 rotation;
    private Vector3 velocity;
    private Rigidbody rigidBody;

	//Use this for initialization
	void Start () {
        speed = 2.0f;
        rotationSpeed = 3.0f;
        rotation = Vector3.zero;
        rigidBody = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.zero;

	}
	
	//Update is called once per frame
	void Update () {

        //move forward/backward
        inputX = Input.GetAxis("Vertical");

        //barrel roll
        rotation.z = -(Input.GetAxis("Horizontal"));

        //turn left/right
        rotation.y = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f) * rotationSpeed;

        //look up/dowbn
        rotation.x = -(Mathf.Clamp(Input.GetAxis("Mouse Y"), -1.0f, 1.0f)) * rotationSpeed;

        transform.Rotate(rotation);
        if (inputX > 0) {
            transform.position += transform.forward * Time.deltaTime * inputX * speed;
        }
    }

    //FixedUpdate is called once per physics interaction
    void FixedUpdate() {

    }
}
