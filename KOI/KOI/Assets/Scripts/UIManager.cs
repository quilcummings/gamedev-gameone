using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    private GameObject fishy;
    
    private int scoreCount;
    public TextMeshProUGUI score;
    public TextMeshProUGUI gameOver;
    
    public TextMeshProUGUI uhOh;
    
    private bool pause = false;

    void Start()
    {
        fishy = Movement.Instance.fishy;
    }

    void Update()
    {
        //display score 
        score.text = "Score: \n" + Movement.Instance.scoreCount.ToString();
        
        // pause game 
        if (fishy.transform.position.y < -140 && !pause)
        {
            Time.timeScale = 0;
            uhOh.text = "Uh-oh! It's getting dark! Collect glowing jellyfish to gain their light";
            pause = true;
        }
        if (Input.GetMouseButtonDown(0) && Movement.Instance.start && pause)
        {
            Time.timeScale = 1;
            Destroy(uhOh);
        }
        
        // display game over text
        if (Movement.Instance.died)
        {
            gameOver.text = "YOU DIED... click to reload";
        }

        if (Movement.Instance.noLight)
        {
            gameOver.text = "YOUR LIGHT HAS WANED... click to reload";
        }
    }
}
