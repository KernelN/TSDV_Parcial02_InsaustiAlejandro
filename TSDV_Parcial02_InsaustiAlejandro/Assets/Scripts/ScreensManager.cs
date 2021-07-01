using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreensManager : MonoBehaviour
{
	enum Scenes { MAIN_MENU, IN_GAME};

    private void OnApplicationQuit()
    {
		GameplayManager.ResetScoreFile();
	}

	public void LoadMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }
	public void LoadInGame()
	{
		SceneManager.LoadScene("InGame");
	}
	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}
	public void Quit()
    {
		Application.Quit();
    }
}