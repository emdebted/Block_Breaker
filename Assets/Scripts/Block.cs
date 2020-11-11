using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    //Config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

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
    GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
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
