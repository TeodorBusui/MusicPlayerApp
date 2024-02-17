namespace MusicPlayerApp.Views;

public partial class TrackView
{
    private readonly TrackViewModel viewModel;

    public TrackView(TrackViewModel viewModel)
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