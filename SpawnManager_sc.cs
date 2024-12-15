using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpawnManager_sc : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    GameObject[] BonusPrefabs;

    public GameObject enemyContainer;
    bool stopSpawning = false;

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-9.4f, 9.4f), 7.4f,0);  //rastgele bir pozisyon değeri alıyor
            GameObject new_enemy = Instantiate(enemyPrefab, position, Quaternion.identity);  //pozisyon bilgisini kullanarak enemyPrefab'den new_enemy nesnesi üretiliyor
            new_enemy.transform.parent = enemyContainer.transform; //new_enemy nesnesi, enemyContainer'ın altına atanır

            yield return new WaitForSeconds(5.0f); //Coroutine 5 saniye boyunca duraklatılır ve sonra kaldığı yerden devam eder.
        }
    }

    IEnumerator SpawnBonusRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-9.4f, 9.4f), 7.4f,0);  
            int randomBonus = Random.Range(0,3);
            Instantiate(BonusPrefabs[randomBonus], position, Quaternion.identity);  
            yield return new WaitForSeconds(3.0f); 

        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine()); //Couroutine'leri çağırmak için bir fonksiyon
        StartCoroutine(SpawnBonusRoutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private GameObject GameObject(string v)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


