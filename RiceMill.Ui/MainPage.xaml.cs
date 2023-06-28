using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;

namespace RiceMill.Ui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IConcernQueries _concernQuery;
        private readonly IConcernCommands _concernCommands;

        public MainPage(/*IConcernQueries concernQueries, IConcernCommands concernCommands*/)
        {
            InitializeComponent();
            //_concernQuery = concernQueries;
            //_concernCommands = concernCommands;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

            var list = _concernCommands.CreateAsync(new DtoCreateConcern("تست"));

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}