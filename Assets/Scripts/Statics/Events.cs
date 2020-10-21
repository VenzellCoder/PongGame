using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
	public static Action startNetworkGameAsClientEvent;
	public static Action<int> chooseBallSkinEvent;
	public static Action<string> showUserFriendlyLogEvent;

	public static Action ballReachTopTouchdownEvent;
	public static Action ballReachBottomTouchdownEvent;

	public static Action pressGoToMainMenuButtonEvent;

	public static Action updateScoreEvent;
	public static Action phoneOwnerHasNewBestScoreEvent;

	public static Action<float, float> receiveNewBallPropertiesOnClientEvent;
}
