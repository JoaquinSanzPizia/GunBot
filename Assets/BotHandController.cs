using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHandController : MonoBehaviour
{
    public BotController botController;
    public GameObject[] handMasks;
    public GameObject[] hands;

    void Start()
    {
        botController = gameObject.GetComponent<BotController>();
    }

    void Update()
    {
        RotateHand();
    }

    void RotateHand()
    {
        hands[0].transform.eulerAngles = new Vector3(0f, 0f, botController.lookingAngle - 180f);
    }


    void ActivateHand(bool isLeft)
    {
        if (isLeft)
        {
            handMasks[0].SetActive(true);
            hands[0].SetActive(true);
        }
        else
        {
            handMasks[1].SetActive(true);
            hands[1].SetActive(true);
        }
    }

    void DeactivateHands()
    {
        handMasks[0].SetActive(false);
        hands[0].SetActive(false);
        handMasks[1].SetActive(false);
        hands[1].SetActive(false);
    }
}
