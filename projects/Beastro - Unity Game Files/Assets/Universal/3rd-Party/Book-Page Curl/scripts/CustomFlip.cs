using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomFlip : MonoBehaviour
{
    AutoFlip pageTurn;
    Book pages;
    public int wantedPage;
    bool turnBook;
    bool enableContent = false;
    bool turnLeft = false;
    bool turnRight = false;

    public GameObject[] rightTabs;
    /*  0: CustomerButton
     *  1: Recipebutton
     *  2: InventoryButton
     *  3: ShopButton
     */ 
    public GameObject[] leftTabs;
    /*  0: CustomerButton
     *  1: Recipebutton
     *  2: InventoryButton
     *  3: ShopButton
     */
    public GameObject[] pageContents;
    /*  0: RestaurantRecordsContent
     *  1: CustomerContent
     *  2: RecipeContent
     *  3: InventoryContent
     *  4: ShopContent
     *  5: SettingsContent
     */

    // Start is called before the first frame update
    void Start()
    {
        pages = GameObject.Find("Book").GetComponent<Book>();
        pageTurn = GameObject.Find("Book").GetComponent<AutoFlip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnBook)
        {
            if (pages.currentPage > wantedPage)
            {
                turnLeft = true;
                pageTurn.FlipLeftPage();
            }

            if (pages.currentPage < wantedPage)
            {
                turnRight = true;
                pageTurn.FlipRightPage();
            }

            if (pages.currentPage == wantedPage)
            {
                turnBook = false;
                enableContent = true;
                SwapTabsAfterFlip();
            }

            if (enableContent && !turnBook)
            {
                switch (pages.currentPage)
                {
                    case 0:
                        pageContents[0].SetActive(true);
                        break;

                    case 2:
                        pageContents[1].SetActive(true);
                        break;

                    case 4:
                        pageContents[2].SetActive(true);
                        break;

                    case 6:
                        pageContents[3].SetActive(true);
                        break;

                    case 8:
                        pageContents[4].SetActive(true);
                        break;

                    case 10:
                        pageContents[5].SetActive(true);
                        break;
                }
            }
        }
    }

    public void FlipToPage0()
    {
        DisableContent();
        wantedPage = 0;
        turnBook = true;
        SwapTabsBeforeFlip();
    }
    public void FlipToPage2()
    {
        DisableContent();
        wantedPage = 2;
        turnBook = true;
        SwapTabsBeforeFlip();
    }
    public void FlipToPage4()
    {
        DisableContent();
        wantedPage = 4;
        turnBook = true;
        SwapTabsBeforeFlip();
    }
    public void FlipToPage6()
    {
        DisableContent();
        wantedPage = 6;
        turnBook = true;
        SwapTabsBeforeFlip();
    }
    public void FlipToPage8()
    {
        DisableContent();
        wantedPage = 8;
        turnBook = true;
        SwapTabsBeforeFlip();
    }

    public void FlipToPage10()
    {
        DisableContent();
        wantedPage = 10;
        turnBook = true;
        SwapTabsBeforeFlip();
    }

    void DisableContent()
    {
        // Disable all page contents
        for (int i = 0; i < 6; ++i)
        {
            pageContents[i].SetActive(false);
        }
    }

    void SwapTabsBeforeFlip()
    {
        int i = wantedPage / 2;

        if (pages.currentPage < wantedPage)
        {
            for (int j = 0; j < i; ++j)
            {
                rightTabs[j].SetActive(false);
            }

            turnRight = true;
        }
        
        else
        {
            for (int j = i; j < 6; ++j)
            {
                leftTabs[j].SetActive(false);
            }

            turnLeft = true;
        }
    }

    void SwapTabsAfterFlip()
    {
        int i = wantedPage / 2;

        if (turnRight)
        {
            for (int j = 0; j < i; ++j)
            {
                leftTabs[j].SetActive(true);
            }
            turnRight = false;
        }
        if (turnLeft)
        {
            for (int j = i; j < 6; ++j)
            {
                rightTabs[j].SetActive(true);
            }
            turnLeft = false;
        }
    }
}

