using System;
using UnityEngine;

namespace Character.Player
{
    public class PlayerController : MonoBehaviour
    {
	    #region Variables

	    private PlayerController playerController;

	    #endregion

	    private void Awake()
	    {
		    if (playerController == null) playerController = this;
		    else Destroy(gameObject);
	    }
    }
}
