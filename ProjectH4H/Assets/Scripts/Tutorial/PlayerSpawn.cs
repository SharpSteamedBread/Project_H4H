using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject objPlayer;
    private void Awake()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        objPlayer.transform.position = new Vector3(-41.12f, 3.3f, 0);

        if (SceneManager.GetActiveScene().name == "Map_Export")
        {
            Debug.Log("ÀÎ½ÄÇÏ!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
