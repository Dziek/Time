using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public GameObject hitExplosionPrefab;
	
	private GameObject[] bulletGOs = new GameObject[0];
	
	private ObjectPool objectPool;
	
	void Awake () {
		objectPool = gameObject.AddComponent<ObjectPool>();
		objectPool.SetUp(hitExplosionPrefab);
		
		// Add Messenger for time
		Messenger.AddListener("timeGo", Unpause);
	}
	
	void OnDestroy () {
		Messenger.RemoveListener("timeGo", Unpause);
	}
	
	void OnDisable () {
		Time.timeScale = 1f;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// transform.Rotate(Vector3.up * 50 * Time.deltaTime);
		
		// if (bulletGOs.Length > 0)
		// {
			
			// bool slowDown = false;
			
			// float slowAmount = 1;
			
			// for (int i = 0; i < bulletGOs.Length; i++)
			// {
				// float distance = Vector3.Distance(transform.position + transform.up * 1, bulletGOs[i].transform.position);
				
				// if (distance < 1f)
				// {
					// slowDown = true;
					// slowAmount = distance;
				// }
			// }
			
			// if (slowDown == true)
			// {
				// Time.timeScale = slowAmount;
			// }else{
				// Time.timeScale = 3f;
			// }
			
			// Debug.Log(slowDown + " " + slowAmount);
		// }
		
	}
	
	void Unpause () {
		bulletGOs = GameObject.FindGameObjectsWithTag("Bullet");
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		
		// Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "Bullet")
		{
			Hit();
		}

	}
	
	void Hit () {
		// GameObject go = Instantiate(hitExplosion, transform.position, Quaternion.identity) as GameObject;
		GameObject go = objectPool.GetObject();
		
		go.transform.position = transform.position;
		
		go.SetActive(true);
		
		gameObject.SetActive(false);
	}
}
