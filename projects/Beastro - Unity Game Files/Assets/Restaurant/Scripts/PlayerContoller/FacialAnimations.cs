using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialAnimations : MonoBehaviour
{
    public GameObject[] face;
    /* Face 0: Normal
     * Face 1: Annoyed
     * Face 2: Normal 2
     * Face 3: Eyes Closed
     * Face 4: Happy
     * Face 5: Angry
     * Face 6: Surprised
     * Face 7: Upset
     * Face 8: Hurt
     */

    [SerializeField] float timer = 35;
    public float blinkTime;
    public float resetTimer;

    PauseGameManager paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= blinkTime)
        {
            face[0].SetActive(false);
            face[3].SetActive(true);
            if (face[3].activeSelf && timer <= 0)
            {
                face[0].SetActive(true);
                face[3].SetActive(false);
                timer = resetTimer;
            }
        }

    }
}
