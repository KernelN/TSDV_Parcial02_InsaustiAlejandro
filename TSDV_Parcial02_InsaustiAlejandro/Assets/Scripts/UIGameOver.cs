using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public bool playerWon;
    [SerializeField] TMPro.TextMeshProUGUI result;
    [SerializeField] ScreensManager scene;
    float gameOverCooldown;

    private void Update()
    {
        if (gameOverCooldown < 25)
        {
            gameOverCooldown += 1;
            return;
        }

        ResetOnSpaceKeyDown();
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        SetResult();
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    void SetResult()
    {
        result.text += playerWon ? "Landed!" : "Crashed";
    }
    void ResetOnSpaceKeyDown()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            scene.LoadInGame();
        }
    }
}