﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{

    public Transform leftHandMount;
    public Transform rightHandMount;

    public Text ammoText;
    public Transform gunPivot;
    public Gun gun;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (playerInput.fire)
        {
            if (gun.state == Gun.State.Empty)
            {
                playerAnimator.SetTrigger("Reload");
                gun.Reload();
            }
            else
            {
                gun.Fire();
            }
        }

        UpdateUI();
    }

    private void OnAnimatorIK(int layerIndex)
    {

        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);
    }

    public void UpdateUI()
    {
        if (gun != null)
        {
            ammoText.text = gun.magAmmo + "/" + gun.ammoRemain;
        }
    }
}