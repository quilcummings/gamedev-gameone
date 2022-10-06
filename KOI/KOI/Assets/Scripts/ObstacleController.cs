using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public static ObstacleController Instance;
    
    public GameObject urchinPF;
    public GameObject trashPF;
    public GameObject lightPF;

    private GameObject urchin;
    private GameObject trash;
    private GameObject burst;
    private GameObject fishy;

    private bool pow = true;
    private bool start = false;
    private bool check = true;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        fishy = Movement.Instance.fishy;
    }

    void Update()
    {
        // start spawning enemies once player clicks
        if (Input.GetMouseButtonDown(0) && check)
        {
            start = true;
            check = false;
        }
        if (start)
        {
            StartCoroutine(DeployUrch());
            StartCoroutine(DeployTrash());
            start = false;
        }
        
        // start displaying light bursts if player is below text block
        if (fishy.transform.position.y < -150 && pow)
        {
            StartCoroutine(DeployLight());
            pow = false;
        }
    }

    // randomly generate urchins in front of player every half second
    IEnumerator DeployUrch()
    {
        while (true)
        {   
            urchin = GameObject.Instantiate(urchinPF);
            urchin.transform.position = new Vector3(Random.Range(-9, 9), fishy.transform.position.y - 7, 0);

            Destroy(urchin, 3);

            yield return new WaitForSeconds(.5f);

        }
    }

    // randomly generate trash in front of player every 3 seconds
     IEnumerator DeployTrash()
    {
        while (true)
        {   
            trash = GameObject.Instantiate(trashPF);
            trash.transform.position = new Vector3(Random.Range(-9, 9), fishy.transform.position.y - 7, 0);
            trash.transform.rotation = Quaternion.Euler(0, 0, 30);

            Destroy(trash, 3);

            yield return new WaitForSeconds(3f);
        }
    }
     
     // randomly generate light in front of player every 4 seconds
     IEnumerator DeployLight()
     {
         while (true)
         {   
             burst = GameObject.Instantiate(lightPF);
             burst.transform.position = new Vector3(Random.Range(-8, 8), fishy.transform.position.y - 7, 0);

             Destroy(burst, 3);

             yield return new WaitForSeconds(4f);
         }
     }
}
