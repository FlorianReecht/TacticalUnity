using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ce script est attaché à la caméra ce qui permet de modifier sa position
public class CameraMovement : MonoBehaviour
{
    float speed=10f;
    private float zoomSpeed = 1000f;
    [SerializeField] Camera cam;
    

    // Start is called before the first frame updateprivate float speed = 2.0f;
    void Update () 
    {
 
    if (Input.GetKey(KeyCode.RightArrow))
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.LeftArrow)){
    transform.position += Vector3.left * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.UpArrow)){
    transform.position += Vector3.up * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.DownArrow)){
    transform.position += Vector3.down * speed * Time.deltaTime;
    }
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    cam.orthographicSize+=-scroll*Time.deltaTime*zoomSpeed;
    if(cam.orthographicSize<=1)
    {
        cam.orthographicSize=1;
    }
    
 
}
}
