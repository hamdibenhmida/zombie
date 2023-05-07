using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    public Transform spherecastSpawn;
    public LayerMask zombieLayer;
    public int attackdamage = 50;
    public Animator anim;
    public AudioSource kill;




    int iswallkingHash;
    int isRunningHash;
    int iswalbackHash;
    int isleftingHash;
    int isrightingHash;
    int iscrouchingHash;
    int isjumpinghash;
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        iscrouchingHash = Animator.StringToHash("iscrouching");
        iswallkingHash = Animator.StringToHash("iswallking");
        isRunningHash = Animator.StringToHash("isrunning");
        iswalbackHash = Animator.StringToHash("iswalback");
        isleftingHash = Animator.StringToHash("islefting");
        isrightingHash = Animator.StringToHash("isrighting");
        isjumpinghash = Animator.StringToHash("isjumping");
    }
    void Update()
    {
        bool iscrouching = anim.GetBool(iscrouchingHash);
        bool isrighting = anim.GetBool(isrightingHash);
        bool islefting = anim.GetBool(isleftingHash);
        bool isrunning = anim.GetBool(isRunningHash);
        bool isWalking = anim.GetBool(iswallkingHash);
        bool iswalback = anim.GetBool(iswalbackHash);
        bool isjumping = anim.GetBool(isjumpinghash);
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("q");
        bool backPressed = Input.GetKey("s");
        
        bool jumpPressed = Input.GetKey(KeyCode.Space);


        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        anim.SetFloat("walk", deltaZ);
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
        
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
            // then set the isWalking boolean to be true
            anim.SetBool(isjumpinghash, true);
        }
        // if player is not pressing w key
        if (isjumping && !jumpPressed)
            // then set the isWalking boolean to be false
            anim.SetBool(isjumpinghash, false);
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
