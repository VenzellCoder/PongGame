using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneRoot : MonoBehaviour
{
	[SerializeField] private GameObject gameLocalPrefab;
	[SerializeField] private GameObject gameNetworkPrefab;

	private void Awake()
	{
		switch(GameConfigs.gameType)
		{
			case GameType.local:
				Instantiate(gameLocalPrefab);
				break;
			case GameType.network:
				Instantiate(gameNetworkPrefab);
				break;
		}
	}
}
 