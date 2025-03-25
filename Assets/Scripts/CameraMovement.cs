using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float shiftMult = 2f; // The multiplier when holding shift
    public float mouseSens = 100f;
    public float camAngle = 0;

    void Update()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= shiftMult;
        }

        // X controlled with A and D, or left and right arrows
        float stepX = Input.GetAxis("Horizontal");
        // Z controlled with W and S, or up and down arrows
        float stepZ = Input.GetAxis("Vertical");
        // Direct up and down movement uses Q and E
        float stepY = 0f;
        if (Input.GetKey(KeyCode.E)) stepY = 1f;
        if (Input.GetKey(KeyCode.Q)) stepY = -1f;

        Vector3 stepDirection = transform.right * stepX + transform.up * stepY + transform.forward * stepZ;
        transform.position += stepDirection * speed * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX); // Rotates Yaw, Horizontally

        // Rotates Pitch, Vertically
        camAngle -= mouseY;
        camAngle = Mathf.Clamp(camAngle, -80, 80); // Restricts the camera to between -90 and 90 degrees,
                                                   // which prevents flipping upside down
        transform.localEulerAngles = new Vector3(camAngle, transform.localEulerAngles.y, 0f); // Applies rotation in degrees rather
                                                                                              // than radians(easier to work with)

    }
}
