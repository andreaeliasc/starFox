using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;      
    public float tiempo = 0.0f;
    public GameObject DayBackg;
    public GameObject SunsetBackg;
    public GameObject NightBackg;

    private Vector3 offset;       
    void Start()
    {
        
        offset = transform.position - player.transform.position;
        DayBackg.SetActive(true);
        SunsetBackg.SetActive(false);
        NightBackg.SetActive(false);
    }

  
    void LateUpdate()
    {
        
        transform.position = player.transform.position + offset;

        tiempo = tiempo + Time.deltaTime;
        if (tiempo >= 60.0f)
        {
            DayBackg.SetActive(false);
            SunsetBackg.SetActive(false);
            NightBackg.SetActive(true);
        }
        else if (tiempo >= 30.0f)
        {
            DayBackg.SetActive(false);
            SunsetBackg.SetActive(true);
            NightBackg.SetActive(false);
        }
    }

}