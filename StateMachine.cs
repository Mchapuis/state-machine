using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    public IState currentlyRunningState;
    private IState previousState;
    private IState runningState;
    public string _currentlyRunningState;                               // flag to check easily what state the machine is in

    public void changeState(IState newState)
    {

        // exit current state and save it
        if (this.currentlyRunningState != null)
        {
            this.currentlyRunningState.Exit();
        }
        
        // save previous
        this.previousState = this.currentlyRunningState;
        // update
        this.currentlyRunningState = newState;
        // start 
        this.currentlyRunningState.Enter();

        // to check what state easily
        _currentlyRunningState = this.currentlyRunningState.ToString();
    }

    public void ExecuteStateUpdate()
    {
        // excecute state
        runningState = this.currentlyRunningState;
        if(runningState != null)
        {
            runningState.Tick();
        }
    }

    public void switchToPreviousState()
    {
        // allow to go back
        this.currentlyRunningState.Exit();
        this.currentlyRunningState = this.previousState;
        this.currentlyRunningState.Enter();
        
    }

}
