using UnityEngine;

public class MouseMovment : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottemClamp = 90f;
    void Start()
    {
        //Locking Mousecursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Geting mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation x Axis
        xRotation -= mouseY;

        //Clamp rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottemClamp);

        //Rotation x Axis
        yRotation += mouseX;

        //Apply rotation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
