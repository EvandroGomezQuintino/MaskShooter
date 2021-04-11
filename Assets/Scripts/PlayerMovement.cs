using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharStates { Idle, Run, Walk, Shoot };


public class PlayerMovement : MonoBehaviour
{
    // Components 
    private Camera cam;
    private Rigidbody body;

    //[SerializeField]
    private float jumpForce = 2.5f;
    private Vector3 jump = new Vector3(0.0f,2.0f,0.0f);

    // Action State of Character
    public CharStates charState;

    //Animation controller
    public Animator characterAnimation;

    public GameObject mouseHitPoint;

    public bool mouseLook;
    public bool isGrounded;

    private float movementSpeed = 5;

    //public Gun gun1;

    //public List<Gun> guns = new List<Gun>();

    //public int gunSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Assing componentes to our GameObject
        body = GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();

        charState = CharStates.Idle;


    }
    
    // Checking if character is touching the floor
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        // If Character is touching the ground
        if (isGrounded == true)
        {
            // ########################### NEED TO REVIEW THIS PART ###########################
            // Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                body.AddForce(jump * jumpForce, ForceMode.Impulse);
                
            }

            
            // Walking mode
            if (charState != CharStates.Run)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    // Triggering animation
                    characterAnimation.SetBool("Walking", true);
                    // Moving game obj
                    body.velocity = transform.forward * movementSpeed;
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    // Triggering animation
                    characterAnimation.SetBool("Walking", false);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    // Triggering animation
                    characterAnimation.SetBool("BackWards", true);
                    // Moving game obj
                    body.velocity = transform.forward * -2.5f;
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    // Triggering animation
                    characterAnimation.SetBool("BackWards", false);

                }

                // Activating running mode
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    // Triggering animations
                    characterAnimation.SetBool("Walking", false);
                    characterAnimation.SetBool("BackWards", false);
                    // Entering Running mode
                    charState = CharStates.Run;

                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    // Triggering animation
                    characterAnimation.SetTrigger("Shooting");
                    // Reducing movement speed while aiming
                    movementSpeed = 2;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    // Triggering animation
                    characterAnimation.SetTrigger("Break");
                    // Increasing movement speed when not aiming
                    movementSpeed = 5;
                }
            }
            // Running Mode
           else if (charState == CharStates.Run)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    // Triggering animation
                    characterAnimation.SetBool("Run", true);
                    // Moving Game obj
                    body.velocity = transform.forward * (movementSpeed + 2.5f);
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    // Triggering animation
                    characterAnimation.SetBool("Run", false);
                    
                }


                if (Input.GetKey(KeyCode.S))
                {
                    // Triggering animation
                    characterAnimation.SetBool("BackWards", true);
                    // Moving Game obj
                    body.velocity = transform.forward * -5.0f;
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    // Triggering animation
                    characterAnimation.SetBool("BackWards", false);
                }
            }

            // Deactivating running mode
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                // Triggering animations
                characterAnimation.SetBool("Run", false);
                //characterAnimation.SetBool("Walking", false);
                charState = CharStates.Idle;
            }
        }
        
        // Turning Right
        if (Input.GetKey(KeyCode.D))
        {
            float currentY = transform.rotation.eulerAngles.y;
            currentY += (Time.deltaTime * 180);
            transform.rotation = Quaternion.Euler(0, currentY, 0);
        }

        // Turning Left
        if (Input.GetKey(KeyCode.A))
        {
            float currentY = transform.rotation.eulerAngles.y;
            currentY -= (Time.deltaTime * 180);
            transform.rotation = Quaternion.Euler(0, currentY, 0);
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
