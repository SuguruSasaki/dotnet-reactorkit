# .Net Reactorkit 

This is C # version of Reactorkit. 

Original code is [swift](https://github.com/ReactorKit/ReactorKit)  
I love ReactorKit in swift :)

## Concept
ReactorKit is a combination of Flux and Reactive Programming. 
The user actions and the view states are delivered to each layer via observable streams.
These streams are unidirectional: the view can only emit actions and the reactor can only emit states.

## Usage

Refer to the following code and create your Reactor class.

```c#

// Bind state
this._reactor.state
    .Select(state => state.Counter.ToString())
    .Bind(to: this.LabelResult.rx_Text())
    .DisposeBag(bag: this._disposeBag);

// Bind Button action
this.ButtonCount.rx_Tap()
    .Select(x => new MainAction(action: MainAction.Action.didChange))
    .Bind(to: this._reactor.action)
    .DisposeBag(bag: this._disposeBag);

```


### Action
Actions represent user actions and Views issue Actions.

```c#
enum Action {
    
}
```

### Mutation

```c#
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

### State

```c#
struct State {
    public int Counter { get; set; }
}
```

### Reactor

```c#
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