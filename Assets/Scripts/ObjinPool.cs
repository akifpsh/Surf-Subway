using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class ObjinPool : MonoBehaviour
{
    [SerializeField] private float spawnInterval;

    [SerializeField] private float timerInterval;

    [SerializeField] private ObjPool objectPool = null;

    private float[] xvalues = { -2.5f, 2.5f, 7.5f };

    private int xvaluesrand;

    private float zdeger = 0;

    private float timer = 0.0f;
    
    Vector3 position;

    public int maxSpawnAttemptsPerObstacle = 100;

    public Material[] material;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerInterval) 
        {
            zdeger += 5;
            timer = 0;
        }
        
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject obj;

            for (int i = 0; i < objectPool.pools.Length; i++) 
            {
                int pooldancek = Random.Range(0, objectPool.pools.Length);
                obj = objectPool.GetPooledObject(pooldancek);

                int randomIndex = Random.Range(0, material.Length);
                obj.GetComponent<Renderer>().material = material[randomIndex];

                for (int j = 0; j < obj.transform.childCount; j++)
                {
                    Transform child = obj.transform.GetChild(j);
                    Renderer childRenderer = child.GetComponent<Renderer>();
                    if (childRenderer.gameObject.tag == "adverb")
                    {
                        continue;
                    }
                    childRenderer.material = material[randomIndex];
                }

                    xvaluesrand = Random.Range(0, xvalues.Length);

                position = new Vector3(xvalues[xvaluesrand], 0, Random.Range(0, 20) + zdeger);

                if (Physics.OverlapSphere(position, 1f).Length > 1)
                {
                    int x=0;
                    while (x < 10)
                    {
                        x++;
                        position=new Vector3(xvalues[xvaluesrand], 0, Random.Range(0, 20) + zdeger);
                        if (x == 10)
                        {
                            position = new Vector3(xvalues[xvaluesrand], Random.Range(20, 30), Random.Range(0, 20) + zdeger);
                            break;
                        }
                    }
                   
                }                    
                
                else
                {
                    if (pooldancek == 0) 
                        obj.transform.position = new Vector3(xvalues[xvaluesrand], 0, Random.Range(0, 20) + zdeger);

                    else if (pooldancek == 1)
                        obj.transform.position = new Vector3(xvalues[xvaluesrand] - 1.3f, 0, Random.Range(0, 20) + zdeger);

                    else if (pooldancek == 2) 
                        obj.transform.position = new Vector3(xvalues[xvaluesrand] - 1.3f, 0, Random.Range(0, 20) + zdeger );
                    
                    else if (pooldancek == 3 || pooldancek == 4)
                        obj.transform.position = new Vector3(xvalues[xvaluesrand] - 2, 0, Random.Range(0, 20) + zdeger );
                    
                    else if (pooldancek == 5 || pooldancek == 6)
                        obj.transform.position = new Vector3(xvalues[xvaluesrand] + 2, 0, Random.Range(0, 20) + zdeger );

                    else if(pooldancek == 7)
                        obj.transform.position = new Vector3(xvalues[xvaluesrand], 1, Random.Range(0, 20) + zdeger);

                }                

                yield return new WaitForSeconds(spawnInterval);

            }
        }
                
    }


}
