using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class NekomiController : MonoBehaviour
{
	//Publics
	public float jumpForce = 1f;
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode jumpMouseButton = KeyCode.Mouse0;
	public ForceMode2D forceMode = ForceMode2D.Impulse;

	[Tooltip("Objects that will kill the player upon collision")]

	public UnityEvent OnJump, OnHit;


	//Privates
	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpMouseButton))
		{
			OnJump.Invoke();
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(0, jumpForce), forceMode);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.GetComponent<Killer>().active == true)
		{
			OnHit.Invoke();
		}
	}
}
