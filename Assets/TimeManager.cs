using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class TimeManager : MonoBehaviour {
	
	public float levelTime = 5;
	
	public GameObject playerPrefab;
	public Text timerText;
	public Light bounceLight;
	public Light dirLight;
	
	private GameObject playerGO;
	
	private float currentTime;
	private float currentTimeInverse;
	
	private float chromaticAberrationStart = 2;
	private float chromaticAberrationEnd = 5;
	
	private Twirl twirlScript;
	private	VignetteAndChromaticAberration aberrationScript;
	
	public static int timesCompleted;

	void Awake () {
		twirlScript = Camera.main.GetComponent<Twirl>();
		aberrationScript = Camera.main.GetComponent<VignetteAndChromaticAberration>();
		
		twirlScript.enabled = false;
		aberrationScript.enabled = false;
		
		if (timesCompleted > 0)
		{
			Camera.main.GetComponent<CameraShake>().RepeatShake(timesCompleted);
		}
	}
	
	// Use this for initialization
	void Start () {
		BeginLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("q"))
		{
			TimeGo();
		}
	}
	
	public void TimeGo () {
		Messenger.Broadcast("timeGo");
	}
	
	public float GetTime () {
		return currentTime;
	}
	
	void BeginLevel () {
		playerGO = Instantiate(playerPrefab, GameObject.Find("SpawnPoint").transform.position, Quaternion.identity) as GameObject;
		
		StartCoroutine("StartTimer");
	}
	
	void Unpause () {
		
		StartCoroutine("RewindTimer");
		TimeGo();
	}
	
	void GameOver () {
		Camera.main.GetComponent<Testing>().Restart();
	}
	
	IEnumerator StartTimer () {
		
		float t = levelTime;
		currentTime = 0;
		
		while (t > 0)
		{
			timerText.text = t.ToString();
			
			t -= Time.deltaTime;
			currentTime += Time.deltaTime;
			
			currentTimeInverse = t;
			
			// timerText.text = t.ToString();
			
			yield return null;
		}
		
		// timerText.text = "0.0000000";
		
		timerText.text = "failure";
		Camera.main.backgroundColor = Color.red;
		// Camera.main.backgroundColor = new Color32(150, 0, 0, 0);
		bounceLight.color = Color.red;
		dirLight.color = Color.red;
		
		aberrationScript.enabled = true;
		aberrationScript.chromaticAberration = chromaticAberrationStart;
		
		GameOver();
	}
	
	IEnumerator RewindTimer () {
		
		Time.timeScale = 2f;
		
		float t = currentTimeInverse;
		// currentTime = 0;
		
		while (t < levelTime)
		{
			timerText.text = t.ToString();
			
			t += Time.deltaTime;
			// currentTime += Time.deltaTime;
			
			// timerText.text = t.ToString();
			
			aberrationScript.chromaticAberration = Mathf.Lerp(chromaticAberrationStart, chromaticAberrationEnd, t/levelTime);
			
			yield return null;
		}
		
		// timerText.text = levelTime.ToString("f7");
		
		if (GameObject.FindGameObjectsWithTag("Target").Length > 0)
		{
			timerText.text = "failure";
			Camera.main.backgroundColor = Color.red;
			// Camera.main.backgroundColor = new Color32(150, 0, 0, 0);
			bounceLight.color = Color.red;
			dirLight.color = Color.red;
			aberrationScript.chromaticAberration = chromaticAberrationStart;
		}else{
			timerText.text = "success";
			// Camera.main.backgroundColor = Color.green;
			Camera.main.backgroundColor = new Color32(0, 120, 0, 0);
			bounceLight.color = Color.green;
			dirLight.color = Color.green;
			aberrationScript.chromaticAberration = chromaticAberrationStart;
			
			timesCompleted++;
		}
		
		GameOver();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		
		// Debug.Log(other.gameObject.tag);
		
		if (other.gameObject.tag == "Player")
		{
			// BeginPhaseTwo();
			StartCoroutine("BeginPhaseTwo");
		}
	}
	
	IEnumerator BeginPhaseTwo () {
		
		playerGO.SetActive(false);
		StopCoroutine("StartTimer");
		
		float twirlTime = 0.5f;
		
		twirlScript.enabled = true;
		aberrationScript.enabled = true;
		
		twirlScript.angle = 360;
		aberrationScript.chromaticAberration = 0;
		
		float t = 0;
		
		while (t < twirlTime)
		{
			twirlScript.angle = Mathf.Lerp(360, 0, t/twirlTime);
			aberrationScript.chromaticAberration = Mathf.Lerp(0, chromaticAberrationStart, t/twirlTime);
			
			t += Time.deltaTime;
			yield return null;
		}
		
		twirlScript.angle = 0;
		// twirlScript.enabled = false;
		
		Unpause();
	}
}
