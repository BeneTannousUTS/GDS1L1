using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject soldierPrefab;

    private int TreesToSpawn;
    private int SoldiersToSpawn;
    
    private float minX = -5f, maxX = 8f;
    private float minY = -4f, maxY = 4f;
    private float minDistance = 1.5f;
    private int maxAttempts = 100;
    
    private GameObject[] spawnedObjects;
    
    private CounterManagerScript _counterManagerScript;
    private UpdateUIContentScript _uiManagerScript;

    void Start()
    {
        _counterManagerScript = GameObject.Find("CounterManager").GetComponent<CounterManagerScript>();
        _uiManagerScript = GameObject.Find("UIManager").GetComponent<UpdateUIContentScript>();
        SpawnAll();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAndRespawn();
        }
    }
    
    void SpawnAll()
    {
        TreesToSpawn = Random.Range(2, 6);
        SoldiersToSpawn = Random.Range(5, 9);

        spawnedObjects = new GameObject[TreesToSpawn + SoldiersToSpawn];

        int index = 0;
        index = SpawnObjects(treePrefab, TreesToSpawn, index);
        SpawnObjects(soldierPrefab, SoldiersToSpawn, index);
        _counterManagerScript.SetSoldiersToSave(SoldiersToSpawn);
    }

    int SpawnObjects(GameObject prefab, int count, int startIndex)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject;
            int attempts = maxAttempts;
        
            do
            {
                Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                newObject = Instantiate(prefab, position, Quaternion.identity);
            
                // Wait for physics update before checking collision
                Physics2D.SyncTransforms();

            } while (IsOverlapping(newObject) && --attempts > 0);

            spawnedObjects[startIndex + i] = newObject;
        }
        return startIndex + count;
    }
    
    bool IsOverlapping(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();

        if (!collider)
        {
            Debug.LogWarning($"No Collider found on {obj.name}");
            return true; // Assume it's overlapping to force a reposition
        }

        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        int numCollisions = collider.Overlap(filter, results);

        return numCollisions > 1; 
    }
    
    void ResetAndRespawn()
    {
        // Destroy all existing objects
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        
        // Set Game Over/win screen inactive
        _uiManagerScript.SetGameOverActiveState(false);
        _uiManagerScript.SetWinScreenActiveState(false);
        
        // Respawn everything
        SpawnAll();
        
        GameObject.FindWithTag("Player").transform.position = new Vector2(-7, 0);
        GameObject.FindWithTag("Player").GetComponent<MovementScript>().setInputsDisabled(false);
        _counterManagerScript.ResetCounters();
    }
    
    
}