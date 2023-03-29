using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHandController : MonoBehaviour
{
    public BotController botController;
    public GameObject[] handMasks;
    public GameObject[] hands;
    public GameObject[] gunHolders;

    [SerializeField] GameObject currentGun;

    void Update()
    {
        RotateHand();
        ChangeHandLayer();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void RotateHand()
    {
        hands[0].transform.eulerAngles = new Vector3(0f, 0f, botController.lookingAngle - 180f);
        hands[1].transform.eulerAngles = new Vector3(0f, 0f, botController.lookingAngle);

        if (botController.lookDir == new Vector2(1, 0))
        {
            ActivateHand(false);
        }

        else if (botController.lookDir == new Vector2(-1, 0))
        {
            ActivateHand(true);
        }
    }

    void ChangeHandLayer()
    {
        if (botController.lookDir == new Vector2(0, -1))
        {
            hands[0].GetComponent<SpriteRenderer>().sortingOrder += 1;
            hands[1].GetComponent<SpriteRenderer>().sortingOrder += 1;
        }

        else if (botController.lookDir == new Vector2(0, 1))
        {
            hands[0].GetComponent<SpriteRenderer>().sortingOrder -= 1;
            hands[1].GetComponent<SpriteRenderer>().sortingOrder -= 1;
        }
    }


    void ActivateHand(bool isLeft)
    {
        DeactivateHands();

        if (isLeft)
        {
            handMasks[0].SetActive(true);
            hands[0].SetActive(true);

            currentGun = gunHolders[0].transform.GetChild(0).gameObject;
        }
        else
        {
            handMasks[1].SetActive(true);
            hands[1].SetActive(true);

            currentGun = gunHolders[1].transform.GetChild(0).gameObject;
        }
    }

    void DeactivateHands()
    {
        handMasks[0].SetActive(false);
        hands[0].SetActive(false);
        handMasks[1].SetActive(false);
        hands[1].SetActive(false);
    }

    void Shoot()
    {
        currentGun.transform.GetChild(0).GetComponent<ParticleSystem>().Play();           
    }
}
