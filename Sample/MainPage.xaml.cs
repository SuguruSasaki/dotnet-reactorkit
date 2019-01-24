using Sample.Reactor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Sample
{
    
    public sealed partial class MainPage : Page
    {

        private MainReactor _reactor = new MainReactor();
       
        public MainPage()
        {
            this.InitializeComponent();



            this._reactor.state.Subscribe(state => {
                this.LabelResult.Text = state.Counter.ToString();
            });

            this.ButtonCount.Click += (sender, e) => {

                this._reactor.action.OnNext(Sample.Reactor.Action.didChange);
            };

        }
    }
}
