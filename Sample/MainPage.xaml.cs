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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace Sample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
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
