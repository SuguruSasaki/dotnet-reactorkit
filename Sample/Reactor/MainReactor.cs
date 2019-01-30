using dotnetReactorkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sample.Reactor {

 
    public class ActionStruct {

        public enum Action {
            didChange
        }

        public Action ActionType;
        public dynamic Param;


        public ActionStruct(Action action, dynamic param = null) {
            this.ActionType = action;
            this.Param = param;
        }


        public static ActionStruct Dispatcher(Action action, dynamic param = null) {
            return new ActionStruct(action, param);
        }

    }

    internal enum MutationType {
        UpdateCount
    }

    public struct State {
        public int Counter { get; set; }
    }

    public class Mutation : ReactorMutation {
        internal MutationType CurrentType;
        internal object Param;

        internal Mutation(MutationType type, object param = null) {
            this.CurrentType = type;
            this.Param = param;
        }
    }

    internal class MainReactor: Reactor<ActionStruct, State>, IReactable<ActionStruct, State> {

        /// <summary>
        /// 
        /// </summary>
        private static State initState = new State {
            Counter = 0
        };


        public MainReactor(): base(initialState: initState) {}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected override IObservable<ReactorMutation> Mutate(ActionStruct action) {
            
            Debug.WriteLine("mutate done");
            switch (action.ActionType) {
                
                case ActionStruct.Action.didChange:
                    var c = this.CurrentState.Counter + 1;
                    return Observable.Return(new Mutation(type: MutationType.UpdateCount, param: c));

                default:
                    return Observable.Empty<Mutation>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="mutation"></param>
        /// <returns></returns>
        protected override State Reduce(State state, ReactorMutation mutation) {
            var m = mutation as Mutation;
            var newState = state;
            switch (m.CurrentType) {
                case MutationType.UpdateCount:

                    newState.Counter = (int)m.Param;
                    break;
            }
            return newState;
        }
    }
}
