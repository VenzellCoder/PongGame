using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class GameNetwork : MonoBehaviourPunCallbacks
{
	private const string BALL_PREFAB_NAME = "BallNetworVariant";
	private const string PADDLE_PREFAB_NAME = "PaddleNetworkVariant";
	private Ball ball;
	private const byte BALL_PROPERTIES_CODE = 1;
	private const byte SCORE_CODE = 2;


	private void Start()
	{
		InitializeGame();
	}

	private void InitializeGame()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			CreatePaddleOnHost();
			CreateBallOnHost();
		}
		else
		{
			CreatePaddleOnClient();
			Events.startNetworkGameAsClientEvent?.Invoke();
		}

		ScoreData.ResetCurentGameScore();
	}

	private void CreatePaddleOnHost()
	{
		GameObject paddle = PhotonNetwork.Instantiate(PADDLE_PREFAB_NAME, new Vector3(0f, -GameConfigs.paddleOffsetFromCenterInUnits, 0f), Quaternion.identity);
		PaddleMover mover = paddle.GetComponent<PaddleMover>();
		PlayerInput.phoneOwnerRightInput += mover.MoveRight;
		PlayerInput.phoneOwnerLeftInput += mover.MoveLeft;
	}

	private void CreatePaddleOnClient()
	{
		GameObject paddle = PhotonNetwork.Instantiate(PADDLE_PREFAB_NAME, new Vector3(0f, GameConfigs.paddleOffsetFromCenterInUnits, 0f), Quaternion.identity);
		PaddleMover mover = paddle.GetComponent<PaddleMover>();
		PlayerInput.phoneOwnerRightInput += mover.MoveLeft;
		PlayerInput.phoneOwnerLeftInput += mover.MoveRight; 
	}

	private void CreateBallOnHost()
	{
		ball = PhotonNetwork.Instantiate(BALL_PREFAB_NAME, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
		ball.ResetAndRandomize();
		SendBallPropertiesToClient();
	}

	private void OnBallReachTopTouchdownZoneOnHost()
	{
		if (!PhotonNetwork.IsMasterClient)
			return;

		ScoreData.IncreasePhoneOwnerScore();
		ball.ResetAndRandomize();

		SendBallPropertiesToClient();
		SendNewScoreToClient();
	}

	private void OnBallReachBottomTouchdownZoneOnHost()
	{
		if (!PhotonNetwork.IsMasterClient)
			return;

		ScoreData.IncreaseOpponentScore();
		ball.ResetAndRandomize();

		SendBallPropertiesToClient();
		SendNewScoreToClient();
	}

	private void OnPressGoToMainMenuButton()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.Disconnect();
		PlayerInput.ResetAllActionSubscriptions();
		SceneManager.LoadScene("MainMenu");
	}

	#region Photon callbacks
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		// Another player leaves --> force push exit button
		OnPressGoToMainMenuButton();

	}
	#endregion

	#region Photon events sending (host)
	private void SendBallPropertiesToClient()
	{
		object[] content = new object[] 
		{
			ball.Speed,
			ball.Size
		};

		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.Receivers = ReceiverGroup.Others;

		PhotonNetwork.RaiseEvent(BALL_PROPERTIES_CODE, content, raiseEventOptions, SendOptions.SendReliable);
	}

	private void SendNewScoreToClient()
	{
		object[] content = new object[]
		{
			ScoreData.opponentScore,
			ScoreData.phoneOwnerScore
		};

		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		raiseEventOptions.Receivers = ReceiverGroup.Others;

		PhotonNetwork.RaiseEvent(SCORE_CODE, content, raiseEventOptions, SendOptions.SendReliable);
	}
	#endregion

	#region Photon events receiving (client)
	private void OnEvent(EventData photonEvent)
	{
		if (PhotonNetwork.IsMasterClient)
			return;

		object[] data = null;
		try
		{
			data = (object[])photonEvent.CustomData;
		}
		catch
		{
			return;
		}

		byte eventCode = photonEvent.Code;

		switch (eventCode)
		{
			case BALL_PROPERTIES_CODE:
				ProcessBallPropertiesMessage(data);
				break;
			case SCORE_CODE:
				ProcessScoreMessage(data);
				break;
		}
	}

	private void ProcessBallPropertiesMessage(object[] data)
	{
		float speed = (float)data[0];
		float size = (float)data[1];
		Events.receiveNewBallPropertiesOnClientEvent(speed, size);
	}

	private void ProcessScoreMessage(object[] data)
	{
		ScoreData.SetPhoneOwnerScore((int)data[0]);
		ScoreData.SetOpponentScore((int)data[1]);
	}
	#endregion

	private void OnEnable()
	{
		base.OnEnable();
		PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
		Events.ballReachTopTouchdownEvent += OnBallReachTopTouchdownZoneOnHost;
		Events.ballReachBottomTouchdownEvent += OnBallReachBottomTouchdownZoneOnHost;
		Events.pressGoToMainMenuButtonEvent += OnPressGoToMainMenuButton;
	}

	private void OnDisable()
	{
		base.OnDisable();
		PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
		Events.ballReachTopTouchdownEvent -= OnBallReachTopTouchdownZoneOnHost;
		Events.ballReachBottomTouchdownEvent -= OnBallReachBottomTouchdownZoneOnHost;
		Events.pressGoToMainMenuButtonEvent -= OnPressGoToMainMenuButton;
	}
}
