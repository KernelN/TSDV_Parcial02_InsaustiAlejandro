using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreensManager : MonoBehaviour
{
	enum Scenes { MAIN_MENU, IN_GAME};

	public static void LoadMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }
	public static void LoadInGame()
	{
		SceneManager.LoadScene("InGame");
	}
	public static void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}
	public static void Quit()
    {
		GameplayManager.ResetScoreFile();
		Application.Quit();
    }
}