using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonNetworkGameConnector : MonoBehaviourPunCallbacks
{
	private const int PLAYERS_AMOUNT_NEED = 2;


	static public void Connect()
	{
		if (PhotonNetwork.IsConnected)
			return;

		PhotonNetwork.NickName = "Pong Guy " + Random.Range(0, 1000);
		Events.showUserFriendlyLogEvent($"You are {PhotonNetwork.NickName}");
		PhotonNetwork.GameVersion = "1";
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.ConnectUsingSettings();
	}

	static public void Disconnect()
	{
		if (!PhotonNetwork.IsConnected)
			return;

		PhotonNetwork.Disconnect();
	}

	private void StartQuickMatch()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	private void CreateRoom()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = PLAYERS_AMOUNT_NEED;
		PhotonNetwork.CreateRoom(null, roomOptions, null);
	}

	private void CheckPlayersAmountInRoom()
	{
		int playersAmount = PhotonNetwork.CurrentRoom.PlayerCount;
		Events.showUserFriendlyLogEvent($"{playersAmount} player(s) in the room");

		if (playersAmount == PLAYERS_AMOUNT_NEED)
		{
			Events.showUserFriendlyLogEvent("Starting the game...");

			if (PhotonNetwork.IsMasterClient)
				PhotonNetwork.LoadLevel("Game");
		}
		else
		{
			Events.showUserFriendlyLogEvent("Waiting for an opponent...");
		}
	}

	#region Photon Callbacks
	public override void OnConnectedToMaster()
	{
		Events.showUserFriendlyLogEvent("You connected to server");
		StartQuickMatch();
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		Events.showUserFriendlyLogEvent("You disconnected from server");
	}

	public override void OnJoinedRoom()
	{
		Events.showUserFriendlyLogEvent("You joined a room");
		CheckPlayersAmountInRoom();
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		CreateRoom();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Events.showUserFriendlyLogEvent($"{newPlayer.NickName} joined a room");
		CheckPlayersAmountInRoom();
	}
	#endregion
}
