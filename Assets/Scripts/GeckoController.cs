using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoController : BaseController 
{
	public override void Move ()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 100f, LayerMask.GetMask("ground"));
		if (hit.collider != null) 
		{
			if (hit.distance <= 0.6f) 
			{
				Vector3 right = Vector3.Cross (transform.forward, hit.normal);
				transform.up = hit.normal;
				if (Input.GetKey (KeyCode.LeftArrow)) 
				{
					transform.position = Vector3.Lerp (transform.position, transform.position + right, Time.deltaTime);
				}

				if (Input.GetKey (KeyCode.RightArrow)) 
				{
					transform.position = Vector3.Lerp (transform.position, transform.position - right, Time.deltaTime);
				}
			}
		}

		hit = Physics2D.Raycast(transform.position, -Vector2.left, 100f, LayerMask.GetMask("ground"));
		if (hit.collider != null) 
		{
			if (hit.distance <= 0.55f) 
			{
				Vector3 right = Vector3.Cross (transform.forward, hit.normal);
				transform.up = hit.normal;
				if (Input.GetKey (KeyCode.DownArrow)) 
				{
					transform.position = Vector3.Lerp(transform.position, transform.position + right, Time.deltaTime);
				}

				if (Input.GetKey (KeyCode.UpArrow))
				{
					transform.position = Vector3.Lerp(transform.position, transform.position - right, Time.deltaTime);
				}
			}
		}

		hit = Physics2D.Raycast(transform.position, Vector2.left, 100f, LayerMask.GetMask("ground"));
		if (hit.collider != null) 
		{
			if (hit.distance <= 0.55f) 
			{
				Vector3 right = Vector3.Cross (transform.forward, hit.normal);
				transform.up = hit.normal;
				if (Input.GetKey (KeyCode.DownArrow)) 
				{
					transform.position = Vector3.Lerp(transform.position, transform.position + right, Time.deltaTime);
				}

				if (Input.GetKey (KeyCode.UpArrow))
				{
					transform.position = Vector3.Lerp(transform.position, transform.position - right, Time.deltaTime);
				}
			}
		}
	}
}
