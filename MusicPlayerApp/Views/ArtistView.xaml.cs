using CommunityToolkit.Maui.Views;

namespace MusicPlayerApp.Views;

public partial class ArtistView
{
    private readonly ArtistViewModel viewModel;

    public ArtistView(ArtistViewModel viewModel)
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