using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour {

    public GameObject currentTile;
    public GameObject[] tilePrefabs;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    private Stack<GameObject> forwardTiles = new Stack<GameObject>();

    private static TileManager instance;

    public static TileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TileManager>();
            }
            return instance;
        }

        //No need "set" for now
        //set{instance = value;}
    }

    public Stack<GameObject> LeftTiles
    {
        get
        {
            return leftTiles;
        }

        set
        {
            leftTiles = value;
        }
    }

    public Stack<GameObject> ForwardTiles
    {
        get
        {
            return forwardTiles;
        }

        set
        {
            forwardTiles = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        CreateTiles(75); 

        for (int i = 0; i < 50; i++)
        {
            SpawnTile();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void CreateTiles(int amounth)
    {
        for (int i = 0; i < amounth; i++)
        {
            LeftTiles.Push(Instantiate(tilePrefabs[0]));    //Create tile
            ForwardTiles.Push(Instantiate(tilePrefabs[1]));
            ForwardTiles.Peek().name = "ForwardTile"; // Change tile name
            ForwardTiles.Peek().SetActive(false);
            LeftTiles.Peek().name = "LeftTile";
            LeftTiles.Peek().SetActive(false); 
        }
    }

    public void SpawnTile()
    {
        //if stack is empty add more tiles
        if (LeftTiles.Count == 0 || ForwardTiles.Count == 0)
        {
            CreateTiles(10);
        }

        //Recycle tiles
        int randomNumber = Random.Range(0, 2); 

        if (randomNumber == 0)
        {
            GameObject tmp = LeftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomNumber).position;
            currentTile = tmp;
        }
        else if (randomNumber == 1)
        {
            GameObject tmp = ForwardTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomNumber).position;
            currentTile = tmp;
        }

        //Refactoring
        //currentTile = (GameObject)Instantiate(tilePrefabs[randomNumber], currentTile.transform.GetChild(0).transform.GetChild(randomNumber).position, Quaternion.identity);

        //Spawn  objects randomly with 10% chance
        int randomSpawnNumber = Random.Range(0, 10);

        //Spawn PickUp
        if (randomSpawnNumber == 0)
        {
            currentTile.transform.GetChild(1).gameObject.SetActive(true);
        }

        //Spawn tree
        if (randomSpawnNumber == 1)
        {
            currentTile.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        int activeLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeLevel);
    }
}
