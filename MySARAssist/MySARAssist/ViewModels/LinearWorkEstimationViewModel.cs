﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    class LinearWorkEstimationViewModel : BaseViewModel
    {
        public LinearWorkEstimationViewModel()
        {
            CalculateCommand = new Command(() =>
            {
                CalculateEstimate();

            });

            EraseCommand = new Command(() =>
            {
                Length = 0;
                SearcherSpeed = 1.6;
                estimatedDuration = 0;
            });

            SpeedUpCommand = new Command(() =>
            {
                SearcherSpeed += 0.1;
                OnPropertyChanged(nameof(SearcherSpeed));
                
            });
            SpeedDownCommand = new Command(() =>
            {
                SearcherSpeed -= 0.1;
                OnPropertyChanged(nameof(SearcherSpeed));
            });


            LengthUpCommand = new Command(() =>
            {
                Length += 0.1;
                OnPropertyChanged(nameof(Length));
            });
            LengthDownCommand = new Command(() =>
            {
                Length -= 0.1;
                OnPropertyChanged(nameof(Length));
            });

        }

        public Command CalculateCommand { get; }
        public Command EraseCommand { get; }
        public Command SpeedUpCommand { get; }
        public Command SpeedDownCommand { get; }
        public Command LengthUpCommand { get; }
        public Command LengthDownCommand { get; }


        private void CalculateEstimate()
        {
            if (Length > 0 && SearcherSpeed > 0)
            {

                estimatedDuration = Length / SearcherSpeed;
            }
            else { estimatedDuration = 0; }

            OnPropertyChanged(nameof(EstimatedDuration));
            OnPropertyChanged(nameof(EstimatedDurationWithRoundTrip));
        }

        double estimatedDuration = 0;
        public string EstimatedDuration
        {
            get => string.Format("{0:#,##0.0}", estimatedDuration);
        }
        public string EstimatedDurationWithRoundTrip
        {
            get => string.Format("{0:#,##0.0}", estimatedDuration * 2);
        }


        double _searcherSpeed = 1.6;
        public double SearcherSpeed { get => _searcherSpeed; set { _searcherSpeed = value; CalculateEstimate(); OnPropertyChanged(nameof(SearcherSpeed)); OnPropertyChanged(nameof(SearcherSpeedStr)); } }
        public string SearcherSpeedStr
        {
            get { if (SearcherSpeed > 0) { return SearcherSpeed.ToString(); } else { return null; } }
            set { if (!string.IsNullOrEmpty(value)) { double temp; double.TryParse(value, out temp); SearcherSpeed = temp; } }
        }
        double _length = 0;
        public double Length { get => _length; set { _length = value; CalculateEstimate(); OnPropertyChanged(nameof(Length)); OnPropertyChanged(nameof(LengthStr)); } }
        public string LengthStr
        {
            get { if (Length > 0) { return Length.ToString(); } else { return null; } }
            set { if (!string.IsNullOrEmpty(value)) { double temp; double.TryParse(value, out temp); Length = temp;  } }
        }
    }
}
