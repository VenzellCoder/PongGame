using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rigidbody;

	public float Speed { get; private set; }
	public float Size { get; private set; }

	private Vector2 moveDirectionNormal;
	private float lastRigidbodyVelocityMagnitude;
	

	private void Update()
	{
		CheckAndPreventStucking();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		switch(other.transform.tag)
		{
			case "IndestructibleObstacle":
			case "Paddle":
				BounceOff(other);
				UpdateRigidbodyVelocity();
				break;
			case "TopTouchdownZone":
				Events.ballReachTopTouchdownEvent?.Invoke();
				break;
			case "BottomTouchdownZone":
				Events.ballReachBottomTouchdownEvent?.Invoke();
				break;
		}
	}

	public void ResetAndRandomize()
	{
		SetRandomMoveSpeed();
		SetRandomSize();
		SetRandomDirection();
		ResetPosition();
		UpdateRigidbodyVelocity();
	}

	private void SetRandomMoveSpeed()
	{
		Speed = Random.Range(GameConfigs.ballMinSpeed, GameConfigs.ballMaxSpeed);
	}

	private void SetMoveSpeed(float speed)
	{
		Speed = speed;
	}

	private void SetRandomSize()
	{
		Size = Random.Range(GameConfigs.BallMinSize, GameConfigs.BallMaxSize);
		transform.localScale = new Vector3(Size, Size, Size);
	}

	private void SetSize(float size)
	{
		Size = size;
		transform.localScale = new Vector3(size, size, size);
	}

	private void SetRandomDirection()
	{
		moveDirectionNormal = Random.insideUnitCircle.normalized;
	}

	private void ResetPosition()
	{
		transform.position = Vector3.zero;
	}

	private void UpdateRigidbodyVelocity()
	{
		rigidbody.velocity = moveDirectionNormal * Speed;
		lastRigidbodyVelocityMagnitude = rigidbody.velocity.magnitude;
	}

	private void BounceOff(Collision2D other)
	{
		float deltaAngle = Vector2.SignedAngle(moveDirectionNormal, other.contacts[0].normal);
		moveDirectionNormal = Quaternion.Euler(0, 0, (180 + deltaAngle * 2)) * moveDirectionNormal;
	}

	private void UpdateSkin()
	{
		spriteRenderer.sprite = BallSkinData.GetSelectedBallSprite();
	}

	private void CheckAndPreventStucking()
	{
		if (IsBallStucking())
		{
			SetRandomDirection();
			UpdateRigidbodyVelocity();
		}
	}

	private bool IsBallStucking()
	{
		return rigidbody.velocity.magnitude < lastRigidbodyVelocityMagnitude;
	}

	private void OnReceiveNewParametersFromHost(float speed, float size)
	{
		SetMoveSpeed(speed);
		SetSize(size);
		UpdateRigidbodyVelocity();
	}

	private void OnEnable()
	{
		UpdateSkin();
		Events.receiveNewBallPropertiesOnClientEvent += OnReceiveNewParametersFromHost;
	}

	private void OnDisable()
	{
		Events.receiveNewBallPropertiesOnClientEvent -= OnReceiveNewParametersFromHost;
	}
}