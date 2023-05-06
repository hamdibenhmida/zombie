using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class animatorcontroller : MonoBehaviour
{
    Animator animator;
    int iswallkingHash;
    int isRunningHash;
    int iswalbackHash;
    int isleftingHash;
    int isrightingHash;
    int iscrouchingHash;
    int isjumpinghash;

    // Start is called before the first frame update

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        iscrouchingHash = Animator.StringToHash("iscrouching");
        iswallkingHash = Animator.StringToHash("iswallking");
        isRunningHash = Animator.StringToHash("isrunning");
        iswalbackHash = Animator.StringToHash("iswalback");
        isleftingHash = Animator.StringToHash("islefting");
        isrightingHash = Animator.StringToHash("isrighting");
        isjumpinghash = Animator.StringToHash("isjumping");
    }
    // Update is called once per frame
    void Update()
    {
        bool iscrouching = animator.GetBool(iscrouchingHash);
        bool isrighting = animator.GetBool(isrightingHash);
        bool islefting = animator.GetBool(isleftingHash);
        bool isrunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(iswallkingHash);
        bool iswalback = animator.GetBool(iswalbackHash);
        bool isjumping = animator.GetBool(isjumpinghash);
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("q");
        bool backPressed = Input.GetKey("s");
        bool forwardPressed = Input.GetKey("z");
        bool runPressed = Input.GetKey("left shift");
        bool coushPressed = Input.GetKey("left ctrl");
        bool jumpPressed = Input.GetKey(KeyCode.Space);


        // if player presses w key
        if (!isWalking && forwardPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(iswallkingHash, true);
        }
        // if player is not pressing w key
        if (isWalking && !forwardPressed)
            // then set the isWalking boolean to be false
            animator.SetBool(iswallkingHash, false);
        // if player is walking and not running and presses left shift
        if (!isrunning && (forwardPressed && runPressed))

            // then set the isRunning boolean to be true
            animator.SetBool(isRunningHash, true);

        // if player is running and stops running or stops walking
        if (isrunning && (!forwardPressed || !runPressed))
        {
            // then set the isRunning boolean to be false
            animator.SetBool(isRunningHash, false);
        }
        if (!iswalback && backPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(iswalbackHash, true);
        }
        // if player is not pressing w key
        if (iswalback && !backPressed)
            // then set the isWalking boolean to be false
            animator.SetBool(iswalbackHash, false);
        if (!islefting && leftPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(isleftingHash, true);
        }
        // if player is not pressing w key
        if (islefting && !leftPressed)
            // then set the isWalking boolean to be false
            animator.SetBool(isleftingHash, false);
        if (!isrighting && rightPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(isrightingHash, true);
        }
        // if player is not pressing w key
        if (isrighting && !rightPressed)
            // then set the isWalking boolean to be false
            animator.SetBool(isrightingHash, false);
        if (!iscrouching && coushPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(iscrouchingHash, true);
        }
        // if player is not pressing w key
        if (iscrouching && !coushPressed)
            // then set the isWalking boolean to be false
            animator.SetBool(iscrouchingHash, false);
        if (!isjumping && jumpPressed)
        {
            // then set the isWalking boolean to be true
            animator.SetBool(isjumpinghash, true);
        }
        // if player is not pressing w key
        if (isjumping && !jumpPressed)
            // then set the isWalking boolean to be false
            animator.SetBool(isjumpinghash, false);
    }
}
