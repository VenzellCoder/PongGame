using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritesCollection", menuName = "ScriptableObjects/SpritesCollection")]
public class SoSpritesCollection : ScriptableObject
{
	public List<CollectibleSprite> sprites;
}

[System.Serializable]
public class CollectibleSprite
{
	public int id;
	public Sprite sprite;
}
