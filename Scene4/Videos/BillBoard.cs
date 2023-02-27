using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
	[SerializeField]
	private Transform player;

	void Update()
	{
		Vector3 p = this.player.transform.position;
		p.y = this.transform.position.y;
		this.transform.LookAt(p);
		this.transform.Rotate(Vector3.right, 90f);
	}
}