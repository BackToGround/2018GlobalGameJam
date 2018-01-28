using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour 
{
	public BaseController MainCharacter;

	// Use this for initialization
	void Start () 
	{
		ControllerManage.Instance.ChangeController (MainCharacter);
	}
}
