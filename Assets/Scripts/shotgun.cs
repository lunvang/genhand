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
        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
