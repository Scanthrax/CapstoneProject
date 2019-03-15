using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{
    public static MenuSelection instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }



    /// <summary>
    /// Directions for the menu transitions
    /// </summary>
    public enum Direction { Left, Right, Up, Down }


    /// <summary>
    /// Directions for the menu transitions
    /// </summary>
    public enum Menu { Welcome, GameSelect, ChooseLanguage, ChooseDifficulty, TopicSelect, None, Login, CharacterSelect}

    /// <summary>
    /// This dictionary keeps track of the different menus.  Feeding in a menu name will obtain the menu
    /// </summary>
    public Dictionary<string, RectTransform> menuDictionary = new Dictionary<string, RectTransform>();

    /// <summary>
    /// This curve controls the sweeping motion between menu transitions
    /// </summary>
    public AnimationCurve menuSweepCurve;

    /// <summary>
    /// The canvas contains all the menus
    /// </summary>
    RectTransform Canvas;

    /// <summary>
    /// The duration of the menu transitions
    /// </summary>
    public float duration = 0.4f;


    public string currentMenu;

    public List<ButtonMapping> buttonMappings = new List<ButtonMapping>();

    public AudioSource buttonTapAudioSource;

    [System.Serializable]
    public struct ButtonMapping
    {
        public Menu menu;
        public RectTransform transform;
        public Button[] button;
    }

    [System.Serializable]
    public struct MenuMapping
    {
        public Menu menu;
        public RectTransform transform;
    }


    public Image fadeScreen;


    public static string goToScene, goToMinigameScene;

    public static string introScene, menuScene;

    public static int numPlayers;



    public TestObject test;

    void Start()
    {
        if (test.localizationFile != "")
        {
            currentMenu = test.menu.ToString();
            LocalizationManager.instance.LoadLocalizedText(test.localizationFile);
        }
        else
        {
            currentMenu = Menu.ChooseLanguage.ToString();
        }


        foreach (var item in buttonMappings)
        {
            menuDictionary.Add(item.menu.ToString(), item.transform);
        }

        foreach (var mapping in buttonMappings)
        {
            for (int i = 0; i < mapping.button.Length; i++)
            {
                mapping.button[i].onClick.AddListener(delegate { GoToNextMenu(mapping.menu.ToString()); });
            }
        }

        // disable all menus
        foreach (KeyValuePair<string, RectTransform> entry in menuDictionary)
        {
            entry.Value.gameObject.SetActive(false);
            entry.Value.localPosition = new Vector2(0, 1334);
        }

        // declare which menu we will be starting at
        

        RectTransform startMenu = null;

        if (menuDictionary.ContainsKey(currentMenu))
            startMenu = menuDictionary[currentMenu];

        // put menu at center of screen
        if (startMenu)
        {
            startMenu.gameObject.SetActive(true);
            startMenu.localPosition = new Vector2(0, 0);
        }




        foreach (var item in GameObject.FindObjectsOfType<Button>())
        {
            item.onClick.AddListener(delegate { TapButton(); });
        }


        introScene = "Introduction";
        menuScene = "Menu";

        goToScene = introScene;
        goToMinigameScene = menuScene;



    }





    public void GoToNextMenu(string menu)
    {
        MoveMenus(currentMenu, true);
        MoveMenus(menu, false);
    }


    /// <summary>
    /// Transition between 2 menus
    /// </summary>
    /// <param name="obj">The menu to move</param>
    /// <param name="setInactive">Do we set the menu as inactive after transitioning?</param>
    void MoveMenus(string menu, bool setInactive)
    {
        StartCoroutine(AnimateMove(menu, setInactive));
    }

    IEnumerator AnimateMove(string menu, bool setInactive)
    {
        RectTransform obj = menuDictionary[menu];

        Vector2 target;
        Vector2 origin;

        Vector2 offset;

        Direction dir = Direction.Down;
        switch (dir)
        {
            case Direction.Right:
                offset = new Vector2(750, 0);
                break;
            case Direction.Left:
                offset = new Vector2(-750, 0);
                break;
            case Direction.Up:
                offset = new Vector2(0, 1334);
                break;
            case Direction.Down:
                offset = new Vector2(0, -1334);
                break;
            default:
                offset = new Vector2(750, 0);
                break;
        }
        // If false, this means that the menu is currently disabled.  We need to enable it & set it off screen so it can swipe in
        if (!setInactive)
        {
            obj.gameObject.SetActive(true);
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
            float curvePercent = menuSweepCurve.Evaluate(percent);
            // adjust the position of the menu
            obj.transform.localPosition = Vector2.LerpUnclamped(origin, target, curvePercent);
            // wait a frame
            yield return null;
        }

        currentMenu = menu;

        // the loop is now over, so if the menu is going out, we disable it
        if (setInactive)
            obj.gameObject.SetActive(false);


    }




    public void FadeOut(float duration)
    {
        StartCoroutine(FadeBlack(duration));
    }


    IEnumerator FadeBlack(float duration)
    {

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
            // adjust the position of the menu
            fadeScreen.color = new Color(0f, 0f, 0f,percent);
            // wait a frame
            yield return null;
        }

            SceneManager.LoadScene(goToScene, LoadSceneMode.Additive);

    }


    public void FadeIn(float duration)
    {
        StartCoroutine(FadeClear(duration));
    }


    IEnumerator FadeClear(float duration)
    {

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
            // adjust the position of the menu
            fadeScreen.color = new Color(0f, 0f, 0f,1 - percent);
            // wait a frame
            yield return null;
        }

    }

    public void TapButton()
    {
        buttonTapAudioSource.Play();
    }

    public void SetMenuStart(Menu menu, string loc)
    {
        test.menu = menu;
        test.localizationFile = loc;
    }


}
