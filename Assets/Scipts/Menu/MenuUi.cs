using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using System.IO;

public class MenuUi : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;

    SaveData loadedData;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        MaxScore();
    }

    public void PlayTheSong()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        audioSource.volume = slider.value;
        DontDestroyOnLoad(audioSource);
    }

    public void MaxScore()
    {
        if( loadedData!=null) { GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Max score " + loadedData.name+" : "+ loadedData.maxScore; }
        else { GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Max score: " + 0; }
    }

    public void LoadMainScene()
    {
        GameManager.isGameOver = false;
        
        SceneManager.LoadScene("MainScene");
        
    }
    public void LoadData()
    {
        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "playerData.json");

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<SaveData>(jsonString);
        }
        else
        {
            // Handle the case when the file does not exist.
            Debug.LogWarning("playerData.json file not found. No data loaded.");
        }



    }
    public void ResetScore()
    {
        SaveData data = new SaveData();
        data.maxScore = 0;
        data.name = "";
        string jsonString = JsonUtility.ToJson(data);

        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "playerData.json");
        File.WriteAllText(filePath, jsonString);
        LoadData();
        MaxScore();
    }
}

