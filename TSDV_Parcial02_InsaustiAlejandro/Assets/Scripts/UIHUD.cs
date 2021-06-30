using UnityEngine;
using TMPro;

public class UIHUD : MonoBehaviour
{
	#region Variables
	[SerializeField] TextMeshProUGUI score;
	[SerializeField] TextMeshProUGUI fuel;
	[SerializeField] TextMeshProUGUI time;
	[SerializeField] TextMeshProUGUI height;
	[SerializeField] TextMeshProUGUI verticalSpeed;
	[SerializeField] TextMeshProUGUI horizontalSpeed;
    #endregion

    #region Unity Events
    private void Update()
    {
        time.text = (Time.timeSinceLevelLoad / 60).ToString("00") + ":" + (Time.timeSinceLevelLoad % 60).ToString("00");
    }
    #endregion

    #region Methods

    #endregion
}