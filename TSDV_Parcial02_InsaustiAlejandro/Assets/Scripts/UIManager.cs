using UnityEngine;

public class UIManager : MonoBehaviour
{
	#region Variables
	[SerializeField] GameObject HUD;
	[SerializeField] GameObject pause;
	#endregion

    #region Unity Events
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }
    #endregion

    #region Methods
    void SetPause()
    {
        bool gameIsPaused = !pause.activeSelf;
        HUD.SetActive(!gameIsPaused);
        pause.SetActive(gameIsPaused);
    }
    #endregion
}