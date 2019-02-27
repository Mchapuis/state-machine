using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CONDITIONS to switch to this state: 1- If mouse is not over the card  2- if the card is not moving
/// </summary>
public class IdleState : IState
{
    private CardDisplay currentCard;
    public System.Action<IdleStateResults> resultCallBack;
    private bool completed;

    public IdleState(CardDisplay currentCard, Action<IdleStateResults> resultCallBack)
    {
        this.currentCard = currentCard;
        this.resultCallBack = resultCallBack;
    }

    void IState.Enter()
    {
        //Debug.Log("Idle State");
        this.completed = false;
    }

    void IState.Tick()
    {

        if (!this.completed)
        {

            this.completed = true;
            // sending information back to card
            var results = new IdleStateResults();

            // publish event to subscribers
            this.resultCallBack(results);
        }
    }

    void IState.Exit()
    {

    }
}

public class IdleStateResults
{
    public IdleStateResults()
    {

    }
}
