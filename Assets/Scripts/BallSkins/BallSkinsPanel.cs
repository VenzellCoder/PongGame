using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSkinsPanel : MonoBehaviour
{
	[SerializeField] private Transform previewsContainer;
	[SerializeField] private BallSkinPreview previewPrefab;
	[SerializeField] private Button closeButton;

	private BallSkinPreview selectedPreview;


	void Start()
    {
		CreatePreviews();
		SubscribeButtonsOnPanel();
	}

	public void ShowPanel()
	{
		gameObject.SetActive(true);
	}

	public void HidePanel()
	{
		gameObject.SetActive(false);
	}

	private void SubscribeButtonsOnPanel()
	{
		closeButton.onClick.AddListener(HidePanel);
	}

	private void CreatePreviews()
	{
		foreach(CollectibleSprite collectibleSprite in BallSkinData.ballSkinsCollection.sprites)
		{
			BallSkinPreview preview = Instantiate(previewPrefab);
			preview.transform.SetParent(previewsContainer, false);
			preview.Initialize(collectibleSprite, OnPressBallSkinPreview);

			if (BallSkinData.currentBallSkinId == collectibleSprite.id)
			{
				preview.ToggleSelection(true);
				selectedPreview = preview;
			}
		}
	}

	private void OnPressBallSkinPreview(BallSkinPreview newSelectedPrevieweview)
	{
		selectedPreview.ToggleSelection(false);
		newSelectedPrevieweview.ToggleSelection(true);
		selectedPreview = newSelectedPrevieweview;

		Events.chooseBallSkinEvent?.Invoke(newSelectedPrevieweview.Id);
	}
}
