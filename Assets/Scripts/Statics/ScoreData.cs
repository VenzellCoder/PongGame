using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ScoreData
{
	static public int bestScore;
	static public int opponentScore;
	static public int phoneOwnerScore;

	private const string PLAYER_PREF_BEST_SCORE_KEY = "bestScore";


	static ScoreData()
	{
		ReadBestScore();
	}

	static public void ResetCurentGameScore()
	{
		opponentScore = 0;
		phoneOwnerScore = 0;
		Events.updateScoreEvent?.Invoke();
	}

	static public void IncreaseOpponentScore()
	{
		opponentScore++;
		Events.updateScoreEvent?.Invoke();
	}

	static public void SetOpponentScore(int value)
	{
		opponentScore = value;
		Events.updateScoreEvent?.Invoke();
	}

	static public void IncreasePhoneOwnerScore()
	{
		phoneOwnerScore++;
		Events.updateScoreEvent?.Invoke();
		CheckForNewBestScoreAndWrite();
	}

	static public void SetPhoneOwnerScore(int value)
	{
		phoneOwnerScore = value;
		Events.updateScoreEvent?.Invoke();
		CheckForNewBestScoreAndWrite();
	}

	static void CheckForNewBestScoreAndWrite()
	{
		if (phoneOwnerScore > bestScore)
		{
			bestScore = phoneOwnerScore;
			WriteBestScore(phoneOwnerScore);
			Events.phoneOwnerHasNewBestScoreEvent?.Invoke();
		}
	}

	static private void ReadBestScore()
	{
		bestScore = PlayerPrefs.GetInt(PLAYER_PREF_BEST_SCORE_KEY, 0);
	}

	static private void WriteBestScore(int newBestScore)
	{
		PlayerPrefs.SetInt(PLAYER_PREF_BEST_SCORE_KEY, newBestScore);
	}
}
