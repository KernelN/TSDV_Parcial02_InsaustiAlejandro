using System.IO;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    #region Variables
    public UnityEngine.Events.UnityAction<int> OnScoreChange;
    public int score;
    [SerializeField] ShipController player;
    #endregion

    #region Unity Events
    private void Start()
    {
        player.OnPlayerLand += ShipLandedSuccesfully;
        player.OnPlayerDeath += ShipCrashed;
        LoadScore();
        OnScoreChange?.Invoke(score);
    }
    #endregion

    #region Methods
    void ShipLandedSuccesfully()
    {
        score += 100;
        SaveScore();
        ScreensManager.LoadInGame();
        Debug.Log("Win");
    }
    void ShipCrashed()
    {
        ResetScoreFile();
        ScreensManager.LoadInGame();
        Debug.Log("Lose");
    }
    void SaveScore()
    {
        //Create Folder and File if not existant
        if (!Directory.Exists(Application.dataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }
        if (!File.Exists(Application.dataPath + "/Saves/score.dat"))
        {
            File.Create(Application.dataPath + "/Saves/score.dat");
        }

        //Save Score
        FileStream fs = File.OpenWrite(Application.dataPath + "/Saves/score.dat");
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(score);
        bw.Close();
        fs.Close();
    }
    void LoadScore()
    {
        //Check if file exists before reading
        if (File.Exists(Application.dataPath + "/Saves/score.dat"))
        {
            FileStream fs = File.OpenRead(Application.dataPath + "/Saves/score.dat");
            BinaryReader br = new BinaryReader(fs);
            score = br.ReadInt32();
            br.Close();
            fs.Close();
        }
    }
    public static void ResetScoreFile()
    {
        if (File.Exists(Application.dataPath + "/Saves/score.dat"))
        {
            FileStream fs = File.OpenWrite(Application.dataPath + "/Saves/score.dat");
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(0);
            bw.Close();
            fs.Close();
        }
    }
    #endregion
}