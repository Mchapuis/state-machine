public interface IState{

    void Enter();                                         // On state starts
    void Tick();                                          // Update During state
    void Exit();                                          // On exiting the state
}
