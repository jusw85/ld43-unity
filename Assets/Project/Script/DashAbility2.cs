using UnityEngine;
using System.Collections;

public class DashAbility2 : MonoBehaviour {

	public Transform buildPoint;
	public GameObject buildingBlock;

	bool facingRight = true;

	public DashState dashState;
	public float dashTimer;
	public float maxDash;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (dashState) 
		{
		case DashState.Ready:

			var isDashKeyDown = (Input.GetKeyDown (KeyCode.M) && facingRight);
			if (isDashKeyDown) 
			{
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				transform.Translate (0.6f, 0f, 0f);
				Instantiate (buildingBlock, buildPoint.position, buildPoint.rotation);
				dashState = DashState.Dashing;
			}
			break;

		case DashState.Dashing:
			dashTimer += Time.deltaTime * 1;
			if (dashTimer >= maxDash) {
				dashTimer = maxDash;
				dashState = DashState.Cooldown;
			}
			break;

		case DashState.Cooldown:
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0) 
			{
				dashTimer = 0;
				dashState = DashState.Ready;
			}
			break;
		}

	}
		
	public enum DashState
	{
		Ready,
		Dashing,
		Cooldown
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}


