using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hydro3.Kontrolki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KontrolkaEx : Grid
    {

        public static readonly BindableProperty StanProperty = BindableProperty.Create(nameof(Stan), typeof(bool), typeof(KontrolkaEx));

        public bool Stan
        {
            get
            {
                return (bool)GetValue(StanProperty);
            }
            set
            {
                SetValue(StanProperty, value);
            }
        }


        public static readonly BindableProperty KolorProperty = BindableProperty.Create(nameof(Kolor), typeof(eKolorKontrolki), typeof(KontrolkaEx));

        public eKolorKontrolki Kolor
        {
            get
            {
                return (eKolorKontrolki)GetValue(KolorProperty);
            }
            set
            {
                SetValue(KolorProperty, value);
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


        public KontrolkaEx()
        {
            InitializeComponent();
        }
    }
}