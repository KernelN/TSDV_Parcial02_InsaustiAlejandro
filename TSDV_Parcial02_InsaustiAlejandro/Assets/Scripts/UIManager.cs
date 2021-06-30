using UnityEngine;

public class UIManager : MonoBehaviour
{
	#region Variables
	[SerializeField] GameObject HUD;
	[SerializeField] GameObject pause;
	[SerializeField] GameObject gameOver;
	[SerializeField] ShipController player;
    bool playerWinned;
    #endregion

    #region Unity Events
    private void Start()
    {
        player.OnPlayerDeath += SetDefeatScreen;
        player.OnPlayerLand += SetVictoryScreen;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver.activeSelf)
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
    void SetDefeatScreen()
    {
        gameOver.GetComponent<UIGameOver>().playerWon = false;
        gameOver.SetActive(true);
        HUD.SetActive(false);
    }
    void SetVictoryScreen()
    {
        gameOver.GetComponent<UIGameOver>().playerWon = true;
        gameOver.SetActive(true);
        HUD.SetActive(false);
    }
    #endregion
}