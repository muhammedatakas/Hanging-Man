using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public TextMeshProUGUI letterTextPrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LineSpawner lineSpawner;
    private Canvas canvas;
    public static bool isCapitalized;
    private TextMeshProUGUI newChildObject;
    public List<TextMeshProUGUI> textMeshPros = new List<TextMeshProUGUI>(); // Initialize the list

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void CreateLetter(int i)
    {
        newChildObject = Instantiate(letterTextPrefab, lineSpawner.linePosition, transform.rotation);
        newChildObject.transform.SetParent(GameObject.Find("Canvas").transform);
        textMeshPros.Add(newChildObject);
        newChildObject.transform.Translate(0, 1, 0);

        if (isCapitalized || i == 0)
        {
            newChildObject.text = gameManager.word[i].ToString().ToUpper();
        }
        else
        {
            newChildObject.text = gameManager.word[i].ToString();
        }

        newChildObject.gameObject.SetActive(false);
    }
}