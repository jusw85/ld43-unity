using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKey (KeyCode.U)) {
			transform.Translate (0f, 0f, 3f);

			//Vector3 position = transform.position;
			//position.z += 3f;
			//transform.position = position;

		}

		if (Input.GetKey (KeyCode.Y)) {
			transform.Translate (0f, 0f, -3f);

			//Vector3 position = transform.position;
			//position.z += -3f;
			//transform.position = position;
	}
		if (Input.GetKey (KeyCode.T)) {
			transform.localScale = new Vector3 (2f, 2f, 0f);

		}

		if (Input.GetKey (KeyCode.R)) {
			transform.localScale = new Vector3 (1f, 1f, 0f);

		}
		if (Input.GetKey (KeyCode.E)) {
			transform.localScale = new Vector3 (0.5f, 0.5f, 0f);

		}
}
}