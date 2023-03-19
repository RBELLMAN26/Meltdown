using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] InputActionAsset playerInputAsset;
    InputActionMap playerInputMap;
    InputAction jumpInput;
    InputAction crouchInput;

    [SerializeField] float jumpPower = 2;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float groundOffset = 0.1f;
    bool jumping = false;
    Rigidbody rb;
    [SerializeField] Transform cameraPosition;
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] Transform ragdollSkeleton;

    private void Awake()
    {
        playerInputMap = playerInputAsset.FindActionMap("Player");
        jumpInput = playerInputMap.FindAction("Jump");
        crouchInput = playerInputMap.FindAction("Crouch");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = FindObjectOfType<Rotator>().transform;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        SetupCamera();
        ToggleRagdoll(false);
    }

    //Assign Player Input
    private void OnEnable()
    {
        jumpInput.performed += Jump;
        jumpInput.canceled += Jump;
        crouchInput.performed += Crouch;
        crouchInput.canceled += Crouch;
        jumpInput.Enable();
        crouchInput.Enable();
    }

    //UnAssign Player Input
    private void OnDisable()
    {
        jumpInput.performed -= Jump;
        crouchInput.performed -= Crouch;
        jumpInput.canceled -= Jump;
        crouchInput.canceled -= Crouch;
        jumpInput.Disable();
        crouchInput.Disable();
    }

    private void Update()
    {
        //Debug.Log($"Grounded : {OnGround()}");
        playerAnim.SetBool("Grounded", OnGround());
        if (jumping)
        {
            if(!HeightReached())
            {
                transform.position += transform.up * jumpPower * Time.deltaTime;
            }
            else
            {
                jumping = false;
            }

        }
        else
        {
            playerAnim.SetBool("Jump", false);
            if (!OnGround())
            {
                rb.useGravity = true;
            }
        }
    }

    //When Character Spawns,  Have camera focus on Player
    void SetupCamera()
    {
        Camera.main.GetComponent<CameraController>().SnapToPlayer(cameraPosition);
        
    }
    void Jump(InputAction.CallbackContext context)
    {
        if(OnGround())
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    playerAnim.SetBool("Jump", true);
                    rb.useGravity = false;
                    jumping = true;
                    
                    break;
                case InputActionPhase.Canceled:
                    playerAnim.SetBool("Jump", false);
                    rb.useGravity = true;
                    jumping = false;
                    break;
            }
        }
        
    }

    //Sets Animation To Crouch
    void Crouch(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                playerAnim.SetBool("Crouch", true);
                break;
            case InputActionPhase.Canceled:
                playerAnim.SetBool("Crouch", false);
                break;
        }
        
    }

    //Checks If Player is OnGround / OnPlatform
    bool OnGround()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y + groundOffset, transform.position.z);
        Ray ray = new Ray(center, -transform.up);
        RaycastHit hit;
        Debug.DrawRay(center, -transform.up, Color.red);
        return Physics.Raycast(ray, out hit, 0.2f, collisionLayer);
    }

    //Check When Player Has Reached Height
    bool HeightReached()
    {
        return transform.position.y >= jumpHeight;
    }

    void ToggleRagdoll(bool isOn)
    {
        foreach(Rigidbody rigidbody in ragdollSkeleton.GetComponentsInChildren<Rigidbody>()) {
            rigidbody.GetComponent<Collider>().enabled = isOn;
            rigidbody.useGravity = isOn;
            rigidbody.isKinematic = !isOn;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bars"))
        {
            playerAnim.enabled = false;
            foreach(var collider in GetComponents<Collider>())
            {
                collider.enabled = false;
            }
            ToggleRagdoll(true);
        }
    }
}
