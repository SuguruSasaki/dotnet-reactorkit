﻿using dotnetReactorkit;
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
       
        public MainPage()
        {
            this.InitializeComponent();

            // Update only when even number
 
            var token = this._reactor.state
                .Select(state => state.Counter)
                //.Where(counter => counter % 2 == 0)
                .Subscribe(counter => {
                    this.LabelResult.Text = (counter).ToString();
                });

        
            this.ButtonCount.Click += (sender, e) => {
                this._reactor.action.OnNext(ActionStruct.Dispatcher(MainAction.Action.didChange));
            };

        }
    }
}
