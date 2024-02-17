namespace MusicPlayerApp.Views;

public partial class FavoritesView
{
    private readonly FavoritesViewModel viewModel;

    public FavoritesView(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;

        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.InitializeAsync();
    }
}