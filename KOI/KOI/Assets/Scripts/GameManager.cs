using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Movement.Instance.noLight && Input.GetMouseButtonDown(0) || Movement.Instance.died && Input.GetMouseButtonDown(0))
        {
            Movement.Instance.died = false;
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }
}
