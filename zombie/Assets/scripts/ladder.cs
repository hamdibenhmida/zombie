using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ladder : MonoBehaviour

{


	public Animator anim;



	public Transform chController;
	bool inside = false;
	public float speedUpDown = 3.2f;
	public FPSInput FPSInput;

	void Start()
	{
		anim = GetComponent<Animator>();
		FPSInput = GetComponent<FPSInput>();
		inside = false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Ladder")
		{
			FPSInput.enabled = false;
			inside = !inside;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Ladder")
		{
			FPSInput.enabled = true;
			inside = !inside;
		}
	}

	void Update()
	{
		if (FPSInput.enabled==false)
        {
			anim.SetBool("isclimbing", true);
			if (inside == true && Input.GetKey("z"))
			{
				chController.transform.position += Vector3.up / speedUpDown;
				
			}


			if (inside == true && Input.GetKey("s"))
			{
				chController.transform.position += Vector3.down / speedUpDown;
				
			}
			


		}
		else anim.SetBool("isclimbing", false);

	}

}

