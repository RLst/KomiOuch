using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
	public float scrollSpeed = 1f;

	Material material;

    void Start()
    {
		material = GetComponent<Renderer>().material;
    }

    void Update()
    {
		material.mainTextureOffset = new Vector2(Time.time * scrollSpeed, 0);
    }
}
