using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Header("Platform Data")]
    public GameObject PlatfromPrefab;

    public Vector3 minPos, maxPos;

    [Header("Object Pooling")]
    public int poolSize = 10;
    private List<GameObject> platformPool = new List<GameObject>();

    [Header("Timer")]
    public float setTime = 2.5f;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(PlatfromPrefab);
            obj.SetActive(false);
            platformPool.Add(obj);
        }

        time = setTime;

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            SpawnPlatforms();
            time = setTime;
        }
    }

    public GameObject GetPlatform()
    {
        for(int i = 0; i < platformPool.Count; i++)
        {
            if (!platformPool[i].activeInHierarchy)
            {
                platformPool[i].SetActive(true);
                return platformPool[i];
            }
        }

        return null;
    }

    public void SpawnPlatforms()
    {
        GameObject platform = GetPlatform();

        if(platform != null)
        {

            Vector3 randomPos = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, minPos.y), Random.Range(minPos.z, maxPos.z));

            //Instantiate(platform, randomPos, Random.rotation);

            platform.transform.position = randomPos;

        }

        StartCoroutine(ReturnPlatformToPool(platform));

    }

    IEnumerator ReturnPlatformToPool(GameObject platform)
    {
        yield return new WaitForSeconds(3);

        if (platform != null)
        {
            platform.SetActive(false);
        }
    }


}
