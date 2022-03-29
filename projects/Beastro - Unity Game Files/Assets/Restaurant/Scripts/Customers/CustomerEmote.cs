using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEmote : MonoBehaviour
{
    SpriteRenderer rend;
    public void Emote(Sprite emotion){
        if (rend == null)
            rend = GetComponent<SpriteRenderer>();
        rend.sprite = emotion;
        StartCoroutine(EmoteWait());
    }
    IEnumerator EmoteWait(){
        yield return new WaitForSeconds(4f);
        rend.sprite = null;
    }
}
