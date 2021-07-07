using UnityEngine;
using TMPro;

public class UIHUD : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI score;
	[SerializeField] TextMeshProUGUI fuel;
	[SerializeField] TextMeshProUGUI time;
	[SerializeField] TextMeshProUGUI height;
	[SerializeField] TextMeshProUGUI verticalSpeed;
	[SerializeField] TextMeshProUGUI horizontalSpeed;
	[SerializeField] ShipController player;
	[SerializeField] GameplayManager gameplayManager;
    float maxFuel = 0;

    #region Unity Events
    private void Start()
    {
        maxFuel = player.maxFuel;
        gameplayManager.OnScoreChange += UpdateScore;
        player.OnFuelChange += UpdateFuel;
        player.OnAltitudeChange += UpdateHeight;
        player.OnVerticalSpeedChange += UpdateVSpeed;
        player.OnHorizontalSpeedChange += UpdateHSpeed;
    }
    private void Update()
    {
        time.text = (Time.timeSinceLevelLoad / 60).ToString("00") + ":" + (Time.timeSinceLevelLoad % 60).ToString("00");
    }
    #endregion

    #region Methods
    void UpdateScore(int newScore)
    {
        score.text = newScore.ToString("0000");
    }
    void UpdateFuel(float newFuel)
    {
        if (maxFuel == 0)
        {
            maxFuel = newFuel;
        }
        fuel.text = (newFuel / maxFuel).ToString("00%");
    }
    void UpdateHeight(float newHeight)
    {
        height.text = newHeight.ToString("0");
    }
    void UpdateVSpeed(float speed)
    {
        if ((int)speed > 0)
        {
            verticalSpeed.text = speed.ToString("↑0");
        }
        else if ((int)speed < 0)
        {
            verticalSpeed.text = (-speed).ToString("↓0");
        }
        else
        {
            verticalSpeed.text = "0";
        }
    }
    void UpdateHSpeed(float speed)
    {
        if ((int)speed > 0)
        {
            horizontalSpeed.text = speed.ToString("0→");
        }
        else if ((int)speed < 0)
        {
            horizontalSpeed.text = (-speed).ToString("←0");
        }
        else
        {
            horizontalSpeed.text = "0";
        }
    }
    #endregion
}