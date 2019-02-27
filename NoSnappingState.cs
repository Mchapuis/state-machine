using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CONDITIONS to switch to this state: 1- current state SELECTED 2- mouse/finger is released 3- cursor/finger is NOT over a slot
/// </summary>
public class NoSnappingState : IState
{
    private CardDisplay currentCard;
    private SlotDisplay currentSlot;
    private bool completed;

    private Vector3 distanceToSlot;
    private Vector3 currentPosition;
    private Vector3 heading;
    private float distance;
    private Vector3 direction;
    public System.Action<NotSnapResults> resultCallBack;

    public NoSnappingState(CardDisplay currentCard, SlotDisplay currentSlot, Action<NotSnapResults> resultCallBack)
    {
        this.currentCard = currentCard;
        this.currentSlot = currentSlot;
        this.resultCallBack = resultCallBack;
    }

    public void Enter()
    {
        Debug.Log("No Snapping State");
        this.completed = false;

        // remove from array because snapping too far away
        SlotDict.slot_dictionary_manager.EmptySlot(this.currentSlot.slot.slotNumber, this.currentCard.card.number);

        // if the card is in the slots zone -- move it away
        // 1- get current location and height of slot
        var slot_height = this.currentSlot.GetComponent<Collider>().bounds.size.z;
        var slot_pos = this.currentSlot.transform.position;

        // 2- get current location and height of card
        var card_height = this.currentCard.GetComponent<Collider>().bounds.size.z;
        this.currentPosition = this.currentCard.transform.position;

        // 3- if the card position is LESS than card_height + slot_position
        if (this.currentPosition.z < (card_height + slot_pos.z))
        {
            var z_pos = ColliderDisabled.delimiter_slot_and_card.z + ColliderDisabled.m_height.z;
            this.currentCard.transform.position = new Vector3(this.currentCard.transform.position.x, this.currentCard.transform.position.y, z_pos);
        }

    }

    public void Tick()
    {
        if (!this.completed)
        {


            /// Get the direction and coordinates
            this.distanceToSlot = this.currentSlot.transform.position;
            this.currentPosition = this.currentCard.transform.position;

            this.heading = distanceToSlot - currentPosition;
            this.distance = heading.magnitude;
            this.direction = heading / distance;
            this.direction.y = 0;

            //if(this.direction.x > this.direction.z)
            //{
            //    var temp = this.direction.z;
            //    this.direction.z = this.direction.x;
            //    this.direction.x = temp;
            //}
            this.completed = true;
        }
        Debug.Log("result is"+ this.direction + " "+ this.distanceToSlot);
        // sending information back to card
        var snap_results = new NotSnapResults(direction);

        // publish event to subscribers
        this.resultCallBack(snap_results);

    }

    public void Exit()
    {
    }
}
public class NotSnapResults
{
    public Vector3 direction;

    public NotSnapResults(Vector3 direction)
    {
        this.direction = direction;
    }

}
