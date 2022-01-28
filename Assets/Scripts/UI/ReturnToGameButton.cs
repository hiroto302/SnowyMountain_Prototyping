using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToGameButton : MonoBehaviour
{
    public void ReturnToGame()
    {
        UIManager.Instance.ToggleOptionMenuDisplay();
    }
}
