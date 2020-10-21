using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Button playLocalMultiplayerButton;
	[SerializeField] private Button playNetworkMultiplayerButton;
	[SerializeField] private Button cancelNetworkMultiplayerButton;
	[SerializeField] private Button openBallSkinsPanelButton;
	[SerializeField] private BallSkinsPanel ballSkinsPanel;


	private void Awake()
	{
		SubscribeButtons();
	}

	private void SubscribeButtons()
	{
		playLocalMultiplayerButton.onClick.AddListener(OnPressPlayLocalMultiplayerButton);
		playNetworkMultiplayerButton.onClick.AddListener(OnPressPlayNetworkMultiplayerButton);
		cancelNetworkMultiplayerButton.onClick.AddListener(OnPressCancelNetworkMultiplayerButton);
		openBallSkinsPanelButton.onClick.AddListener(OnPressOpenBallSkinsPanelButtonButton);
	}

	private void OnPressPlayLocalMultiplayerButton()
	{
		GameConfigs.gameType = GameType.local;
		PhotonNetworkGameConnector.Disconnect();
		SceneManager.LoadScene("Game");
	}

	private void OnPressPlayNetworkMultiplayerButton()
	{
		GameConfigs.gameType = GameType.network;
		PhotonNetworkGameConnector.Connect();
	}

	private void OnPressCancelNetworkMultiplayerButton()
	{
		PhotonNetworkGameConnector.Disconnect();
	}

	private void OnPressOpenBallSkinsPanelButtonButton()
	{
		ballSkinsPanel.ShowPanel();
	}
}
