using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentUIManager : MonoBehaviour
{
    [SerializeField] private Image pictureBorderPlayer;
    [SerializeField] private Image pictureBorderAgentA;
    [SerializeField] private Image pictureBorderAgentB;
    [SerializeField] private Image pictureBorderAgentC;

    private Color playerColor = new Color(0.8235294f, 0.8235294f, 0.8235294f, 1.0f);
    private Color alexColor = new Color(0.5450981f, 0.6862745f, 0.9254902f, 1.0f);
    private Color BergColor = new Color(0.7490196f, 0.7058824f, 0.8509804f, 1.0f);
    private Color CaelColor = new Color(0.6117647f, 0.7607843f, 0.5450981f, 1.0f);
    private Color selectedColor = new Color(0.07f, 1.0f, 0.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
