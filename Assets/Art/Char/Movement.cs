using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpForce;
    private float fallVelocity;
    [SerializeField] private float SlideVelocity;
    [SerializeField] private float SlopeMaxAngleForceDown;
    private bool isOnSlope = false;
    private Vector3 hitNormal;
    private Vector3 playerInput;
    private Vector3 movePlayer;
    private Vector3 camForward;
    private Vector3 camRight;
    private CharacterController player;
    private Animator playerAnimatorController;
    [SerializeField] private Camera mainCamera;


    [SerializeField] private int noOfClicks = 0;
    private float lastClickedTime = 0;
    [SerializeField] private float maxComboDelay = 0.9f;



    int clickCount;
    bool canClick;


    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();

        clickCount = 0;
        canClick = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed = 12;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = 8;
        }
        

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {

            lastClickedTime = Time.time;
            noOfClicks++;
        }

        switch (noOfClicks)
        {
            case 1:
                playerAnimatorController.SetBool("AttackOne", true);
                break;
            case 2:
                playerAnimatorController.SetBool("AttackTwo", true);
                break;
            case 3:
                playerAnimatorController.SetBool("AttackThree", true);
                break;
            default:
                playerAnimatorController.SetBool("AttackOne", false);
                playerAnimatorController.SetBool("AttackTwo", false);
                playerAnimatorController.SetBool("AttackThree", false);
                break;
        }

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);


        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.magnitude * playerSpeed);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerActions();

        player.Move(movePlayer * Time.deltaTime);


    }

    public void ToZero()
    {
        noOfClicks = 0;
    }



    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void PlayerActions()
    {
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetTrigger("PlayerJump");
        }
    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else if (!player.isGrounded)
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
        }

        playerAnimatorController.SetBool("IsGrounded", player.isGrounded);

        SlideDown();
    }

    void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * SlideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * SlideVelocity;
            movePlayer.y += SlopeMaxAngleForceDown;                                         //<- Para cuando se esta deslizando no pegue saltos
        }
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    

}


