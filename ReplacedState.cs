using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CONDITIONS to switch to this state:
/// When a card is pushed away from a slot by another card, it is in the ReplacedState so that the card is pushed away
/// TODO: If card is selected than
/// </summary>
public class ReplacedState : IState
{
    private CardDisplay currentCard;
    private SlotDisplay currentSlot;
    private List<SlotDisplay> arr_of_slots;

    public System.Action<ReplacedResult> resultCallBack;
    private bool completed;

    // to push away the card
    private Vector3 slotPosition;
    private Vector3 currentPosition;
    private Vector3 heading;
    private float distance;
    private Vector3 direction;

    public ReplacedState(CardDisplay currentCard, SlotDisplay currentSlot, List<SlotDisplay> arr_of_slots, Action<ReplacedResult> resultCallBack)
    {
        this.currentCard = currentCard;
        this.currentSlot = currentSlot;
        this.arr_of_slots = arr_of_slots;
        this.resultCallBack = resultCallBack;
    }

    public void Enter()
    {
        Debug.Log("Replaced State");
        this.completed = false;

        // remove from array because snapping too far away
        //SlotDict.slot_dictionary_manager.EmptySlot(this.currentSlot.slot.slotNumber, this.currentCard.card.number);

    }

    public void Tick()
    {
        if (!this.completed)
        {
            // remove from array because snapping too far away
            SlotDict.slot_dictionary_manager.EmptySlot(this.currentSlot.slot.slotNumber, this.currentCard.card.number);

            // update slot information - double check
            this.currentSlot.slot.b_Filled = false;

            // position of delimiter + it's total size
            var z_pos = ColliderDisabled.delimiter_slot_and_card.z + ColliderDisabled.m_height.z;
            

            this.currentCard.transform.position = new Vector3(this.currentCard.transform.position.x, this.currentCard.transform.position.y, z_pos);

            //this.heading = slotPosition - currentPosition;
            //this.distance = heading.magnitude;
            //this.direction =  //heading / distance;
            this.currentPosition = this.currentCard.transform.position;

            this.direction = new Vector3(0,0,1);

            // sending information back to card
            var results = new ReplacedResult(direction);

            // publish event to subscribers
            this.resultCallBack(results);

            // state change finished
            this.completed = true;

        }
    }

    public void Exit()
    {

    }
}

public class ReplacedResult
{
    public Vector3 direction;
    public ReplacedResult(Vector3 direction)
    {
        this.direction = direction;
    }
}

