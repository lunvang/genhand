using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.position = ((Vector3)myRigidBody.position) + new Vector3(0, 0, -3);
        if (Input.GetMouseButtonDown(0))
        {
            float direction = (myRigidBody.rotation + 180) *  Mathf.PI / 180;
            myRigidBody.velocity = new Vector3(
                Mathf.Cos(direction) * 20,
                Mathf.Sin(direction) * 20,
                0
            );
        }

        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object

        var object_pos = Camera.main.WorldToScreenPoint(myRigidBody.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
