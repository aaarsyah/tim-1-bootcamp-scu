using Microsoft.AspNetCore.Components;

namespace MyApp.BlazorUI.Components
{
    public class NavigationManagerExt
    {
        private readonly NavigationManager _navigation;

        public NavigationManagerExt(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public void GoToLogin()
        {
            _navigation.NavigateTo("/login");
        }

        public void GoToRegister()
        {
            _navigation.NavigateTo("/register");
        }

        public void GoToHome()
        {
            _navigation.NavigateTo("/");
        }

        public void GoToForgotPass()
        {
            _navigation.NavigateTo("/forgot-password");
        }

        public void GoToNewPass()
        {
            _navigation.NavigateTo("/new-password");
        }

        public void GoToEmailConfirmSuccess()
        {
            _navigation.NavigateTo("/email-confirm");
        }

        public void GoToKelasKu()
        {
            _navigation.NavigateTo("/kelasku");
        }

        public void GoToPembelian()
        {
            _navigation.NavigateTo("/pembelian");
        }

        public void GoToDetail()
        {
            _navigation.NavigateTo("/detail");
        }
        public void GoToListMenu()
        {
            _navigation.NavigateTo("/listmenu");
        }

        // Contoh jika menggunakan id
        // public void GoToDetail(int id)
        // {
        //     _navigation.NavigateTo($"/detail/{id}");
        // }
    }
}
