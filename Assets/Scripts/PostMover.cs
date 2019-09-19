using UnityEngine;

[SelectionBase]
public class PostMover : MonoBehaviour
{
	public float speed = 1f;
	public bool invertDirection = false;
	public Transform start;
	public float randomRange = 2f;

	private float finalSpeed;

	void Start()
	{
		RandomizeVerticalPosition();
	}

	void Update()
	{
		finalSpeed = (invertDirection) ? speed : -speed;
		transform.Translate(new Vector3(finalSpeed, 0, 0));

		if (Input.GetKeyDown(KeyCode.R))
			GoBackToStart();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<PostLimit>())
		{
			GoBackToStart();
		}
	}

	void GoBackToStart()
	{
		transform.position = start.position;
		RandomizeVerticalPosition();
	}

	void RandomizeVerticalPosition()
	{
		transform.position += Vector3.up * Random.Range(0, randomRange);
	}
}
