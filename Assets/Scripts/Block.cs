using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    //Config params
    [SerializeField] public AudioClip breakSound;
    [SerializeField] public GameObject blockSparklesVFX;
    [SerializeField] public Sprite[] hitSprites;

    //Cached reference
    Level level;

    //State variables
    [SerializeField] int timesHit; // TODO only serialized for debug purposes

    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

private void HandleHit()
{
    timesHit++;
    int maxHits = hitSprites.Length + 1;
    if (timesHit >= maxHits)
    {
        DestroyBlock();
    }
    else
    {
        ShowNextHitSprite();
    }
}

private void ShowNextHitSprite()
{
    int spriteIndex = timesHit - 1;
    if(hitSprites[spriteIndex] != null)
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }
    else
    {
        Debug.LogError("Block sprite is missing from array" + gameObject.name);
    }
}

    private void DestroyBlock()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        level.BlockDestroyed();
        AudioSource.PlayClipAtPoint(breakSound,Camera.main.transform.position);
        TriggerSparklesVFX();
        Destroy(gameObject);
    }


    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
