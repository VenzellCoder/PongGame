using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiLogger : MonoBehaviour
{
	[SerializeField] private Text logText;


	private void OnEnable()
	{
		Events.showUserFriendlyLogEvent += ShowLog;
	}

	private void OnDisable()
	{
		Events.showUserFriendlyLogEvent -= ShowLog;
	}

	private void ShowLog(string message)
	{
		logText.text += message + "\n";
	}
}
