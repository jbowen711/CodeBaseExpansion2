using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] prefabPool; // Prefab havuzunuzu burada belirtin
    public float spawnAreaWidth = 10f; // Spawn alanının genişliği
    public float spawnAreaHeight = 10f; // Spawn alanının yüksekliği
    public int spawnCount = 5; // Spawn edilecek prefab sayısı

    public float bufferRadius = 1f; // Her objenin etrafındaki "buffer zone" yarıçapı
    public string avoidTag = "Avoid"; // Kaçınılacak objenin tag'ı
    public float avoidRadius = 5f; // Kaçınılacak objenin etrafındaki yarıçap

    public bool coolDown = false;
    private float spawnCooldown = 5f;

    private List<Vector2> spawnedPositions = new List<Vector2>(); // Spawn edilen objelerin konumlarını saklayan liste

    void Start()
    {
        if (coolDown == false)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnPrefab();
                
            }
        }
        if (coolDown == true) 
        {
            StartCoroutine(SpawnPrefabWithCooldown());
        }
    }


    void Update () { 

        
    }
    IEnumerator SpawnPrefabWithCooldown()
    {

        while (true)
        {
            Vector2 spawnPos;

            do
            {
                // Rastgele bir konum belirle
                float spawnPosX = Random.Range(transform.position.x - spawnAreaWidth / 2, transform.position.x + spawnAreaWidth / 2);
                float spawnPosY = Random.Range(transform.position.y - spawnAreaHeight / 2, transform.position.y + spawnAreaHeight / 2);
                spawnPos = new Vector2(spawnPosX, spawnPosY);
            }
            while (IsInBufferZone(spawnPos) || IsInAvoidZone(spawnPos)); // Spawn olacak konumun belirlenen bölge dışında olduğunu kontrol et

            // Rastgele bir prefab seç
            GameObject prefab = prefabPool[Random.Range(0, prefabPool.Length)];

            // Prefab objesini belirlenen konumda oluştur
            Instantiate(prefab, spawnPos, Quaternion.identity);

            // Spawn edilen objenin konumunu listeye ekle
            spawnedPositions.Add(spawnPos);
            yield return new WaitForSeconds(spawnCooldown);
        }    
        
    }
    void SpawnPrefab()
    {
        Vector2 spawnPos;

        do
        {
            // Rastgele bir konum belirle
            float spawnPosX = Random.Range(transform.position.x - spawnAreaWidth / 2, transform.position.x + spawnAreaWidth / 2);
            float spawnPosY = Random.Range(transform.position.y - spawnAreaHeight / 2, transform.position.y + spawnAreaHeight / 2);
            spawnPos = new Vector2(spawnPosX, spawnPosY);
        }
        while (IsInBufferZone(spawnPos) || IsInAvoidZone(spawnPos)); // Spawn olacak konumun belirlenen bölge dışında olduğunu kontrol et

        // Rastgele bir prefab seç
        GameObject prefab = prefabPool[Random.Range(0, prefabPool.Length)];

        // Prefab objesini belirlenen konumda oluştur
        Instantiate(prefab, spawnPos, Quaternion.identity);

        // Spawn edilen objenin konumunu listeye ekle
        spawnedPositions.Add(spawnPos);
    }

    bool IsInBufferZone(Vector2 position)
    {
        foreach (Vector2 spawnedPos in spawnedPositions)
        {
            // Eğer belirlenen konum, spawn edilen bir objenin "buffer zone" içindeyse, true döndür
            if (Vector2.Distance(spawnedPos, position) <= bufferRadius)
            {
                return true;
            }
        }

        // Belirlenen konum, hiçbir objenin "buffer zone" içinde değilse, false döndür
        return false;
    }

    bool IsInAvoidZone(Vector2 position)
    {
        // Belirtilen tag'a sahip tüm objeleri bul
        GameObject[] objectsToAvoid = GameObject.FindGameObjectsWithTag(avoidTag);

        foreach (GameObject obj in objectsToAvoid)
        {
            // Eğer obje belirtilen yarıçapa sahip bir çemberin içindeyse, true döndür
            if (Vector2.Distance(obj.transform.position, position) <= avoidRadius)
            {
                return true;
            }
        }

        // Hiçbir obje belirtilen yarıçapa sahip bir çemberin içinde değilse, false döndür
        return false;
    }
}
