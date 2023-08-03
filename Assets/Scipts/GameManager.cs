using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public static bool isGameOver;

    private bool isGameLost;

    [SerializeField]
    LetterSpawner letterSpawner;
    public string word;
    bool isTextFounded;
    public char selectedLetter;
    private int listIndex=0;
    private int tryCount=10;
    
    public int score=10;
    public int maxScore;
    public string NameOfMaxScorer;
    [SerializeField]
    private LineSpawner lineSpawner;
    [SerializeField]
    private List<GameObject> lineManPrefabs;
    [SerializeField]
    public TMP_InputField inputField;
    [SerializeField]
    GameObject winPanel;
    [SerializeField]
    GameObject lostPanel;
    [SerializeField]
    GameObject gameobject;
    public int emptyFields;
    public static List<string> words = new List<string>() { "body", "brain", "cards", "design", "dress", "elephant", "flowers", "furniture", "garden", "guitar", "house", "jeans", "jewelry", 
        "keyboard", "lamp", "machine", "map", "money", "necktie", "office", "paintbrush", "penknife", "piano", "picture", "plant", "police officer", "postcard", "purse", "question mark",
        "raincoat", "razor", "schoolbag", "scissors", "suit", "suitcase", "tie", "toothbrush", "toothpaste", "umbrella","ibne sehmus","html furkan","sapik apo","dinsiz sinan", };
    // Start is called before the first frame update

    
    private Keyboard keyboard;

    SaveData data= new SaveData();
void Start() 
    {
        

        StartState();


        
        
    }private void Awake()
    {
        keyboard = InputSystem.GetDevice<Keyboard>();
        
    }
        
        
    public void StartState()
    {
        int random = Random.Range(0, words.Count);
        word = words[random];
        emptyFields = word.Length;
        DrawLines();


    }
    private void OnEnable()
    {
        // Enable the input system
        InputSystem.EnableDevice(Keyboard.current);
    }

    private void OnDisable()
    {
        // Disable the input system
        InputSystem.DisableDevice(Keyboard.current);
    }



    void OnKeyboardInput()
    {

        for (KeyCode key = KeyCode.A; key <= KeyCode.Z; key++)
        {
            isTextFounded = false;
            if (Input.GetKeyDown(key) && !isGameOver)
            {
                selectedLetter = (char)key;
                Method();
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Quote) && !isGameOver)
        {
            selectedLetter = (char)KeyCode.I;
            Method();
        }
       
        Debug.Log(selectedLetter + " was pressed");
       
    }
    private void Method()
    {
        if (letterSpawner.textMeshPros != null)
        {
            foreach (var text in letterSpawner.textMeshPros)

            {
                if (text != null && text.text == selectedLetter.ToString() || text.text == selectedLetter.ToString().ToUpper())
                {

                    isTextFounded = true;
                    text.gameObject.SetActive(true);
                    emptyFields--;

                }

            }


        }


        if (!isTextFounded)
        {
            lineManPrefabs[listIndex].gameObject.SetActive(true);
            listIndex++;
            tryCount--;
            score--;
            UpdateTryCount();
        }
    }
            
        
    

    public void OnSubmitButtonClicked()
    {
        NameOfMaxScorer = inputField.text;
       
        UpdateScoreCount();

    }
    void UpdateScoreCount()
    {
        LoadData();
        if (score > data.maxScore )
        {
            maxScore = score;
            SaveScores();
            
        }
    }
    void WinGame()
    {
        if (emptyFields == 0)
        {
            isGameOver = true;
            gameobject.SetActive(false);
            winPanel.gameObject.SetActive(true);


            GameObject.Find("CurrentScore").GetComponent<TextMeshProUGUI>().text = "Your Score " + score;

            GameOver();
           
        }
    }void LostGame()
    {
        if(isGameLost)
        {
            isGameOver = true;
            gameobject.SetActive(false);
            lostPanel.gameObject.SetActive(true);

            GameObject.Find("CurrentScore").GetComponent<TextMeshProUGUI>().text = "Your Score " + score;
            GameOver();
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
            
        
        }
    }

    void UpdateTryCount()
    {
        TextMeshProUGUI tryLeft=GameObject.Find("TryCount").GetComponent<TextMeshProUGUI>();
        tryLeft.text = "You have " + tryCount + " tries left";
        if(tryCount == 0)
        {
            isGameLost = true;
        }
    }
    
    void GameOver()
    {
        foreach (GameObject @object in GameObject.FindGameObjectsWithTag("Letter"))
        {
Destroy(@object);
        }
        foreach (GameObject @object in GameObject.FindGameObjectsWithTag("Line")) {
            Destroy(@object);
        }
        Destroy(GameObject.Find("HangingMan"));
        

    }
    public void ChangeSceneOnSubmit()
    {
        SceneManager.LoadScene(0);
    }




    
    // Update is called once per frame
    void Update()
    {
        OnKeyboardInput();
        WinGame();
        LostGame();
    }

    [System.Serializable]
    public class SaveData
    {
        public int maxScore;
        public string name;
    }
    public void SaveScores()
    {
        
        data.maxScore = maxScore;
        data.name = NameOfMaxScorer;
        string jsonString=JsonUtility.ToJson(data);

        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "playerData.json");
        File.WriteAllText(filePath, jsonString);

    }
    public void LoadData()
    {
        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "playerData.json");

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(jsonString);
        }
        else
        {
            // Handle the case when the file does not exist.
            Debug.LogWarning("playerData.json file not found. No data loaded.");
        }



    }




    public void DrawLines()
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] != ' ')
            {
                lineSpawner.DrawLine();
                letterSpawner.CreateLetter(i);
                lineSpawner.linePosition.x += 1.5f;
                LetterSpawner.isCapitalized = false;
            }
            else {
                LetterSpawner.isCapitalized = true;
                lineSpawner.linePosition.x += 2; }
        }
    }
    




    

}
