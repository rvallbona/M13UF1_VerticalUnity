using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimatorManager : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetFloat("Speed", playerController.GetCurrentSpeed());
        animator.SetBool("Jump", Input_Manager._INPUT_MANAGER.GetSpaceButtonPressed());
    }
}
