using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreensManager : MonoBehaviour
{
	enum Scenes { MAIN_MENU, IN_GAME};

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
		GameplayManager.ResetScoreFile();
		Application.Quit();
    }
}