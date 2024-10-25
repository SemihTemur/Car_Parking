using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool progress = false;
    private float carSpeed = 15f;

    public GameObject[] wheel_Line;
    public Transform parent;
    private GameManager gameManager;

    private bool carAnimation = false;

    public ParticleSystem _CrashingParticleEffect;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      
    }


    void Update()
    {
        if (!carAnimation)
            transform.Translate(Vector3.forward * 6f * Time.deltaTime);

        if (progress)
            transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Parking"))
        {
            CarTechicalOperation();
            transform.SetParent(parent);
            gameManager.cars›ndex++;
            gameManager.CarActivate();
        }
        else if (collision.gameObject.CompareTag("Car"))
        {
            CarTechicalOperation();
            _CrashingParticleEffect.Play();
            gameManager.audioSources[2].Play();
            StartCoroutine(ShowLoseState());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car_Animation"))
        {
            carAnimation = true;
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            gameManager.diamondsCount++;
            gameManager.audioSources[3].Play();
        }
        else if (other.gameObject.CompareTag("Middle"))
        {
            CarTechicalOperation();
            _CrashingParticleEffect.Play();
            gameManager.audioSources[2].Play();
            StartCoroutine(ShowLoseState());
            
        }
        else if (other.gameObject.CompareTag("Front_Parking"))
        {
            other.GetComponent<Front_Parking>()._Parking.SetActive(true);
        }
    }

    // Close the wheel_Line and stop the car movement
    public void CarTechicalOperation()
    {
        foreach (var item in wheel_Line)
        {
            item.gameObject.SetActive(false);
        }
        progress = false;
    }

    IEnumerator ShowLoseState()
    {
        yield return new WaitForSeconds(1f);
        gameManager.LoseState();
    }

}
