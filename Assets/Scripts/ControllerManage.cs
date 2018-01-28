using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
	public float JumpHeigh = 4f;

	public float HorizeSpeed = 4f;

	public bool mIsGround;

	public Vector3 InitPosition;

	public Vector3 InitRotation;

	void Awake()
	{
		_Awake ();

		InitPosition = transform.position;
		InitRotation = transform.localEulerAngles;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag("ground"))
		{
			mIsGround = true;
		}

		PlaySound ();

		_OnTriggerEnter2D (coll);
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag("ground"))
		{
			mIsGround = false;
		}

		PlaySound ();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("ground"))
		{
			mIsGround = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("ground"))
		{
			mIsGround = false;
		}
	}

	public virtual void _OnTriggerEnter2D(Collider2D coll)
	{
		
	}

	public virtual void _Awake()
	{
		
	}

	public virtual void Enter()
	{
	}

	public virtual void Exit()
	{
		
	}

	public virtual void PlaySound()
	{
		
	}

	public virtual void Reset()
	{
		transform.position = InitPosition;
		transform.localEulerAngles = InitRotation;
	}

	public virtual void Move ()
	{
		if (mIsGround)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				transform.position = transform.position + new Vector3(0f, JumpHeigh, 0f);
			}
		}
	}

	public virtual void LateMove()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(1f, 0f, 0f), Time.deltaTime * HorizeSpeed);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(1f, 0f, 0f), Time.deltaTime * HorizeSpeed);
		}
	}
}

public class ControllerManage : MonoBehaviour 
{
	private static ControllerManage mInstance;

	public static ControllerManage Instance
	{
		get
		{
			return mInstance;
		}
	}

	private BaseController mCurrentController;

	private BaseController mPreController;

	private Dictionary<int, BaseController> mAllUsedController = new Dictionary<int, BaseController>();

	private void Awake()
	{
		mInstance = this;
	}

	public void ChangeController(BaseController baseController)
	{
		if (mCurrentController != null) 
		{
			mPreController = mCurrentController;
			mCurrentController.Exit ();
		}

		mCurrentController = baseController;
		mCurrentController.Enter ();

		if (!mAllUsedController.ContainsKey (baseController.GetInstanceID ())) 
		{
			mAllUsedController.Add (baseController.GetInstanceID (), baseController);
		}
	}

	public void ResetController()
	{
		foreach (var item in mAllUsedController) 
		{
			item.Value.Reset ();
		}
	}

	public BaseController GetRecentController()
	{
		return mPreController != null ? mPreController : mCurrentController;
	}

	private void Update()
	{
		if (mCurrentController != null) 
		{
			mCurrentController.Move ();
		}
	}

	private void LateUpdate()
	{
		if (mCurrentController != null) 
		{
			mCurrentController.LateMove ();
		}
	}
}
