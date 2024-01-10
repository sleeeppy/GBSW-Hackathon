using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int enemyScore;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject objects;
    public GameObject mesh;
    public GameObject player;


    GameObject bullet;
    Collider collider;

    void Start()
    {
        collider = gameObject.GetComponent<Collider>();
        objectManager = GetComponent<ObjectManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health--;
            if(health == 0)
            {
                TPSController playerLogic = player.GetComponent<TPSController>();
                playerLogic.score += enemyScore;

                Destroy(collider);
                bullet = other.gameObject;
                StartCoroutine(Desttory2dObject());

                if (enemyName == "D")
                    gameManager.StageEnd();
            }
        }
        else if( other.gameObject.tag == "Border")
        {
            TPSController playerLogic = player.GetComponent<TPSController>();

            gameObject.SetActive(false);
            playerLogic.life--;
            
            gameManager.UpdateLifeIcon(playerLogic.life);

            if (enemyName == "D")
            {
                playerLogic.life = 0;
                gameManager.GameOver();
            }

            if(playerLogic.life <= 0)
            {
                gameManager.GameOver();
            }
        }
    }
    private IEnumerator Desttory2dObject()
    {
        yield return new WaitForSeconds(0.02f);
        Destroy(bullet);
        
        yield return new WaitForSeconds(0.1f);
        objects.SetActive(false);

        mesh.SetActive(true);

        yield return new WaitForSeconds(1f);
        mesh.SetActive(false);
    }

}
