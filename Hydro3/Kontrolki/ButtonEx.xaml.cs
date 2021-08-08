using Prism.Commands;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hydro3.Kontrolki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonEx : Frame
    {

        public ButtonEx()
        {
            InitializeComponent();
          
        }



        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(ButtonEx));

        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }

        public static readonly BindableProperty OpisProperty = BindableProperty.Create(nameof(Opis), typeof(string), typeof(KontrolkaEx));

        public string Opis
        {
            get
            {
                return (string)GetValue(OpisProperty);
            }
            set
            {
                SetValue(OpisProperty, value);
            }
        }


        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(KontrolkaEx));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }


        public static readonly BindableProperty ParamProperty = BindableProperty.Create(nameof(Param), typeof(int), typeof(KontrolkaEx));

        public int Param
        {
            get
            {
                return (int)GetValue(ParamProperty);
            }
            set
            {
                SetValue(ParamProperty, value);
            }
        }



    }
}
