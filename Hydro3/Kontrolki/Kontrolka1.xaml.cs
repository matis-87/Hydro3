using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hydro3.Kontrolki
{

    public enum eKolorKontrolki
    {
        czerwony,
        zielony
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class Kontrolka1 : Frame
    {

        public static readonly BindableProperty StanProperty = BindableProperty.Create(nameof(Stan), typeof(bool), typeof(Kontrolka1));

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


        public static readonly BindableProperty KolorProperty = BindableProperty.Create(nameof(Kolor), typeof(eKolorKontrolki), typeof(Kontrolka1));

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


        public Kontrolka1()
        {
            InitializeComponent();
        }
    }
}