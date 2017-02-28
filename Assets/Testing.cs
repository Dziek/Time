using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Application.targetFrameRate = 30;
		// QualitySettings.vSyncCount = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("r"))
		{
			TimeManager.timesCompleted = 0;
			Restart();
		}
		
		if (Input.GetKeyDown("escape"))
		{
			Application.Quit();
		}
	}
	
	public void Restart () {
		Application.LoadLevel(Application.loadedLevel);
	}
	
	//TODO
	// Make it all 3D
	// VVV - Limit bullets pt 1 - limit pt 2 - UI
	// VVV - Detect Success, Failure, GameOver
	
	// Ensure game loads another level okay
	// Make multiple levels
	// Add menus
	
	// VVV - Performance
	// Make player better - faster, slidier, wall jumpier
	
	// Shields
	// Noise - have enemies stop and look at radius for a second
	
	// Controller support
	// VVV - Screenshake?
	
	// SLOWMO? - bullets near? Player controlled?
	// Fix frame colliders
	// Fix Trailrenderers pt1 - reset trails pt2 - trail stay after bullets gone
	
	// Tint everything red at failure - shader?
}
