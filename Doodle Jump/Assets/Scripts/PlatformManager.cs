using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public float minX = -5.5f;
    public float maxX = 5.5f;
    public float maxY = 14;
    public float offsetMinY = 0.5f;
    public float offsetMaxY = 1f;
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject springPrefab;
    private GameObject newPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DestroyAndCreatePlatform(collision);
        MovePlatformUp(collision);
    }

    private void MovePlatformUp(Collider2D collision)
    {
        Vector2 newPlatformPos = new Vector2(Random.Range(minX, maxX), player.transform.position.y + maxY + Random.Range(offsetMinY, offsetMaxY));
        if (collision.gameObject.name.StartsWith("Platform"))
        {
            if (Random.Range(1, 7) == 1)
            {
                // destroy platform and create spring platform
                Destroy(collision.gameObject);
                newPlatform = (GameObject)Instantiate(springPrefab, newPlatformPos, Quaternion.identity);
            }
            else
            {
                // move platform upwards
                collision.gameObject.transform.position = newPlatformPos;
            }
        }
        else if (collision.gameObject.name.StartsWith("Spring"))
        {
            if (Random.Range(1, 7) == 1)
            {
                // move spring upwards
                collision.gameObject.transform.position = newPlatformPos;
            }
            else
            {
                // destroy spring and create platform
                Destroy(collision.gameObject);
                newPlatform = (GameObject)Instantiate(platformPrefab, newPlatformPos, Quaternion.identity);
            }
        }
    }

    // Instantiating and Destroying GameObject is not very efficient. Can take up alot of memory
    private void DestroyAndCreatePlatform(Collider2D collision)
    {
        Destroy(collision.gameObject);
        Vector2 newPlatformPos = new Vector2(Random.Range(minX, maxX), player.transform.position.y + maxY + Random.Range(offsetMinY, offsetMaxY));
        if (Random.Range(1, 7) > 1)
        {
            newPlatform = (GameObject)Instantiate(platformPrefab, newPlatformPos, Quaternion.identity);
        }
        else
        {
            newPlatform = (GameObject)Instantiate(springPrefab, newPlatformPos, Quaternion.identity);
        }
    }
}
