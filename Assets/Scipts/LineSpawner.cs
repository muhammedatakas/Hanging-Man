using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    public GameObject line;
    public Vector2 linePosition=new Vector2() { x=-10,y=3};
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawLine()
    {
        
        Instantiate(line,linePosition,line.transform.rotation);
        
        
    }
}
