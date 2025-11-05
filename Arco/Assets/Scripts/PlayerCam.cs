using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 200f;
    private float distanceZ = 10f;
    private float distanceY = 12f;
    private float minYAngle = -45f;
    private float maxYAngle = 35f;

    private float rotX = 0f;
    private float rotY = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Maus wird zentriert
        Cursor.lockState = CursorLockMode.Locked; // Maus zentrieren
        Cursor.visible = false;
    }

    // LateUpdate, da LateUpdate nach Update aufgerufen wird und die Kamera dadurch nicht ruckelt
    void LateUpdate()
    {
        // Mausbewegung lesen
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;


        // Rotationswinkel aktualisieren
        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minYAngle, maxYAngle); // vertikale Begrenzung


        // Rotation und Position der Kamera berechnen
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);
        Vector3 offset = new Vector3(0, distanceY, -distanceZ);
        Vector3 direction = rotation * offset;

        transform.position = player.position + direction;
        transform.LookAt(player.position);
    }
}
