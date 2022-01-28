using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI を管理する manager クラス
// player が esc key を押した時 option 画面の表示

public class UIManager : MonoSingletone<UIManager>
{
    [SerializeField] GameObject OptionCanvas = null;
    [SerializeField] GameObject PlayerCanvas = null;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenuDisplay();
        }
    }
    // オプションメニューの表示・非表示の切り替え
    public void ToggleOptionMenuDisplay()
    {
        if(!OptionCanvas.activeSelf)
        {
            OptionCanvas.SetActive(true);
            PlayerCanvas.SetActive(false);
            CursorManager.Instance.UnLockCurcor();
            AudioManager.Instance.SetPausedSnapShot();
        }
        else if(OptionCanvas.activeSelf)
        {
            OptionCanvas.SetActive(false);
            PlayerCanvas.SetActive(true);
            CursorManager.Instance.LockCursor();
            AudioManager.Instance.SetUnPausedSnapShot();
        }
        TogglePauseState();
    }

    void TogglePauseState()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
