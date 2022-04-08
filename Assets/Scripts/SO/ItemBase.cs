using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    public abstract class ItemBase : ScriptableObject
    {
	    public string itemName;
	    public Sprite itemSprite;
	    public GameObject itemPrefab;
    }
}
