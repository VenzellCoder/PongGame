using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMover : MonoBehaviour
{
	private float speed = 5f;
	private float paddleWidth;


	private void Start()
	{
		paddleWidth = transform.localScale.x;
	}

	public void MoveRight()
	{
		if (IsPaddleBeyondRightCourtBoundary())
		{
			SetPositionToRightCourtBoundary();
			return;
		}

		transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
	}

	public void MoveLeft()
	{
		if (IsPaddleBeyondLeftCourtBoundary())
		{
			SetPositionToLeftCourtBoundary();
			return;
		}

		transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
	}

	private bool IsPaddleBeyondRightCourtBoundary()
	{
		return transform.position.x + paddleWidth / 2 > GameConfigs.courtWidthInUnits / 2;
	}

	private bool IsPaddleBeyondLeftCourtBoundary()
	{
		return transform.position.x - paddleWidth / 2 < -GameConfigs.courtWidthInUnits / 2;
	}

	private void SetPositionToRightCourtBoundary()
	{
		transform.position = new Vector3(GameConfigs.courtWidthInUnits / 2 - paddleWidth / 2, transform.position.y, transform.position.z);
	}

	private void SetPositionToLeftCourtBoundary()
	{
		transform.position = new Vector3(-GameConfigs.courtWidthInUnits / 2 + paddleWidth / 2, transform.position.y, transform.position.z);
	}
}
