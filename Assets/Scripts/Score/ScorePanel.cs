using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
	[SerializeField] private Text opponentScore;
	[SerializeField] private Text phoneOwnerScore;
	[SerializeField] private Text phoneOwnerBestScore;


	void Start()
    {
		OnUpdateScore();
		OnUpdatePhoneOwnerBestScore();
	}

	private void OnUpdateScore()
	{
		opponentScore.text = ScoreData.opponentScore.ToString();
		phoneOwnerScore.text = ScoreData.phoneOwnerScore.ToString();
	}

	private void OnUpdatePhoneOwnerBestScore()
	{
		phoneOwnerBestScore.text = ScoreData.bestScore.ToString();
	}

	private void OnEnable()
	{
		Events.updateScoreEvent += OnUpdateScore;
		Events.phoneOwnerHasNewBestScoreEvent += OnUpdatePhoneOwnerBestScore;
	}

	private void OnDisable()
	{
		Events.updateScoreEvent -= OnUpdateScore;
		Events.phoneOwnerHasNewBestScoreEvent -= OnUpdatePhoneOwnerBestScore;
	}
}
