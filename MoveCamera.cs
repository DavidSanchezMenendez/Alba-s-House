using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour { 
    public float mouseSeniblidiad = 1000f;
    public Transform PlayerBody;
    float xRotation = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
      
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSeniblidiad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSeniblidiad * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -50, 50); //Para bloquear que no pueda mirar hacia arriba o hacia abajo infinitamente


       
         transform.localRotation = Quaternion.Euler(xRotation, 0, 0);//Para mirar arriba y abajo
       


        PlayerBody.Rotate(Vector3.up * mouseX *3);//para mirar a la derecha y izquierda y rotar el cuerpo

       
    }   

}
