using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    [Header("Cards Status (*no need to change)")]
    [Tooltip("Unique ID for each cover card, must be assigned sequentially from 0")] 
    public int coverId;
    [Tooltip("ID from card type, each card has a pair of the same type of card (no need to change in inspector)")] 
    public int cardId;
    [Tooltip("The status of card is open or not (no need to change in inspector)")]
    public bool isOpened;
    [Tooltip("The status of card is open or not (no need to change in inspector)")]
    public Animator cardAnimator;

    void Start()
    {
        // Get the Animator component attached to this card GameObject
        // (needed to trigger animations when the card is flipped)
        cardAnimator = GetComponent<Animator>();
    }

    // This function is called when the player clicks on the card cover
    public void CoverCardIsClicked()
    {
        // Tell the CardManager singleton that this card has been clicked
        // Passes the coverId so CardManager knows which card to open
        CardManager.instance.OpenCard(coverId);
    }
}
