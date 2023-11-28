namespace MusicPlayerApp.Views;

public partial class AlbumView
{
    private readonly AlbumViewModel viewModel;

    public AlbumView(AlbumViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;

		BindingContext = viewModel;
	}
}