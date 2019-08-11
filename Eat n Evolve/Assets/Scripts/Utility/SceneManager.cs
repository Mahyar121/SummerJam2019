using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    // Stores the quadrants for random generation
    [SerializeField] private List<Transform> quadrants;
    [SerializeField] private List<Transform> playerSpawns;
    [SerializeField] private List<GameObject> zones;

    // Creates a singleton of the SceneManager since we only need one unique instance
    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<SceneManager>(); }
            return instance;
        }
    }

    public Transform PlayerSpawnLocation { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        randomizePlayerSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        // if player is dead then randomize spawn of player
    }

    private void randomizePlayerSpawn()
    {
        int randomSpawn = Random.Range(0, playerSpawns.Capacity);
        PlayerSpawnLocation = playerSpawns[randomSpawn];
    }


}
