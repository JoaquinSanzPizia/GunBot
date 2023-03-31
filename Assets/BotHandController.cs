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

    bool shooting;
    bool canShoot;

    private void Start()
    {
        canShoot = true;
    }
    void Update()
    {
        RotateHand();
        ChangeHandLayer();
        ShootGun();

        if (Input.GetMouseButton(0))
        {
            shooting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shooting = false;
        }
    }

    void RotateHand()
    {
        hands[0].transform.eulerAngles = new Vector3(0f, 0f, botController.lookingAngle - 180f);
        hands[1].transform.eulerAngles = new Vector3(0f, 0f, botController.lookingAngle);

        if (botController.lookDir == new Vector2(1, 0))
        {
            ActivateHand(false);
            currentGun.GetComponent<Gun>().ChangeSprite(false);
        }

        else if (botController.lookDir == new Vector2(-1, 0))
        {
            ActivateHand(true);
            currentGun.GetComponent<Gun>().ChangeSprite(false);
        }

        else if (botController.lookDir == new Vector2(0, -1) || botController.lookDir == new Vector2(0, 1))
        {
            currentGun.GetComponent<Gun>().ChangeSprite(true);
        }
    }

    void ChangeHandLayer()
    {
        if (botController.lookDir == new Vector2(0, -1)) //DOWN
        {
            hands[0].GetComponent<SpriteRenderer>().sortingOrder = 5;
            hands[1].GetComponent<SpriteRenderer>().sortingOrder = 5;

            currentGun.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }

        else if (botController.lookDir == new Vector2(0, 1)) //UP
        {
            hands[0].GetComponent<SpriteRenderer>().sortingOrder = 4;
            hands[1].GetComponent<SpriteRenderer>().sortingOrder = 4;

            currentGun.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }

        else if (botController.lookDir == new Vector2(-1, 0)) //LEFT
        {
            hands[0].GetComponent<SpriteRenderer>().sortingOrder = 4;
            hands[1].GetComponent<SpriteRenderer>().sortingOrder = 4;

            currentGun.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }

        else if (botController.lookDir == new Vector2(1, 0)) //RIGHT
        {
            hands[0].GetComponent<SpriteRenderer>().sortingOrder = 4;
            hands[1].GetComponent<SpriteRenderer>().sortingOrder = 4;

            currentGun.GetComponent<SpriteRenderer>().sortingOrder = 5;
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

    void ShootGun()
    {
        if (shooting)
        {
            if (canShoot)
            {
                currentGun.GetComponent<Gun>().Shoot();

                canShoot = false;

                LeanTween.delayedCall(1f / currentGun.GetComponent<Gun>().attackSpeed, () =>
                {
                    canShoot = true;
                });
            }           
        }        
    }
}
