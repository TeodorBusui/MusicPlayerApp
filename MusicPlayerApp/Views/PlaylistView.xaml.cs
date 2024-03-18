namespace MusicPlayerApp.Views;

public partial class PlaylistView
{
    private readonly PlaylistViewModel viewModel;

    public PlaylistView(PlaylistViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;

        BindingContext = viewModel;
    }

    private void OpenActions(object sender, System.EventArgs e)
    {
        ActionsFrame.IsVisible = !ActionsFrame.IsVisible;
    }
}