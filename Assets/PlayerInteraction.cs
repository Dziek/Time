using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : Player {
	
	public GameObject armGO;
	
	public float armRotationSpeed = 10;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Vector2 lookDir = new Vector2(Input.GetAxis("Stick_H" + playerNum), -Input.GetAxis("Stick_V" + playerNum));
		
		var pos = Camera.main.WorldToScreenPoint(transform.position);
		var lookDir = Input.mousePosition - pos;
		
		if (lookDir.magnitude > 0.1f)
		{
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			// transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			// transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * cannonRotationSpeed);
			
			// cannonGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			armGO.transform.rotation = Quaternion.Slerp(armGO.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * armRotationSpeed);
				
			// rb2d.angularVelocity = 0;
			GetComponent<Rigidbody2D>().angularVelocity = 0;
			
			// lDir = lookDir;
		}
	}
}
