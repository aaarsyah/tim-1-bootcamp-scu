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

        public void GoToSuccessPurchase()
        {
            _navigation.NavigateTo("/success-purchase");
        }

        public void GoToKelasKu()
        {
            _navigation.NavigateTo("/myclass");
        }

        public void GoToCheckout()
        {
            _navigation.NavigateTo("/checkout");
        }

        public void GoToDetail()
        {
            _navigation.NavigateTo("/detail");
        }

        public void GoToListMenu()
        {
            _navigation.NavigateTo("/listmenu");
        }

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
    }
}
