using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetReactorkit;

namespace Sample.Reactor {

    public enum Action {
        didChange
    }

    public enum MutationType {
        UpdateCount
    }

    public struct State {
        public int Counter { get; set; }
    }

    public class Mutation : ReactorMutation {
        public MutationType CurrentType;
        public object Param;

        public Mutation(MutationType type, object param = null) {
            this.CurrentType = type;
            this.Param = param;
        }
    }
    public class MainReactor: Reactor<Action, State> {

        /// <summary>
        /// 
        /// </summary>
        private static State initState = new State {
            Counter = 0
        };


        public MainReactor(): base(initialState: initState) {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected override IObservable<ReactorMutation> Mutate(Action action) {
            switch (action) {
                
                case Action.didChange:

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
