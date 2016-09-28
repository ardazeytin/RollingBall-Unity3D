using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TileManager.Instance.SpawnTile();
            StartCoroutine(FallDown());
        }
    }


    //Recycle Tiles
    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(0.6f);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.6f);

        switch (gameObject.name)
        {
            case "LeftTile": //gameobject.name
                TileManager.Instance.LeftTiles.Push(gameObject); //Push leftTile to stack
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;

            case "ForwardTile": //gameobject.name
                TileManager.Instance.ForwardTiles.Push(gameObject); //Push forwardTile to stack
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
        }
    }
}
