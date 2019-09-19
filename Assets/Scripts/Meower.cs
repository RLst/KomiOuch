using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meower : MonoBehaviour
{
	public AudioClip[] sounds;
	AudioSource source;

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	public void RandomMeow()
	{
		source.PlayOneShot(sounds[Random.Range(0, sounds.Length-1)]);
	}
}
