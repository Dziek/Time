using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
	
	public GameObject bulletGO;
	public Transform firePoint;
	
	public int levelAmmo = 5;
	private int ammoCount;
	
	private ObjectPool objects;
	
	private Text ammoCountText;
	
	// Use this for initialization
	void Start () {
		objects = gameObject.AddComponent<ObjectPool>();
		objects.SetUp(bulletGO);
		
		ammoCount = levelAmmo;
		
		ammoCountText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			if (ammoCount > 0)
			{
				FireForward();
				
				ammoCount--;
				ammoCountText.text = ammoCount.ToString();
				
				if (ammoCount == 0)
				{
					ammoCountText.text = "-";
				}
			}
		}
		
		if (Input.GetMouseButtonDown(1))
		{
			if (ammoCount > 0)
			{
				FireBack();
				
				ammoCount--;
				ammoCountText.text = ammoCount.ToString();
				
				if (ammoCount == 0)
				{
					ammoCountText.text = "-";
				}
			}
		}
	}
	
	void FireForward () {
		GameObject go = objects.GetObject();
		
		go.GetComponent<SpriteRenderer>().color = Color.blue;
		
		go.transform.position = firePoint.position;
		go.transform.right = firePoint.right;
		
		go.SetActive(true);
	}
	
	void FireBack () {
		GameObject go = objects.GetObject();
		
		go.GetComponent<SpriteRenderer>().color = Color.red;
		
		Vector2 bulletPos = Vector2.zero;
		
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, 100, 1 << LayerMask.NameToLayer("Geometry") | 1 << LayerMask.NameToLayer("Frame"));
		// RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
        
		if (hit.collider != null)
		{
			// Debug.Log(hit.collider.gameObject);
			bulletPos = hit.point;
		}
		
		go.transform.position = bulletPos;
		go.transform.right = -firePoint.right;
		
		go.SetActive(true);
	}
}
