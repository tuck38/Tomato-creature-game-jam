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
	[SerializeField] float baseDistance = 0;

	// Start is called before the first frame update
	void Start()
    {
		baseAxisY = transform.position.y;
		baseDistance = (playerTransform.position - transform.position).magnitude;
	}

    // Update is called once per frame
    void Update()
    {
		// make creature look at player
		Vector3 playerCreatureVector = playerTransform.position - transform.position;
		transform.forward = new Vector3(playerCreatureVector.x, transform.forward.y, playerCreatureVector.z);

		// check player isn't looking at monster
		var playerLookingAngle = Vector3.Angle(transform.position - playerTransform.position, cameraTransform.forward);
		print(playerLookingAngle);
		if (playerLookingAngle >= vanishAngle)
		{
			if (canTeleport == true)
			ScaryTeleport();
		}
		else if (playerLookingAngle < vanishAngle)
		{
			canTeleport = true;
		}

		//------------------------------------------------------------------------------------------------------------
		var leftAngle = Quaternion.Euler(cameraTransform.up * (vanishAngle * 0.5f)) * cameraTransform.forward;
		var rightAngle = Quaternion.Euler(cameraTransform.up * -(vanishAngle * 0.5f)) * cameraTransform.forward;
		leftAngle.Normalize();
		rightAngle.Normalize();

		Debug.DrawRay(cameraTransform.position, leftAngle, Color.yellow);
		Debug.DrawRay(cameraTransform.position, rightAngle, Color.yellow);
	}

	void ScaryTeleport()
	{
		var leftAngle = Quaternion.Euler(cameraTransform.up * (vanishAngle * 0.5f)) * cameraTransform.forward;
		var rightAngle = Quaternion.Euler(cameraTransform.up * -(vanishAngle * 0.5f)) * cameraTransform.forward;
		leftAngle.Normalize();
		rightAngle.Normalize();

		var viewAngle = Vector3.Angle(leftAngle, rightAngle);
		var behindAngle = 360 - viewAngle;
		var randomBehindAngle = viewAngle + Random.Range(0, behindAngle);
		var randomBehindDirection = Quaternion.Euler(cameraTransform.up * (randomBehindAngle * 0.5f)) * cameraTransform.forward;
		randomBehindDirection = randomBehindDirection.normalized * baseDistance;

		// teleport behind the player (omae wa mou) but also in the same range 
		//var teleportDirection = (cameraTransform.forward * -1).normalized * baseDistance;

		var teleportLocation = playerTransform.position + randomBehindDirection;
		transform.position = new Vector3(teleportLocation.x, baseAxisY, teleportLocation.z);

		canTeleport = false;
	}
}
