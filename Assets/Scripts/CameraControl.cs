using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float followDistance;
    public float height;

    public float cameraSmoothing;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void FixedUpdate()
    {

        Vector3 camTargetPosition = player.transform.position;
        camTargetPosition.y += height;
        camTargetPosition.z -= followDistance;

        transform.position = Vector3.Lerp(transform.position, camTargetPosition, cameraSmoothing);
       

    }

}
