using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSkinPreview : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private GameObject selectionIndicator;
	[SerializeField] private Button selectButton;
	public int Id { get; private set; }
	private Action<BallSkinPreview> callbackOnSelect;


	public void Initialize(CollectibleSprite collectibleSprite, Action<BallSkinPreview> callbackOnSelect)
	{
		image.sprite = collectibleSprite.sprite;
		Id = collectibleSprite.id;
		this.callbackOnSelect = callbackOnSelect;
		selectButton.onClick.AddListener(OnPressSelectButton);
	}

	public void ToggleSelection(bool isSelected)
	{
		selectionIndicator.SetActive(isSelected);
	}

	private void OnPressSelectButton()
	{
		callbackOnSelect?.Invoke(this);
	}
}
