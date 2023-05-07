using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    public Transform spherecastSpawn;
    public LayerMask zombieLayer;
    public int attackdamage = 100;
    public Animator anim;
    public AudioSource kill;

    private Vector3 _moveDirection = Vector3.zero;


    int iswallkingHash;
    int isRunningHash;
    int iswalbackHash;
    int isleftingHash;
    int isrightingHash;
    int iscrouchingHash;
    int isjumpinghash;
    
    int isclimbingdownHash;


    public Vector3 jump;
    public float jumpForce = 2.0f;

    
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        jump = new Vector3(0.0f, 5.0f, 0.0f);
        _charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        iscrouchingHash = Animator.StringToHash("iscrouching");
        iswallkingHash = Animator.StringToHash("iswallking");
        isRunningHash = Animator.StringToHash("isrunning");
        iswalbackHash = Animator.StringToHash("iswalback");
        isleftingHash = Animator.StringToHash("islefting");
        isrightingHash = Animator.StringToHash("isrighting");
        isjumpinghash = Animator.StringToHash("isjumping");
        
        isclimbingdownHash = Animator.StringToHash("isclimbingdown");

    }
   
    void Update()
    {
       


        bool isrighting = anim.GetBool(isrightingHash);
        bool islefting = anim.GetBool(isleftingHash);
        bool isrunning = anim.GetBool(isRunningHash);
        bool isWalking = anim.GetBool(iswallkingHash);
        bool iswalback = anim.GetBool(iswalbackHash);
        bool isjumping = anim.GetBool(isjumpinghash);
        
        bool isclimbingdown = anim.GetBool(isclimbingdownHash);
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("q");
        bool backPressed = Input.GetKey("s");
        bool walkPressed = Input.GetKey("z");
        bool jumpPressed = Input.GetKey(KeyCode.Space);
        bool climpPressed = Input.GetKey("c");



        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);



        if (_charController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = jumpSpeed;
            }
        }

        _moveDirection.y += gravity * Time.deltaTime;
        movement.y = _moveDirection.y;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);



        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
        if (!isWalking && walkPressed)
        {
            // then set the isWalking boolean to be true
            anim.SetBool(iswallkingHash, true);
        }
        // if player is not pressing w key
        if (isWalking && !walkPressed)
            // then set the isWalking boolean to be false
            anim.SetBool(iswallkingHash, false);


        if (!iswalback && backPressed)
        {
            // then set the isWalking boolean to be true
            anim.SetBool(iswalbackHash, true);
        }
        // if player is not pressing w key
        if (iswalback && !backPressed)
            // then set the isWalking boolean to be false
            anim.SetBool(iswalbackHash, false);
        if (!islefting && leftPressed)
        {
            // then set the isWalking boolean to be true
            anim.SetBool(isleftingHash, true);
        }
        // if player is not pressing w key
        if (islefting && !leftPressed)
            // then set the isWalking boolean to be false
            anim.SetBool(isleftingHash, false);
        if (!isrighting && rightPressed)
        {
            // then set the isWalking boolean to be true
            anim.SetBool(isrightingHash, true);
        }
        // if player is not pressing w key
        if (isrighting && !rightPressed)
            // then set the isWalking boolean to be false
            anim.SetBool(isrightingHash, false);
        
        if (!isjumping && jumpPressed)
       {
        //    // then set the isWalking boolean to be true
    anim.SetBool(isjumpinghash, true);
      }
        // if player is not pressing w key
        if (isjumping && !jumpPressed)
        //    // then set the isWalking boolean to be false
           anim.SetBool(isjumpinghash, false);
       /* if (!isclimbing && (climpPressed&& walkPressed))
        {
            // then set the isWalking boolean to be true
            anim.SetBool(isclimbingHash,true);
          
        }
        if (isclimbing && (!climpPressed&& !walkPressed) )
        {
            anim.SetBool(isclimbingHash, false);
        
        }
        if (!isclimbingdown && climpPressed && !backPressed)
        {
            // then set the isWalking boolean to be true
            anim.SetBool(isclimbingdownHash, true);
      
        }
        if (isclimbingdown && !climpPressed && !backPressed)
        {
            // then set the isWalking boolean to be true
            anim.SetBool(isclimbingdownHash, false);

        }*/

    }
     public void attack()
      {
        anim.SetTrigger("slash");
        kill.Play();
        RaycastHit hit;
       if (Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.TransformDirection(Vector3.forward), out hit, zombieLayer))
        {

            hit.transform.GetComponent<AI>().Onhit(attackdamage);
        }
      }
}
