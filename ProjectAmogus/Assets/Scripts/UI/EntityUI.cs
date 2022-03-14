using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

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
        }

        ++activeOrder;
    }

    public void SetActiveOrder(int order) { activeOrder = order; }
}
