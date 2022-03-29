using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Visibility : MonoBehaviour
{
    public GameObject InvisbleObject;
    public bool resetAfterClick;

     // Start is called before the first frame update
    void Start()
    {
        
    }

    public void toggleObjectTrue() 
    {

        InvisbleObject.SetActive(true);

        if (resetAfterClick) 
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void toggleObjectFalse()
    {

        InvisbleObject.SetActive(false);

        if (resetAfterClick)
            EventSystem.current.SetSelectedGameObject(null);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
