using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteSound : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    public Sprite sound_on_sprite;
    public Sprite sound_off_sprite;

    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Data.SoundEnabled==true)
        {
            sprRenderer.sprite = sound_on_sprite;
        }
        if (Data.SoundEnabled==false)
        {
            sprRenderer.sprite = sound_off_sprite;
        }
    }
    private void OnMouseUp()
    {
        Data.SoundEnabled= !Data.SoundEnabled;
    }
}
