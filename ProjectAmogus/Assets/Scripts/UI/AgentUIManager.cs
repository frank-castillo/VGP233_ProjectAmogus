using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentUIManager : MonoBehaviour
{
    // Color Variables
    [Header("Characters' borders")]
    [SerializeField] private Image pictureBorderPlayer;
    [SerializeField] private Image pictureBorderAgentA;
    [SerializeField] private Image pictureBorderAgentB;
    [SerializeField] private Image pictureBorderAgentC;

    private Color playerColor = new Color(0.8235294f, 0.8235294f, 0.8235294f, 1.0f);
    private Color alexColor = new Color(0.5450981f, 0.6862745f, 0.9254902f, 1.0f);
    private Color BergColor = new Color(0.7490196f, 0.7058824f, 0.8509804f, 1.0f);
    private Color CaelColor = new Color(0.6117647f, 0.7607843f, 0.5450981f, 1.0f);
    private Color selectedColor = new Color(0.07f, 1.0f, 0.0f, 1.0f);

    // Order's sprites logic
    [Header("Orders Logic")]
    [SerializeField] private Sprite[] orderImage;
    [SerializeField] private Image playerOrder;
    [SerializeField] private Image alexOrder;
    [SerializeField] private Image bergOrder;
    [SerializeField] private Image caelOrder;
    [SerializeField] private EntityUI[] entities;
    int generalOrder = 0;

    // Objectives Logic
    [SerializeField] private List<GameObject> objectives = new List<GameObject>();
    [SerializeField] private GameObject objectivesContainer;
    [SerializeField] private GameObject objectivePrefab;

    // External references
    //[Header("References")]
    [SerializeField] private UnitManager unitManager;

    private static AgentUIManager uIManager;

    public static AgentUIManager Instance { get { return uIManager; } }

    private void Awake()
    {
        if (uIManager != null && uIManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            uIManager = this;
        }

        var allEntities = FindObjectsOfType<PlayerChara>();

        foreach (var entity in allEntities)
        {
            foreach (var uiEntity in entities)
            {
                if (uiEntity.GetEntityUIType() == TypeOfEntityUI.Scientist && entity.GetType() == EntityType.Scientist)
                {
                    uiEntity.SetPlayerCharaReference(entity);
                }
                else if (uiEntity.GetEntityUIType() == TypeOfEntityUI.Alex && entity.GetType() == EntityType.TrooperA)
                {
                    uiEntity.SetPlayerCharaReference(entity);
                }
                else if (uiEntity.GetEntityUIType() == TypeOfEntityUI.Berg && entity.GetType() == EntityType.TrooperB)
                {
                    uiEntity.SetPlayerCharaReference(entity);
                }
                else if (uiEntity.GetEntityUIType() == TypeOfEntityUI.Cael && entity.GetType() == EntityType.TrooperC)
                {
                    uiEntity.SetPlayerCharaReference(entity);
                }
            }
        }

        SetAsActivePlayer(0);

    }

    private void Start()
    {
        SetAllOrdersImagesByClick();
    }

    public void SetAsActivePlayer(int unit)
    {
        switch (unit)
        {
            case 0:
                pictureBorderAgentA.color = alexColor;
                pictureBorderAgentB.color = BergColor;
                pictureBorderAgentC.color = CaelColor;
                pictureBorderPlayer.color = selectedColor;
                break;
            case 1:
                pictureBorderAgentA.color = selectedColor;
                pictureBorderAgentB.color = BergColor;
                pictureBorderAgentC.color = CaelColor;
                pictureBorderPlayer.color = playerColor;
                break;
            case 2:
                pictureBorderAgentA.color = alexColor;
                pictureBorderAgentB.color = selectedColor;
                pictureBorderAgentC.color = CaelColor;
                pictureBorderPlayer.color = playerColor;
                break;
            case 3:
                pictureBorderAgentA.color = alexColor;
                pictureBorderAgentB.color = BergColor;
                pictureBorderAgentC.color = selectedColor;
                pictureBorderPlayer.color = playerColor;
                break;
            default:
                break;
        }
    }

    public void SetAllOrdersImagesByClick()
    {
        if (generalOrder % 4 == 0 || generalOrder > 4) generalOrder = 0;

        switch (generalOrder)
        {
            case 0:
                playerOrder.overrideSprite = orderImage[0];
                bergOrder.overrideSprite = orderImage[0];
                caelOrder.overrideSprite = orderImage[0];
                alexOrder.overrideSprite = orderImage[0];
                foreach (var unit in UnitManager.units)
                {
                    unit.Priority = AIPriority.FollowMe;
                }
                break;
            case 1:
                playerOrder.overrideSprite = orderImage[1];
                bergOrder.overrideSprite = orderImage[1];
                caelOrder.overrideSprite = orderImage[1];
                alexOrder.overrideSprite = orderImage[1];
                foreach (var unit in UnitManager.units)
                {
                    unit.Priority = AIPriority.CloseIn;
                }
                break;
            case 2:
                playerOrder.overrideSprite = orderImage[2];
                bergOrder.overrideSprite = orderImage[2];
                caelOrder.overrideSprite = orderImage[2];
                alexOrder.overrideSprite = orderImage[2];
                foreach (var unit in UnitManager.units)
                {
                    unit.Priority = AIPriority.SpreadOut;
                }
                break;
            case 3:
                playerOrder.overrideSprite = orderImage[3];
                bergOrder.overrideSprite = orderImage[3];
                caelOrder.overrideSprite = orderImage[3];
                alexOrder.overrideSprite = orderImage[3];
                foreach (var unit in UnitManager.units)
                {
                    unit.Priority = AIPriority.WaitHere;
                }
                break;
            default:
                break;
        }

        ++generalOrder;
        foreach (var entity in entities)
        {
            entity.SetActiveOrder(generalOrder);
        }
    }

    public void SetAllOrdersImagesByKeyboard(AIPriority priority)
    {
        switch (priority)
        {
            case AIPriority.FollowMe:
                generalOrder = 0;
                break;
            case AIPriority.WaitHere:
                generalOrder = 1;
                break;
            case AIPriority.SpreadOut:
                generalOrder = 2;
                break;
            case AIPriority.CloseIn:
                generalOrder = 3;
                break;
        }

        switch (generalOrder)
        {
            case 0:
                playerOrder.overrideSprite = orderImage[0];
                bergOrder.overrideSprite = orderImage[0];
                caelOrder.overrideSprite = orderImage[0];
                alexOrder.overrideSprite = orderImage[0];
                break;
            case 1:
                playerOrder.overrideSprite = orderImage[1];
                bergOrder.overrideSprite = orderImage[1];
                caelOrder.overrideSprite = orderImage[1];
                alexOrder.overrideSprite = orderImage[1];
                break;
            case 2:
                playerOrder.overrideSprite = orderImage[2];
                bergOrder.overrideSprite = orderImage[2];
                caelOrder.overrideSprite = orderImage[2];
                alexOrder.overrideSprite = orderImage[2];
                break;
            case 3:
                playerOrder.overrideSprite = orderImage[3];
                bergOrder.overrideSprite = orderImage[3];
                caelOrder.overrideSprite = orderImage[3];
                alexOrder.overrideSprite = orderImage[3];
                break;
            default:
                break;
        }
    }

    public UnitManager GetUnitManager() { return unitManager; }

    public void AddNewObjective(Objective objective)
    {
        var temp = Instantiate(objectivePrefab);
        temp.transform.SetParent(objectivesContainer.transform);
        objectives.Add(temp);
        temp.GetComponent<ObjectiveTemplate>().objectiveText.text = objective.text;
        temp.GetComponent<ObjectiveTemplate>().objectiveEventType = objective;
    }

    public void DeleteObjective(ObjectiveEvent objectiveEvent)
    {
        foreach(var prefab in objectives)
        {
            var objective = prefab.GetComponent<ObjectiveTemplate>();
            if(objective.objectiveEventType.objectiveEvent == objectiveEvent)
            {
                Destroy(prefab);
            }
        }
    }
}
