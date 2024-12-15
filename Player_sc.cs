using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Mono.Reflection;
using TreeEditor;
using UnityEngine;

public class Player_sc : MonoBehaviour
{
    public float speed = 5; //degisken public tanimlanirsa Inspector penceresinde gorunur
    float speedMultiplier =2;

    public GameObject laserPrefab;
    public GameObject shieldVisualizer;

    [SerializeField]
    GameObject rightEngine,leftEngine;

    float fireRate = 0.5f;  //atislar arasındaki sure 
    float nextFire = 0f; //atis yapmak icin beklenmesi gereken süre(en az)

    [SerializeField]
    private int _lives = 3;
    
    [SerializeField]
    bool isTripleShotActive =false;
    bool isSpeedBonusActive = false;
    bool isShieldBonusActive = false;

    [SerializeField]
    GameObject tripleShotPrefab;

    SpawnManager_sc spawnManager_sc;
    UIManager_sc uiManager_sc;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(-2, 0, 0);

        //spawnManager_sc değişkeni, "Spawn_Manager" oyun nesnesi üzerindeki "SpawnManager_sc" bileşenine bir referans içerir.
        spawnManager_sc = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager_sc>();
        if(spawnManager_sc == null)
        {
            Debug.LogError("Spawn_Manager bulunamadı");
        }

        uiManager_sc = GameObject.Find("Canvas").GetComponent<UIManager_sc>();
        if(uiManager_sc == null)
        {
            Debug.LogError("UIManager bulunamadı");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        FireLaser();

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        //Math.clamp, y koordinatının bir sınır icinde tutulmasını saglar
        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, -3.8f,0), 0);

        if (transform.position.x >= 9.2f){
            transform.position = new Vector3(-9.2f, transform.position.y,0);
        }
        else if (transform.position.x <= -9.2f){
            transform.position = new Vector3(9.2f, transform.position.y,0);
        }
    }

    void FireLaser(){
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire){
            if(!isTripleShotActive)
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
            }
            else
            {
                Instantiate(tripleShotPrefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
            }
            nextFire = Time.time + fireRate;
        }
    }

    public void Damage()
    {
        if(isShieldBonusActive)
        {
            isShieldBonusActive = false;
            shieldVisualizer.SetActive(false);
            return;  //kodun devamı çalışmasın (lives'ın azaldığı satıra gelmesin)
        }
        _lives--;

        if(_lives == 2)
        {
            leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            rightEngine.SetActive(true);
        }
        
        if(uiManager_sc != null)
        {
            uiManager_sc.UpdateLivesImg(_lives);
        }
        
        if(_lives < 1)
        {
            if(spawnManager_sc != null){
                //"OnPlayerDeath()" static bir fonksiyon olmadığı için sınıf adı ile erişilemez
                //bu yüzden scripte bir referans oluşturduk "spawnManager_sc"
                spawnManager_sc.OnPlayerDeath();
            }
            
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        isTripleShotActive = true;

        StartCoroutine(TripleShotBonusDisableRoutine());

    }

    public void ActivateSpeedBonus(){
        isSpeedBonusActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBonusDisableRoutine());
      
    }
    public void ActivateShieldBonus()
    {
        isShieldBonusActive = true;
        shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldBonusDisableRoutine());
    }

    IEnumerator TripleShotBonusDisableRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            isTripleShotActive = false;
        }

    IEnumerator SpeedBonusDisableRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            speed /= speedMultiplier;
            isSpeedBonusActive = false;
        }
    IEnumerator ShieldBonusDisableRoutine()
        {
            yield return new WaitForSeconds(10.0f);
            shieldVisualizer.SetActive(false);
            isShieldBonusActive = false;
        }

    public void UpdateScore(int points)
    {
        score += points;
        if(uiManager_sc != null)
        {
            uiManager_sc.UpdateScoreTMP(score);
        }
    }
}
