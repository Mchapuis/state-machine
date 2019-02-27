using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CONDITIONS to switch to this state: 1- currentstate is SelectedState 2- Mouse is released 3- cursor is over a slot
/// When a card is in a slot it has the InSlotState
/// </summary>
public class InSlotState :IState {

    public System.Action<SnapResults> resultCallBack;
    private CardDisplay currentCard;
    private SlotDisplay currentSlot;
    private List<SlotDisplay> arr_of_slots;
    public bool snapCompleted;


    // constructor
    public InSlotState(CardDisplay currentCard, SlotDisplay currentSlot, List<SlotDisplay> arr_of_slots, Action<SnapResults> resultCallBack)
    {
        this.currentCard = currentCard;
        this.currentSlot = currentSlot;
        this.arr_of_slots = arr_of_slots;
        this.resultCallBack = resultCallBack;
    }

    public void Enter()
    {
        Debug.Log("InSlot State");
        this.snapCompleted = false;
    }

    public void Tick()
    {
        //Debug.Log("result of snap....");
        if (!snapCompleted)
        {
            // update slot manager
            SlotDict.slot_dictionary_manager.FillSlot(this.currentSlot.slot.slotNumber, this.currentCard.card.number);

            // snapping state --- make card move
            //Debug.Log("THIS CURRENT CARD TRANSFORM POSITION "+ this.currentCard.transform.position);
            //this.currentCard.transform.position = this.currentSlot.gameObject.transform.position;

            // REMOVING THIS --- SHOULD NOT BE HANDLE BY THE CARD STATE
            // update array of slots with the card currently in
            // if the new card has not been registered yet in the slot, 
            //if (!this.currentSlot.slot.b_Filled)
            //{
            //    // card is currently in the slot
            //    this.currentSlot.slot.b_Filled = true;

            //    // calling the slot manager to update the slots
            //    //Debug.Log("The card number is " + aCard.card.number + " and the number of the slot is " + slot.slotNumber);
            //    //SlotDict.slot_dictionary_manager.FillSlot(this.currentSlot.slot.slotNumber, this.currentCard.card.number);

            //    this.snapCompleted = true;
            //}
            //else
            //{
            //    this.snapCompleted = false;
            //    throw new Exception("Problem with the In Slot State...couldn't snap to slot since it's already filled. Should change logic of state machine");
            //}
            this.currentCard.transform.position = this.currentSlot.gameObject.transform.position;
            ///if(this.currentSlot.slot.b_Filled)this.currentSlot.slot.b_Filled = true;
            this.snapCompleted = true;
            // sending information back to card
            var snap_results = new SnapResults(snapCompleted);

            // publish event to subscribers
            this.resultCallBack(snap_results);

        }
    }

    public void Exit()
    {

    }

}


public class SnapResults
{
    public bool b_snapCompleted;

    public SnapResults(bool b_snapCompleted)
    {
        this.b_snapCompleted = b_snapCompleted;
    }
}
