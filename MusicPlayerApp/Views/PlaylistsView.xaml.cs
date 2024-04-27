namespace MusicPlayerApp.Views;

public partial class PlaylistsView
{
    private readonly PlaylistsViewModel viewModel;

    public PlaylistsView(PlaylistsViewModel viewModel)
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

    private void OpenActions(object sender, System.EventArgs e)
    {
        ActionsFrame.IsVisible = !ActionsFrame.IsVisible;
    }
}