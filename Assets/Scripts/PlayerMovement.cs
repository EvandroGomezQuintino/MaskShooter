using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;

    public Rigidbody body;

    public GameObject mouseHitPoint;

    public bool mouseLook;


    private float movementSpeed = 5;

    //public Gun gun1;

    //public List<Gun> guns = new List<Gun>();

    //public int gunSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = 10;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 5;
        }

        if(Input.GetKey(KeyCode.W))
        {
            body.velocity = transform.forward * movementSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            body.velocity = transform.forward * -2.5f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            float currentY = transform.rotation.eulerAngles.y;
            currentY += (Time.deltaTime * 180);
            transform.rotation = Quaternion.Euler(0, currentY, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float currentY = transform.rotation.eulerAngles.y;
            currentY -= (Time.deltaTime * 180);
            transform.rotation = Quaternion.Euler(0, currentY, 0);
        }

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    guns[gunSelected].Fire();
        //}

            
        if(mouseLook == true)
        {
            TurnPlayerToMouse();
        }
        

    }


    void TurnPlayerToMouse ()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject)
            {
                Debug.Log(hit.collider.gameObject.name);
                mouseHitPoint.transform.position = hit.point;

                Vector3 lookPosition = hit.point;
                lookPosition.y = transform.position.y;

                transform.LookAt(lookPosition);
            }
        }

    }


}
