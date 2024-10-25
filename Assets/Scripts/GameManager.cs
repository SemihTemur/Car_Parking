using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("----Cars Settings----")]
    public List<GameObject> cars = new List<GameObject>();
    public int cars›ndex = 0;
    public int maxCurrentCar = 3;
    int remainingCars;
    public bool isStart=false;

    [Header("----Canvas Settings----")]
    public List<Image> carsImage = new List<Image>();
    public Sprite carsParked;
    public TextMeshProUGUI[] texts;
    public GameObject[] panels;
    public GameObject[] tapToButtons;

    [Header("----Platform Settings----")]
    public GameObject platform_1;
    public GameObject platform_2;
    public float[] rotationSpeeds;
    public bool isPlatformStop=false;

    [Header("----Level Settings----")]
    public int diamondsCount;
    public AudioSource[] audioSources;


    void Start()
    {
        remainingCars = maxCurrentCar;
        texts[0].text = PlayerPrefs.GetInt("Level").ToString();
        texts[1].text = PlayerPrefs.GetInt("Diamond").ToString();;

        for (int i = 0; i < maxCurrentCar; i++)
        {
            carsImage[i].gameObject.SetActive(true);
        }
    }

    
    public void CarActivate()
    {
        if (maxCurrentCar > cars›ndex)
        {
            cars[cars›ndex].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + diamondsCount);
            WinState();
        }
        carsImage[cars›ndex - 1].sprite = carsParked;
        remainingCars--;

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)&&isStart)
        {
            cars[cars›ndex].GetComponent<CarController>().progress = true;
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            panels[0].SetActive(false);
            panels[3].SetActive(true);
            isStart = true;
        }
        if(!isPlatformStop)
        platform_1.transform.Rotate(new Vector3(0, 0, rotationSpeeds[0]), Space.Self);
    }


    public void LoseState()
    {
        audioSources[1].Play();
        isStart = false;
        isPlatformStop = true;
        panels[0].SetActive(false);
        panels[3].SetActive(false);
        panels[2].SetActive(true);
        texts[6].text = "Level : " + PlayerPrefs.GetInt("Level").ToString();
        texts[7].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[8].text = (maxCurrentCar-remainingCars).ToString();
        texts[9].text = diamondsCount.ToString();
        StartCoroutine(ActivateButtonWithDelay(1));
    }

    void WinState()
    {
        audioSources[0].Play();
        isStart = false;
        panels[0].SetActive(false);
        panels[3].SetActive(false);
        panels[1].SetActive(true);
        texts[2].text ="Level : "+PlayerPrefs.GetInt("Level").ToString();
        texts[3].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[4].text = (cars›ndex).ToString();
        texts[5].text = diamondsCount.ToString();
        StartCoroutine(ActivateButtonWithDelay(0));
    }

    public void ButtonsSettings(int index)
    {
        if (index == 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
    }

    IEnumerator ActivateButtonWithDelay(int index)
    {
        yield return new WaitForSeconds(2f);
        tapToButtons[index].SetActive(true);
    }


}
