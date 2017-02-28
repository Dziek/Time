using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	
	public GameObject receiver; // the item to trigger
	
	[Tooltip("How long the trigger effect will last - 0 means unlimited")]
	public float timeActive; // how long the trigger effect will last
	
	// public bool boostTrigger;
	public bool shootTrigger;
	
	public bool oneShot; // can only be triggered once, but stays deactivated
	public bool oneShotDestroy; // destroys self after touch
	
	public Color normalActiveColor;
	public Color boostActiveColor;
	public Color shootActiveColor;
	public Color deactiveColor;
	
	private Color activeColor;
	private bool active = true;
	
	private SpriteRenderer sR;
	
	// Use this for initialization
	void Awake () {
		receiver.SendMessage("WaitForTrigger");
		// activated = GetComponent<Renderer>().material;
		
		// if (boostTrigger)
		// {
			// activeColor = boostActiveColor;
		// }else if (shootTrigger)
		// {
			// activeColor = shootActiveColor;
		// }else{
			// activeColor = normalActiveColor;
		// }
		
		sR = GetComponent<SpriteRenderer>();
		sR.color = activeColor;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{	
		if (active)
		{
			if (other.tag == "Player" && !shootTrigger)
			{
				// if (boostTrigger)
				// {
					// if (other.GetComponent<PlayerControl>().GetBoosting())
					// {
						// ActivateTrigger();
					// }
				// }else{
					ActivateTrigger();
				// }
			}
			
			if (other.tag == "Projectile" && shootTrigger)
			{
				ActivateTrigger();
			}

		}
	}
	
	void ActivateTrigger () {
		receiver.SendMessage("TriggerActivated", timeActive);
		sR.color = deactiveColor;
		active = false;
		if (oneShotDestroy)
		{
			Destroy(gameObject);
		}
		if (!oneShot)
		{
			StartCoroutine("Reactivate");
		}
	}
	
	IEnumerator Reactivate () {
		
		float timer = 0;
		
		while (timer < timeActive)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		
		yield return null;
		sR.color = activeColor;
		active = true;
	}
}
