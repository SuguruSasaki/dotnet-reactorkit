# .Net Reactorkit 

This is C # version of Reactorkit. 

Original code is [swift](https://github.com/ReactorKit/ReactorKit)  
I love ReactorKit in swift :)

## Concept
ReactorKit is a combination of Flux and Reactive Programming. 
The user actions and the view states are delivered to each layer via observable streams.
These streams are unidirectional: the view can only emit actions and the reactor can only emit states.

## How to use

Refer to the following code and create your Reactor class.

Action

```
enum Action {
    
}
```

MutationType

```
enum MutationType {
    
}

public class Mutation : ReactorMutation {
    internal MutationType CurrentType;
    internal object Param;

    internal Mutation(MutationType type, object param = null) {
        this.CurrentType = type;
        this.Param = param;
    }
}
```

State

```
struct State {
    public int Counter { get; set; }
}
```

Reactor

```
class MainReactor: Reactor<Action, State> {

    private static State initState = new State {

    };

    public MainReactor(): base(initialState: initState) {}

    protected override IObservable<ReactorMutation> Mutate(Action action) {
        switch (action) {
            default:
                return Observable.Empty<Mutation>();
        }
    }

    protected override State Reduce(State state, ReactorMutation mutation) {
        var m = mutation as Mutation;
        var newState = state;
    
        return newState;
    }
}


```