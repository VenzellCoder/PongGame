using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class BallSkinData
{
	static public int currentBallSkinId;
	static public SoSpritesCollection ballSkinsCollection;

	private const string PLAYER_PREF_BALL_SKIN_ID_KEY = "ballSkinId";


	static BallSkinData()
	{
		LoadBallSkinsCollection();
		ReadCurrentBallSkinId();
		Events.chooseBallSkinEvent += WriteCurrentBallSkinId;
	}

	static private void ReadCurrentBallSkinId()
	{
		currentBallSkinId = PlayerPrefs.GetInt(PLAYER_PREF_BALL_SKIN_ID_KEY, 1);
	}

	static private void WriteCurrentBallSkinId(int id)
	{
		currentBallSkinId = id;
		PlayerPrefs.SetInt(PLAYER_PREF_BALL_SKIN_ID_KEY, id);
	}

	static private void LoadBallSkinsCollection()
	{
		ballSkinsCollection = Resources.Load<SoSpritesCollection>("BallSkinsCollection");
	}

	static public Sprite GetSelectedBallSprite()
	{
		return ballSkinsCollection.sprites.Find(s => s.id == currentBallSkinId).sprite;
	}
}
