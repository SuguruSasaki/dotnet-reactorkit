using System;
using System.Reactive.Subjects;

namespace dotnetReactorkit {

    public interface IReactable<Action, State> {

        Subject<Action> action { get; }


        IObservable<State> state { get; }

    }
}
