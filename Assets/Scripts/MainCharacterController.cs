using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : BaseController
{
	public override void _Awake ()
	{
		base._Awake ();

		JumpHeigh = 3f;
		HorizeSpeed = 3f;
	}

	public override void Move ()
	{
		base.Move ();

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(new Vector3(0f, 0f, 5f));
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(new Vector3(0f, 0f, -5f));
		}
	}

	public override void _OnTriggerEnter2D (Collider2D coll)
	{
		base._OnTriggerEnter2D (coll);

		if (coll.gameObject.CompareTag ("deadline")) 
		{
			ControllerManage.Instance.ResetController ();

			BaseController closeController = ControllerManage.Instance.GetRecentController ();
			transform.position = closeController.InitPosition + Vector3.up * 5f;
		}
	}

	public override void PlaySound ()
	{
		base.PlaySound ();

		transform.GetComponent<AudioSource> ().Play ();
	}
}
