namespace MusicPlayerApp.Views;

public partial class NewReleasesView
{
    private readonly NewReleasesViewModel newReleasesViewModel;

    public NewReleasesView(NewReleasesViewModel newReleasesViewModel)
	{
		InitializeComponent();
        this.newReleasesViewModel = newReleasesViewModel;

        BindingContext = newReleasesViewModel;
    }
}