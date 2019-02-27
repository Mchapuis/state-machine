using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// CONDITIONS to switch to this state: user is clicking the card
/// the card is in SelectedState mode until the user release the card
/// </summary>
public class SelectedState : IState {

    public System.Action<SelectedResults> resultCallBack;
    private CardDisplay currentCard;
    private SlotDisplay currentSlot;
    private List<SlotDisplay> arr_of_slots;
    public bool b_card_is_selected;
    private enum ResultState { InSlotState = 0, NoSnappingState = 1, SelectedState = 2 };

    // constructor
    public SelectedState(CardDisplay currentCard, SlotDisplay currentSlot, List<SlotDisplay> arr_of_slots, Action<SelectedResults> resultCallBack)
    {
        this.resultCallBack = resultCallBack;
        this.currentCard = currentCard;
        this.currentSlot = currentSlot;
        this.arr_of_slots = arr_of_slots;
    }

    /// <summary>
    /// 
    /// Set card state to true so that CardDisplay can handle states changes
    /// </summary>
    public void Enter()
    {
        this.b_card_is_selected = true;
    }

    /// <summary>
    /// The update method called in state machine
    /// </summary>
    public void Tick()
    {
        var resultState = (int)ResultState.SelectedState;
        if (this.currentSlot != null)                                                           // slot is touched by card (for error check) 
        {
            if (this.currentSlot.GetCursorOnSlot() && Input.GetMouseButtonUp(0))                    // finger/mouse is touching the slot and the input is released
            {
                if (SlotDict.slot_dictionary_manager.IsAvailable(this.currentSlot.slot.slotNumber)) // slot is not already filled
                {
                    // IN SLOT STATE :: card is snapped to slot position
                    //this.stateMachine.changeState(new InSlotState(this, this.in_slot, this.arr_of_slots, this.DoIfSnappedToSlot));
                    resultState = (int)ResultState.InSlotState;
                    Debug.Log("Hello");
                }

            }
            else if (!this.currentSlot.GetCursorOnSlot() && Input.GetMouseButtonUp(0))// finger/mouse is NOT touching the slot
            {                                                                // and the input is released
                                                                             // NOT SNAPPING STATE :: card is puched away
                                                                             //this.stateMachine.changeState(new NoSnappingState(this, in_slot, this.DoIfNotSnappedToSlot));
                resultState = (int)ResultState.NoSnappingState;
            }
        }
        // return result as a callback
        var res = new SelectedResults(resultState);
        this.resultCallBack(res);
    }

    /// <summary>
    /// On card release set the card flag to false
    /// </summary>
    public void Exit()
    {
        this.b_card_is_selected = false;
    }

}

/// <summary>
/// Helper class for the result returned
/// </summary>
public class SelectedResults
{
    public int i_state_result;
    public SelectedResults(int i_state_result)
    {
        this.i_state_result = i_state_result;
    }
    
}
