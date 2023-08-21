﻿using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;

namespace RiceMill.Ui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            var res = DisplayAlert("تاییدیه", "آیا از این درخواست اطمینان دارید؟", "بلی", FlowDirection.LeftToRight);
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}