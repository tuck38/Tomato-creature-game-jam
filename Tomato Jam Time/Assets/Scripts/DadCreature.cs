using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadCreature : MonoBehaviour
{
	[Tooltip("Reference to the Player GameObject")]
	[SerializeField] Transform playerTransform;
	[Tooltip("Reference to the Camera GameObject")]
	[SerializeField] Transform cameraTransform;

	[Range(80.0f, 180.0f)]
	public float vanishAngle = 90f;

	bool canTeleport = false;

	float baseAxisY = 0;
	float baseDistance = 0;

	// Start is called before the first frame update
	void Start()
    {
		baseAxisY = transform.position.y;
		baseDistance = (transform.position - playerTransform.position).magnitude;
	}

    // Update is called once per frame
    void Update()
    {
		// make creature look at player
		Vector3 playerCreatureVector = transform.position - playerTransform.position;
		transform.forward = playerCreatureVector;
		// this currently forces a "looming" perspective, if you want it to just face the player flat, do this:
		// transform.forward = new Vector3(playerCreatureVector.x, transform.forward.y, playerCreatureVector.z);

		// check player isn't looking at monster
		var playerLookingAngle = Vector3.Angle(transform.position, cameraTransform.forward);
		if (playerLookingAngle >= vanishAngle)
		{
			if (canTeleport == true)
			ScaryTeleport();
		}
		else if (playerLookingAngle < vanishAngle)
		{
			canTeleport = true;
		}
	}

	void ScaryTeleport()
	{
		// teleport behind the player (omae wa mou) but also in the same range 
		var teleportDirection = (cameraTransform.forward * -1).normalized * baseDistance;

		transform.position = new Vector3(teleportDirection.x, baseAxisY, teleportDirection.z);

		canTeleport = false;
	}
}
