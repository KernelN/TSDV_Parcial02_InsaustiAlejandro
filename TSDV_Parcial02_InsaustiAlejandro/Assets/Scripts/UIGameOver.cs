using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public bool playerWon;
    [SerializeField] TMPro.TextMeshProUGUI result; 

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
}