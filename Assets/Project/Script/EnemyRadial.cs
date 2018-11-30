using UnityEngine;
using System.Collections;

public class EnemyRadial : MonoBehaviour {

	public float MinSpeed = 1f;

	public float MaxSpeed = 1f;

	public float currentSpeed;

	public Transform Projectile;

	public float[] angles = new float[] {-40f, -35f, -25f, -10f, 0, 10f, 25f, 35f, 40f};

	//public float angles = 40f;

	private float x,y,z;

		float firingRate = 10.0f;

		float lastFired = -100f;


	void Start () 
	{
		//currentSpeed = Random.Range (MinSpeed, MaxSpeed);
		currentSpeed = 0;
		x = Random.Range (0f, 1f);
		transform.position = new Vector3 (x, 0.0f, 0.0f);
	
	}
	

	void Update () 
	{
		//float amtToMove = currentSpeed * Time.deltaTime;
		//transform.Translate (-Vector3.up * amtToMove);

		//if( lastFired +firingRate)
			//return;

		//if(transform.position.y &Lt; = 6.0)
		//{
			//currentSpeed= Random.Range(MinSpeed, MaxSpeed);
			//transform.position = new Vector3(x, 7.0f,0.0f);
		//}

		//lastFired= Time.time;

		//if (transform.position.y &gt;= 0 &amp;&amp; transform.position.y &lt;= 1)

			//Vector3 rightposition = new Vector3(transform.position.x + transform.localScale.x * 1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, rightposition, transform.rotation);
			//Vector3 right2position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right2position, Quaternion.Euler(0, 20, 20));
			//Vector3 right3position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right3position, Quaternion.Euler(0, 40, 40));
			//Vector3 right4position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right4position, Quaternion.Euler(0, 60, 60));
			//Vector3 right5position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right5position, Quaternion.Euler(0, 70, 70));
			//Vector3 right6position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right6position, Quaternion.Euler(0, 80, 80));
			//Vector3 right7position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right7position, Quaternion.Euler(0, 85, 85));
			//Vector3 right8position = new Vector3(transform.position.x + transform.localScale.x * -1, transform.position.y + transform.localScale.y * 1);
			//Instantiate(Projectile, right8position, Quaternion.Euler(0, 90, 90));

		foreach(float angle in angles) 
		{
			//Transform instance = (Instantiate(Projectile, transform.position, transform.rotation)
				//as GameObject).transform;

			//Vector3 v = instance.eulerAngles;

			//v.z += angle; //rotate


			//instance.position += -instance.up * (new Vector2(transform.localScale.x * 8f,
				//transform.localScale.y * 5f)).magnitude; //offset
		}


		//if(transform.position.y &lt; 0f || transform.position.y &gt; 1f)
			//return;

		//Vector3 localShotPos = new Vector3 (0, -((new Vector2((transform.localScale.x*8f, transform.localScale.y*5f)).magnitude));

			//foreach(float angle in angles)
			//{
				//Quaternion rotation = Quaternion.AngleAxis(angle,transform.forward);
				//Vector3 shotPosition = transform.position + rotation * localShotPos;
				//Instantiate(Projectile, shotPosition, rotation * transform.rotation);
			//}
	}


}
