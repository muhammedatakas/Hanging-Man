using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;



public class GameManager : MonoBehaviour
{
   public char pressedLetter;
    public string word;
    LineSpawner lineSpawner;
    public static List<string> words = new List<string>() { "body", "brain", "cards", "design", "dress", "elephant", "flowers", "furniture", "garden", "guitar", "house", "jeans", "jewelry", 
        "keyboard", "lamp", "machine", "map", "money", "necktie", "office", "paintbrush", "penknife", "piano", "picture", "plant", "police officer", "postcard", "purse", "question mark",
        "raincoat", "razor", "schoolbag", "scissors", "suit", "suitcase", "tie", "toothbrush", "toothpaste", "umbrella" };
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, words.Count);
        word = words[random];

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    class SaveData
    {
        public string word;
    }
    public void SaveWords()
    {
        SaveData data = new SaveData();
        
        
       
    }
    public void ControlWordInput()
    {
        char pressedLetter = (char)Input.inputString.ToCharArray()[0];
        if (word.Contains(pressedLetter))
        {
            
        }

    }
    void OnKeyboardInput() { 
    if(Input.anyKeyDown)
        {
            pressedLetter=(char)Input.inputString.ToCharArray()[0];
        }
    }
   public void DrawLines()
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] != ' ')
            {
                lineSpawner.DrawLine();
            }else { lineSpawner.linePosition.x += 2; }
        }
    }
    

}
