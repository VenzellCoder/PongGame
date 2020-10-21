using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	[SerializeField] private Button goToMainMenuButton;


	private void Awake()
	{
		SubscribeButtons();
	}

	private void SubscribeButtons()
	{
		goToMainMenuButton.onClick.AddListener(OnPressGoToMainMenuButton);
	}

	private void OnPressGoToMainMenuButton()
	{
		Events.pressGoToMainMenuButtonEvent?.Invoke();
	}
}
