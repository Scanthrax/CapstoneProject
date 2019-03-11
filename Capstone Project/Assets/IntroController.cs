using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public RectTransform canvas;
    public RectTransform characterUIScreen;

    public int amountOfCharacters;


    public RectTransform greetScreen;
    public RectTransform[] listOfScreens;

    bool goToNextAction = false;

    int action = 0;


    float timer, time;


    public float duration;

    public AnimationCurve sweepCurve;


    public RectTransform container;

    private void Start()
    {
        amountOfCharacters = MenuSelection.numPlayers;

        listOfScreens = new RectTransform[amountOfCharacters + 1];


        listOfScreens[0] = greetScreen;

        for (int i = 0; i < amountOfCharacters; i++)
        {
            listOfScreens[i + 1] = Instantiate(characterUIScreen, canvas.localPosition, Quaternion.identity, container);
            listOfScreens[i + 1].gameObject.SetActive(false);
        }


        time = 0f;
        timer = 3f;

        if(MenuSelection.instance)
        {
            MenuSelection.instance.FadeIn(1f);
        }
    }



    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timer)
        {
            goToNextAction = true;
            time = 0f;
        }

        if(goToNextAction)
        {
            goToNextAction = false;

            action++;

            if (action < listOfScreens.Length)
                GoToNextMenu();
            else if (action == listOfScreens.Length)
            {
                MenuSelection.instance.FadeOut(1f);
                MenuSelection.goToScene = MenuSelection.goToMinigameScene;
            }

        }

    }


















    public void GoToNextMenu()
    {
        MoveMenus(listOfScreens[action - 1], true);
        MoveMenus(listOfScreens[action], false);
    }


    /// <summary>
    /// Transition between 2 menus
    /// </summary>
    /// <param name="obj">The menu to move</param>
    /// <param name="setInactive">Do we set the menu as inactive after transitioning?</param>
    void MoveMenus(RectTransform menu, bool setInactive)
    {
        StartCoroutine(AnimateMove(menu, setInactive));
    }

    IEnumerator AnimateMove(RectTransform menu, bool setInactive)
    {

        Vector2 target;
        Vector2 origin;

        Vector2 offset = new Vector2(750, 0);

        // If false, this means that the menu is currently disabled.  We need to enable it & set it off screen so it can swipe in
        if (!setInactive)
        {
            menu.gameObject.SetActive(true);
            origin = offset;
            target = Vector2.zero;
        }
        // otherwise, we need to tell the current menu to go off the screen
        else
        {
            origin = Vector2.zero;
            target = -offset;
        }

        // timer for moving the menu
        float journey = 0f;
        // percentage of completion, used for finding position on animation curve
        float percent = 0f;

        // keep adjusting the position while there is time
        while (journey <= duration)
        {
            // add to timer
            journey = journey + Time.deltaTime;
            // calculate percentage
            percent = Mathf.Clamp01(journey / duration);
            // find the percentage on the curve
            float curvePercent = sweepCurve.Evaluate(percent);
            // adjust the position of the menu
            menu.transform.localPosition = Vector2.LerpUnclamped(origin, target, curvePercent);
            // wait a frame
            yield return null;
        }


        // the loop is now over, so if the menu is going out, we disable it
        if (setInactive)
            menu.gameObject.SetActive(false);


    }


}
