using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentUIManager : MonoBehaviour
{
    public enum SelectedUnit
    {
        Player,
        AgentA,
        AgentB,
        AgentC
    }

    [SerializeField] private Image pictureBorderAgentA;
    [SerializeField] private Image pictureBorderAgentB;
    [SerializeField] private Image pictureBorderAgentC;
    [SerializeField] private Image pictureBorderPlayer;

    private Color defaultColor = new Color(0.3882353f, 0.7764706f, 1.0f, 1.0f);
    private Color selectedColor = new Color(0.07f, 1.0f, 0.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAsActivePlayer(SelectedUnit unit)
    {
        switch (unit)
        {
            case SelectedUnit.Player:
                pictureBorderAgentA.color = defaultColor;
                pictureBorderAgentB.color = defaultColor;
                pictureBorderAgentC.color = defaultColor;
                pictureBorderPlayer.color = selectedColor;
                break;
            case SelectedUnit.AgentA:
                pictureBorderAgentA.color = selectedColor;
                pictureBorderAgentB.color = defaultColor;
                pictureBorderAgentC.color = defaultColor;
                pictureBorderPlayer.color = defaultColor;
                break;
            case SelectedUnit.AgentB:
                pictureBorderAgentA.color = defaultColor;
                pictureBorderAgentB.color = selectedColor;
                pictureBorderAgentC.color = defaultColor;
                pictureBorderPlayer.color = defaultColor;
                break;
            case SelectedUnit.AgentC:
                pictureBorderAgentA.color = defaultColor;
                pictureBorderAgentB.color = defaultColor;
                pictureBorderAgentC.color = selectedColor;
                pictureBorderPlayer.color = defaultColor;
                break;
            default:
                break;
        }
    }
}
