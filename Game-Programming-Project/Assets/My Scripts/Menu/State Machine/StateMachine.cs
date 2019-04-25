namespace StateMachine
{
    public class StateMachine<T>
    {
        public State<T> currentState; //{ get; private set; }
        public T type;

        public StateMachine(T type)
        {
            this.type = type;
            currentState = null;
        }

        public void ChangeState(State<T> newState)
        {
            if (currentState != null) currentState.ExitState(type);
            currentState = newState;
            currentState.EnterState(type);
        }

        public void Update()
        {
            if (currentState != null) currentState.UpdateState(type);
        }
    }
}