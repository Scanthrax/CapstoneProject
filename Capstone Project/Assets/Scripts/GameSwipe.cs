using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSwipe : MonoBehaviour
{

    public static GameSwipe instance;

    public GameObject gamePanel;

    public Transform panelContainer;
    Vector3[] panelPositions;

    public float duration;

    public AnimationCurve sweepCurve;

    List<GamePanel> uiPanels = new List<GamePanel>();

    public List<MinigameObject> listOfMinigames;

    public LinkedList<MinigameObject> linkedPanels;


    LinkedListNode<MinigameObject> node;



    bool transitioning;





    public float scaleSize;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {

        linkedPanels = new LinkedList<MinigameObject>();

        panelPositions = new Vector3[5];
        panelPositions[0] = (Vector3.right * -375f * 2f) + (Vector3.down * 150f);
        panelPositions[1] = (Vector3.left * 375f) + (Vector3.down * 150f);
        panelPositions[2] = (Vector3.zero) + (Vector3.down * 150f);
        panelPositions[3] = (Vector3.right * 375f) + (Vector3.down * 150f);
        panelPositions[4] = (Vector3.right * 375f * 2f) + (Vector3.down * 150f);

        foreach (var item in listOfMinigames)
        {
            linkedPanels.AddLast(item);
        }

        node = linkedPanels.First;


        transitioning = false;


        for (int i = 0; i < 5; i++)
        {
            uiPanels.Add(Instantiate(gamePanel,panelContainer).GetComponent<GamePanel>());
        }

        for (int i = 0; i < uiPanels.Count; i++)
        {
            UpdatePanel(uiPanels[i], GetNodeValue(i - 2));
            uiPanels[i].position = i;
            uiPanels[i].transform.localPosition = panelPositions[i];
            uiPanels[i].transform.rotation = Quaternion.identity;

            
        }

        uiPanels[2].transform.localScale = Vector3.one * scaleSize;


        //SetSelectedMinigame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwipeLeft();
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            SwipeRight();

        }
    }

    public void SwipeLeft()
    {
        if (transitioning) return;

        transitioning = true;
        PreviousNode();
        for (int i = 0; i < uiPanels.Count; i++)
        {
            MoveMenus(uiPanels[i], true);
        }

        //SelectedMinigame(node.Value);
    }

    public void SwipeRight()
    {
        if (transitioning) return;

        transitioning = true;
        NextNode();
        for (int i = 0; i < uiPanels.Count; i++)
        {
            MoveMenus(uiPanels[i], false);
        }
    }


    void NextNode()
    {
        node = node.Next ?? node.List.First;
        //SetSelectedMinigame();
    }
    void PreviousNode()
    {
        node = node.Previous ?? node.List.Last;
        //SetSelectedMinigame();
    }


    void SetSelectedMinigame()
    {
        MenuSelection.instance.selectedMinigame = node.Value;
    }


    MinigameObject GetNodeValue(int iter)
    {
        LinkedListNode<MinigameObject> tempNode = node;

        for (int i = 0; i < (int)Mathf.Abs(iter); i++)
        {
            if (Mathf.Sign(iter) > 0)
                tempNode = tempNode.Next ?? tempNode.List.First;
            else
                tempNode = tempNode.Previous ?? tempNode.List.Last;
        }

        return tempNode.Value;
    }


    /// <summary>
    /// Transition between 2 menus
    /// </summary>
    /// <param name="obj">The menu to move</param>
    /// <param name="setInactive">Do we set the menu as inactive after transitioning?</param>
    void MoveMenus(GamePanel menu, bool right)
    {

        StartCoroutine(AnimateMove(menu, right));
    }

    IEnumerator AnimateMove(GamePanel menu, bool right)
    {


        if (menu.position == 2)
            ScalePanel(menu, false);


        menu.NextPosition(right);


        if (menu.position == 2)
            ScalePanel(menu, true);




        if ((menu.position == 4 && !right) || (menu.position == 0 && right))
        {
            menu.transform.localPosition = panelPositions[menu.position];

            UpdatePanel(menu, right ? GetNodeValue(3) : GetNodeValue(-3));
        }
        else
        {

            Vector2 target = panelPositions[menu.position];

            Vector2 origin = menu.transform.localPosition;

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
        }
    }


    void ScalePanel(GamePanel menu, bool scaleUp)
    {
        StartCoroutine(AnimateScale(menu, scaleUp));
    }


    IEnumerator AnimateScale(GamePanel menu, bool scaleUp)
    {
        Vector3 target = scaleUp ? Vector3.one * scaleSize : Vector3.one;
        Vector3 origin = scaleUp ? Vector3.one : Vector3.one * scaleSize;


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
            menu.transform.localScale = Vector3.LerpUnclamped(origin, target, curvePercent);
            // wait a frame
            yield return null;
        }



        transitioning = false;
    }


    void UpdatePanel(GamePanel panel, MinigameObject minigame)
    {
        panel.UpdateMinigame(minigame);
        panel.amtOfPlayers.text = "Players: " + panel.minigame.AmountOfPlayers.ToString();
        panel.titleOfGame.text =  panel.minigame.Name;
        panel.image.sprite = panel.minigame.Thumbnail;
        panel.topicOfGame.text = panel.minigame.topicString;
    }

    void SelectedMinigame(MinigameObject minigame)
    {
        MenuSelection.goToScene = MenuSelection.introScene;
        MenuSelection.numPlayers = minigame.AmountOfPlayers;
        MenuSelection.goToMinigameScene = minigame.sceneName;
    }

}
