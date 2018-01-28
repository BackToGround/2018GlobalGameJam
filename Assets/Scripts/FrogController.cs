using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : BaseController 
{
	private Transform anotherPlayer;

	private Animator mAnimator;

	private bool mIsTrigger;
	private Vector3 mStartPosition;
	private float mTime;

	public override void _Awake ()
	{
		base._Awake ();

		JumpHeigh = 5f;
		HorizeSpeed = 5f;

		mAnimator = GetComponent<Animator> ();
	}

	public override void _OnTriggerEnter2D (Collider2D coll)
	{
		base._OnTriggerEnter2D (coll);

		if (coll.gameObject.CompareTag("character"))
		{
			BaseController baseController = coll.gameObject.GetComponent<BaseController> ();
			if (baseController.mIsGround)
				return;

			anotherPlayer = coll.transform;

			Transform parent = transform.Find ("Parent");
			anotherPlayer.SetParent (parent != null ? parent : transform);
			anotherPlayer.localPosition = Vector3.zero;
			anotherPlayer.localEulerAngles = Vector3.zero;
			ControllerManage.Instance.ChangeController (this);

			Rigidbody2D rigidbody2d = anotherPlayer.gameObject.GetComponent<Rigidbody2D> ();
			rigidbody2d.bodyType = RigidbodyType2D.Kinematic;

			SoftSprite softObject = anotherPlayer.GetComponent<SoftSprite> ();
			softObject.Density = 1.0f;
			softObject.ForceUpdate ();
		}
	}

	public override void Reset ()
	{
		base.Reset ();

		JumpDown ();
	}

	private void JumpDown()
	{
		if (anotherPlayer != null) 
		{
			anotherPlayer.parent = null;
			ControllerManage.Instance.ChangeController (anotherPlayer.GetComponent<BaseController> ());

			Rigidbody2D rigidbody2d = anotherPlayer.gameObject.GetComponent<Rigidbody2D> ();
			rigidbody2d.bodyType = RigidbodyType2D.Dynamic;

			SoftSprite softObject = anotherPlayer.GetComponent<SoftSprite> ();
			softObject.Density = 0.14f;
			softObject.ForceUpdate ();

			anotherPlayer = null;
		}
	}

	public override void LateMove ()
	{
		if (!mIsGround) 
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(HorizeSpeed, 0f, 0f), Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(HorizeSpeed, 0f, 0f), Time.deltaTime);
			}
		}
	}

	public override void Move ()
	{
		if (mIsGround) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				mAnimator.SetTrigger ("Play");
				mStartPosition = transform.position;
				transform.position =  mStartPosition + new Vector3 (0f, JumpHeigh, 0f);
			}
		} 

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
			JumpDown ();
		}
	}

	public override void PlaySound ()
	{
		base.PlaySound ();

		transform.GetComponent<AudioSource> ().Play ();
	}
}
