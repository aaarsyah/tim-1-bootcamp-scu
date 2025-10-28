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

        public void GoToProfile()
        {
            _navigation.NavigateTo("/profil");
        }

        public void GoToEditProfile()
        {
            _navigation.NavigateTo("/edit-profile");
        }

        public void GoToHome()
        {
            _navigation.NavigateTo("/");
        }

        /// <summary>
        /// Versi GoToHome() lain untuk force refresh (untuk update state ketika login/logout)
        /// </summary>
        public void GoToHome2()
        {
            _navigation.NavigateTo("/", true);
        }

        public void GoToForgotPass()
        {
            _navigation.NavigateTo("/forgot-password");
        }

        public void GoToNewPass()
        {
            _navigation.NavigateTo("/reset-password");
        }

        public void GoToEmailConfirmSuccess()
        {
            _navigation.NavigateTo("/confirm-email");
        }

        public void GoToSuccessPurchase()
        {
            _navigation.NavigateTo("/success-purchase");
        }

        public void GoToKelasKu()
        {
            _navigation.NavigateTo("/MyClass");
        }

        public void GoToCheckout()
        {
            _navigation.NavigateTo("/checkout");
        }

        // public void GoToDetail()
        // {
        //     _navigation.NavigateTo("/detail");
        // }

        // public void GoToListMenu()
        // {
        //     _navigation.NavigateTo("/listmenu");
        // }

        public void GoToInvoice()
        {
            _navigation.NavigateTo("/invoice");
        }

        public void GoToDetailsInvoice()
        {
            _navigation.NavigateTo("/details-invoice");
        }

        public void GoToDashboard()
        {
            _navigation.NavigateTo("/dashboard");
        }

        public void GoToUserManagement()
        {
            _navigation.NavigateTo("/user-management");
        }

        public void GoToCategoryManagement()
        {
            _navigation.NavigateTo("/category");
        }

        public void GoToCourseManagement()
        {
            _navigation.NavigateTo("/course");
        }

        public void GoToPaymentMethod()
        {
            _navigation.NavigateTo("/payment-method");
        }

        public void GoToPaymentMethodSelected(string selectedPayment)
        {
            _navigation.NavigateTo($"/success-purchase?method={selectedPayment}");
        }

        // Contoh jika menggunakan id
        // public void GoToDetail(int id)
        // {
        //     _navigation.NavigateTo($"/detail/{id}");
        // }

        public void GoToListMenu(int categoryId)
        {
            _navigation.NavigateTo($"/listmenu/{categoryId}");
        }

        public void GoToDetail(int courseId)
        {
            _navigation.NavigateTo($"/detail/{courseId}");
        }
    }
}

