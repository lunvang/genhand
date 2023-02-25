using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Transform cam;

    private float knockbackPower = 800;
    private float recoilPower = 17;
    private float camTorque = 10;
    private float longRangeCamTorque = 3;
    private float camTorqueCutoff = 15;
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
        resetRotation();
        pointToMouse();
    }

    void snapCamera()
    {
        cam.position = (Vector3)rigidBody.position + initCamPosition;
    }

    void listenToEvents()
    {
        if (Input.GetMouseButtonDown(0))
            shoot();
    }

    void shoot()
    {
        // knockback
        float knockbackDirection = (rigidBody.rotation + 180) * Mathf.Deg2Rad;
        rigidBody.AddForce(
            new Vector2(Mathf.Cos(knockbackDirection), Mathf.Sin(knockbackDirection)) * knockbackPower
        );
        // recoil
        float recoilStrength = recoilPower * rigidBody.inertia;
        rigidBody.AddTorque(recoilStrength, ForceMode2D.Impulse);
    }

    void resetRotation() // if rotation is more than full circle, substract 360
    {
        if (Mathf.Abs(rigidBody.rotation) > 360)
            rigidBody.rotation = rigidBody.rotation - (360 * Mathf.Sign(rigidBody.rotation));
    }

    void pointToMouse()
    {
        // calculate angle difference between desired angle and current angle
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition - objectPos;
        float targetAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        float objectAngle = rigidBody.rotation;
        float angleDiff = targetAngle - objectAngle;

        // if the angle is over 180 degrees, flip it
        if (Mathf.Abs(angleDiff) > 180)
            angleDiff = angleDiff - (360 * Mathf.Sign(angleDiff));

        // calculate impulse and apply it
        float impulseStrength = (angleDiff < camTorqueCutoff) ? camTorque : longRangeCamTorque;
        float impulse = angleDiff * Mathf.Deg2Rad * impulseStrength * rigidBody.inertia;
        rigidBody.AddTorque(impulse, ForceMode2D.Impulse);
    }
}
