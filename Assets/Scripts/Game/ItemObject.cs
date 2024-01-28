using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemObject : MonoBehaviour
{
	[SerializeField]
	private AudioClip makeSound;

	[SerializeField]
	private Rigidbody2D rigidbody2d;

	public Rigidbody2D Rigidbody => rigidbody2d;

	[SerializeField]
	private Collider2D collider2d;
	public Collider2D Collider => collider2d;

	[SerializeField]
	private SpriteRenderer spriteRenderer;
	public SpriteRenderer SpriteRenderer => spriteRenderer;

	[SerializeField]
	private bool isCombinedItem = false;

	public bool IsCombinedItem => isCombinedItem;

	public void Throw(Vector2 force)
	{
		rigidbody2d.AddForce(force, ForceMode2D.Force);
	}

	public void PlayCreateSound()
	{
		if(makeSound != null)
		{
			AudioSource.PlayClipAtPoint(makeSound, Camera.main.transform.position, 1);
		}
	}
}