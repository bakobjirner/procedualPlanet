using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    // Cameracontrol
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public Transform cameraT;
    float verticalLookRotation;

    // Movement
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    public float walkSpeed = 8;
    // Jump
    public float jumpForce = 500f;
    bool grounded;
    public LayerMask groundedMask;

    //Animation
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 axisInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        animator.SetFloat("Forward", axisInput.z);
        animator.SetFloat("Turn", axisInput.x);
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        // Movement smoothing because we take GetAxisRaw instead of GetAxis
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        if(Input.GetButtonDown("Jump"))
        {
            if(grounded)
            {
                animator.SetTrigger("Jump");
                animator.SetBool("Fall", true);
                rb.AddForce(transform.up * jumpForce);
            }
        }
        grounded = false;
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0 + .5f, groundedMask))
        {
            grounded = true;
            animator.SetBool("Fall", false);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
