using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLocal : MonoBehaviour
{
	[SerializeField] private GameObject paddlePrefab;
	[SerializeField] private Ball ballPrefab;
	private Ball ball;


    void Start()
    {
		InitializeGame();
	}

	private void InitializeGame()
	{
		CreatePhoneOwnerPaddle();
		CreateOpponentPaddle();
		CreateBall();
		ScoreData.ResetCurentGameScore();
	}

	private void CreatePhoneOwnerPaddle()
	{
		GameObject paddle = Instantiate(paddlePrefab, new Vector3(0f, -GameConfigs.paddleOffsetFromCenterInUnits, 0f), Quaternion.identity);
		PaddleMover mover = paddle.GetComponent<PaddleMover>();
		PlayerInput.phoneOwnerRightInput += mover.MoveRight;
		PlayerInput.phoneOwnerLeftInput += mover.MoveLeft;
	}

	private void CreateOpponentPaddle()
	{
		GameObject paddle = Instantiate(paddlePrefab, new Vector3(0f, GameConfigs.paddleOffsetFromCenterInUnits, 0f), Quaternion.identity);
		PaddleMover mover = paddle.GetComponent<PaddleMover>();
		PlayerInput.opponentRightInput += mover.MoveRight;
		PlayerInput.opponentLeftInput += mover.MoveLeft;
	}

	private void CreateBall()
	{
		ball = Instantiate(ballPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
		ball.ResetAndRandomize();
	}

	private void OnBallReachTopTouchdownZone()
	{
		ScoreData.IncreasePhoneOwnerScore();
		ball.ResetAndRandomize();
	}

	private void OnBallReachBottomTouchdownZone()
	{
		ScoreData.IncreaseOpponentScore();
		ball.ResetAndRandomize();
	}

	private void OnPressGoToMainMenuButton()
	{
		PlayerInput.ResetAllActionSubscriptions();
		SceneManager.LoadScene("MainMenu");
	}

	private void OnEnable()
	{
		Events.ballReachTopTouchdownEvent += OnBallReachTopTouchdownZone;
		Events.ballReachBottomTouchdownEvent += OnBallReachBottomTouchdownZone;
		Events.pressGoToMainMenuButtonEvent += OnPressGoToMainMenuButton;
	}

	private void OnDisable()
	{
		Events.ballReachTopTouchdownEvent -= OnBallReachTopTouchdownZone;
		Events.ballReachBottomTouchdownEvent -= OnBallReachBottomTouchdownZone;
		Events.pressGoToMainMenuButtonEvent -= OnPressGoToMainMenuButton;
	}
}
