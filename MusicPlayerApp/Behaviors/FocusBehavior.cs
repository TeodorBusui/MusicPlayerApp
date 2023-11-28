using System.Runtime.CompilerServices;

namespace MusicPlayerApp.Behaviors
{
    public class FocusBehavior : Behavior<View>
    {
        public static BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(FocusBehavior));

        private View currentView;

        public bool IsFocused
        {
            get => (bool)GetValue(IsFocusedProperty);
            set => SetValue(IsFocusedProperty, value);
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            currentView = bindable;

            currentView.Focused += CurrentView_FocusChanged;
            currentView.Unfocused += CurrentView_FocusChanged;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);

            currentView = null;

            currentView.Focused -= CurrentView_FocusChanged;
            currentView.Unfocused -= CurrentView_FocusChanged;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == nameof(IsFocused) && IsFocused == true && currentView != null) 
            { 
                currentView.Focus();
            }
        }

        private void CurrentView_FocusChanged(object sender, FocusEventArgs e)
        {
            IsFocused = e.IsFocused;
        }
    }
}
