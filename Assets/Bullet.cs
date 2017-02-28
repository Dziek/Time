using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float fireForce = 100;
	
	public GameObject hitExplosionPrefab;
	
	private Rigidbody2D rb2d;
	private Collider2D col2d;
	
	private float spawnTime;
	
	private ObjectPool objectPool;
	
	void Awake () {
		objectPool = gameObject.AddComponent<ObjectPool>();
		objectPool.SetUp(hitExplosionPrefab);

		rb2d = GetComponent<Rigidbody2D>();
		col2d = GetComponent<Collider2D>();
		
		Messenger.AddListener("timeGo", TimeBegin);
	}
	
	void OnDestroy () {
		Messenger.RemoveListener("timeGo", TimeBegin);
	}
	
	void OnEnable () {
		// TimeBegin();
		col2d.isTrigger = true;
		
		// spawnTime = GameObject.Find("LevelExit").GetComponent<TimeManager>().GetTime();
		spawnTime = 1;
	}
	
	public void TimeBegin () {
		Invoke("Unpause", spawnTime);
	}
	
	void Unpause () {
		rb2d.AddForce(transform.right * fireForce);
		col2d.isTrigger = false;
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag != "Bullet")
		{
			// GameObject go = Instantiate(hitExplosion, transform.position, Quaternion.identity) as GameObject;
			
			GameObject go = objectPool.GetObject();
		
			go.transform.position = transform.position;
			// go.GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
			
			go.SetActive(true);			
			
			gameObject.SetActive(false);
		}
	}
}
