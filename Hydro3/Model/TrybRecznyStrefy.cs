using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hydro3.Model
{
  public  class TrybRecznyStrefy : BindableBase
    {
        private String _stan;
        public String Stan
        {
            get { return _stan; }
            set { SetProperty(ref _stan, value); }
        }
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (value)
                    Stan = "Stop";
                else
                    Stan = "Start";
                NonChangable = !value;
                SetProperty(ref _isActive, value); }
        }

        private bool _nonChangable;
        public bool NonChangable
        {
            get { return _nonChangable; }
            set
            {
                SetProperty(ref _nonChangable, value);
            }
        }

        private int _wateringTime;
        public int WateringTime
        {
            get { return _wateringTime; }
            set { SetProperty(ref _wateringTime, value); }
        }

        private int _remaining;
        public int Remaining
        {
            get { return _remaining; }
            set
            {
                if (WateringTime > 0)
                {
                    float wt = WateringTime;
                    float val = value;
                    Progress = (wt - val) / wt;
                }
                else
                    Progress = 0;
                SetProperty(ref _remaining, value);
            }
        }

        private float _progress;
        public float Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }
    }
}
