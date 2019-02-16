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
        DontDestroyOnLoad(gameObject);
    }



    /// <summary>
    /// Directions for the menu transitions
    /// </summary>
    public enum Direction { Left, Right, Up, Down }


    /// <summary>
    /// Directions for the menu transitions
    /// </summary>
    public enum Menu { Welcome, GameSelect, ChooseLanguage, ChooseDifficulty, TopicSelect }

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

    public Object introScene;

    public string currentMenu;

    public List<ButtonMapping> buttonMappings = new List<ButtonMapping>();

    [System.Serializable]
    public struct ButtonMapping
    {
        public Menu to;
        public Button[] button;
    }

    [System.Serializable]
    public struct MenuMapping
    {
        public Menu menu;
        public RectTransform transform;
    }

    public List<MenuMapping> listOfMenus;


    public Image fadeScreen;


    public static string goToScene;

    void Start()
    {

        foreach (var item in listOfMenus)
        {
            menuDictionary.Add(item.menu.ToString(), item.transform);
        }

        foreach (var mapping in buttonMappings)
        {
            for (int i = 0; i < mapping.button.Length; i++)
            {
                mapping.button[i].onClick.AddListener(delegate { GoToNextMenu(mapping.to.ToString()); });
            }
        }

        // disable all menus
        foreach (KeyValuePair<string, RectTransform> entry in menuDictionary)
        {
            entry.Value.gameObject.SetActive(true);
            entry.Value.localPosition = new Vector2(0, 1334);
        }

        // declare which menu we will be starting at
        currentMenu = Menu.ChooseLanguage.ToString();

        RectTransform startMenu = null;

        if (menuDictionary.ContainsKey(currentMenu))
            startMenu = menuDictionary[currentMenu];

        // put menu at center of screen
        if (startMenu)
        {
            startMenu.gameObject.SetActive(true);
            startMenu.localPosition = new Vector2(0, 0);
        }


<<<<<<< HEAD
=======


        foreach (var item in GameObject.FindObjectsOfType<Button>())
        {
            item.onClick.AddListener(delegate { TapButton(); });
        }

<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        print(goToScene);
        SceneManager.LoadScene("Introduction", LoadSceneMode.Single);
=======
=======
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15
        if (goToScene != null)
        {
            print(goToScene);
            SceneManager.LoadScene("Introduction", LoadSceneMode.Single);
        }
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15

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
}
