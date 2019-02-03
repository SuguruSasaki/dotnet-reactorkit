using dotnetReactorkit;
using RxUWP.Disposable;
using RxUWP.Observable.Extensions;
using RxUWP.UI.Extensions;
using Sample.Reactor;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;


using MainAction = Sample.Reactor.ActionStruct;
namespace Sample
{
    public sealed partial class MainPage : Page
    {
        private IReactable<MainAction, State> _reactor = new MainReactor();

        private DisposeBag _disposeBag = new DisposeBag();
       
        public MainPage()
        {
            this.InitializeComponent();

            // Update only when even number

            this._reactor.state
                .Select(state => state.Counter.ToString())
                .Bind(to: this.LabelResult.rx_Text())
                .DisposeBag(bag: this._disposeBag);

            this.ButtonCount.rx_Tap()
                .Select(x => new MainAction(action: MainAction.Action.didChange))
                .Bind(to: this._reactor.action)
                .DisposeBag(bag: this._disposeBag);
        }
    }
}
