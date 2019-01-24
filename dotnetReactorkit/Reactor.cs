using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace dotnetReactorkit
{
    /// <summary>
    /// Mutation 
    /// </summary>
    public abstract class ReactorMutation { }

    /// <summary>
    /// Reactor manages View state and user interaction.
    /// </summary>
    /// <typeparam name="ActionType"></typeparam>
    /// <typeparam name="StateType"></typeparam>
    public class Reactor<ActionType, StateType> {

        /// <summary>
        /// Subject of Action
        /// </summary>
        protected Subject<ActionType> _action { get; } = new Subject<ActionType>();

        /// <summary>
        /// Observable of State 
        /// </summary>
        private IObservable<StateType> _state { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected StateType InitialState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected StateType CurrentState { get; set; }


        /// <summary>
        /// Get Subject<Action> object.
        /// </summary>
        public Subject<ActionType> action {
            get { return this._action; }
        }

        /// <summary>
        /// Get Observable<State> object.
        /// </summary>
        public IObservable<StateType> state {
            get { return this._state; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Reactor(StateType initialState) {
            this.InitialState = initialState;
            this._state = this.CreateStateStream();
        }

        /// <summary>
        /// Create Flux Stream
        /// </summary>
        /// <returns></returns>
        private IObservable<StateType> CreateStateStream() {
            var action = this._action.AsObservable();
            var mutation = action.SelectMany(__action => {
                return Mutate(action: __action);
            });
            var state = mutation.Scan(this.InitialState, (__state, __mutation) => {
                return Reduce(state: __state, mutation: __mutation);
            })
            .StartWith(this.InitialState)
            .ObserveOnDispatcher()
            .Do((__state) => {
                this.CurrentState = __state;
            })
            .Replay(1);
            return state.AutoConnect();
        }

        /// <summary>
        /// Override on subclass
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual IObservable<ReactorMutation> Mutate(ActionType action) {
            return Observable.Empty<ReactorMutation>();
        }

        /// <summary>
        /// Override on subclass
        /// </summary>
        /// <param name="state"></param>
        /// <param name="mutation"></param>
        /// <returns></returns>
        protected virtual StateType Reduce(StateType state, ReactorMutation mutation) {
            return state;
        }
    }
}
