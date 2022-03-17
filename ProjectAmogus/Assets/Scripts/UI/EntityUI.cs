using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfEntityUI
{
    Scientist,
    Alex,
    Berg,
    Cael
}

public class EntityUI : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private Image orderImage;
    [SerializeField] private Sprite[] ordersImages;
    [SerializeField] private Button abilityButton;

    public float abilityCooldown;
    int activeOrder = 0;
    bool isCooldown;

    // ReferencesLogic
    [Header("References Logic")]
    [SerializeField] private TypeOfEntityUI entityType;
    private PlayerChara characterReference;
    [SerializeField] private Slider healthBar;

    // Update is called once per frame
    void Update()
    {
        if (isCooldown == true)
        {
            cooldownImage.fillAmount += 1.0f / abilityCooldown * Time.deltaTime;
            if (cooldownImage.fillAmount >= 1.0f)
            {
                isCooldown = false;
                abilityButton.interactable = true;
                cooldownImage.fillAmount = 0.0f;
            }
        }

        healthBar.value = characterReference.Health;
    }

    public TypeOfEntityUI GetEntityUIType() { return entityType; }

    public void SetPlayerCharaReference(PlayerChara character) { characterReference = character; }

    public void UseAbility()
    {
        isCooldown = true;
        abilityButton.interactable = false;
    }

    public void ChangeOrder()
    {
        if (activeOrder % 4 == 0 || activeOrder > 4) activeOrder = 0;

        if (this.tag == "Soldiers")
        {
            orderImage.overrideSprite = ordersImages[activeOrder];

            switch (activeOrder)
            {
                case 0:
                    characterReference.Priority = AIPriority.FollowMe;
                    break;
                case 1:
                    characterReference.Priority = AIPriority.CloseIn;
                    break;
                case 2:
                    characterReference.Priority = AIPriority.SpreadOut;
                    AgentUIManager.Instance.GetUnitManager().SetIndividualSpreadOut(characterReference.GetNavMeshAgent());
                    break;
                case 3:
                    characterReference.Priority = AIPriority.WaitHere;
                    break;
            }
        }

        ++activeOrder;
    }

    public void SetActiveOrder(int order) { activeOrder = order; }
}
