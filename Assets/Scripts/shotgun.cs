using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Transform cam;

    private float shotPower = 20;
    private Vector3 initCamPosition = new Vector3(0, 0, -3);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        snapCamera();
        listenToEvents();
    }

    private void FixedUpdate()
    {
        pointToMouse();
    }

    void snapCamera()
    {
        cam.position = (Vector3)rigidBody.position + initCamPosition;
    }

    void listenToEvents()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float recoilDirection = (rigidBody.rotation + 180) * Mathf.Deg2Rad;
            rigidBody.velocity += new Vector2(
                Mathf.Cos(recoilDirection) * shotPower,
                Mathf.Sin(recoilDirection) * shotPower
            );
        }
    }

    void pointToMouse()
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition - objectPos;
        float targetAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(rigidBody.rotation) > 360)
        {
            rigidBody.rotation = (Mathf.Abs(rigidBody.rotation) - 360) * Mathf.Sign(rigidBody.rotation);
        }
        float objectAngle = rigidBody.rotation;
        float angleDiff = targetAngle - objectAngle;
        if (Mathf.Abs(angleDiff) > 180)
        {
            angleDiff = (Mathf.Abs(angleDiff) - 360) * Mathf.Sign(angleDiff);
        }
        if (Mathf.Abs(angleDiff) > 90)
        {
            angleDiff = 90 * Mathf.Sign(angleDiff);
        } else if (Mathf.Abs(angleDiff) < 15)
        {
            angleDiff *= 2;
        }
        float impulse = (angleDiff * Mathf.Deg2Rad * 5) * rigidBody.inertia;
        rigidBody.AddTorque(impulse, ForceMode2D.Impulse);
    }
}
