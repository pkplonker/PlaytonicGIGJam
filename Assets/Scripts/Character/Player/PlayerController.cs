using System;
using UnityEngine;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
	    #region Variables

	    public static PlayerController instance;

	    #endregion

	    private void Awake()
	    {
		    if (instance == null) instance = this;
		    else Destroy(this);
	    }
    }
}
