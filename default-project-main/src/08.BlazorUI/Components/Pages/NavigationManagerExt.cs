using Microsoft.AspNetCore.Components;

namespace MyApp.BlazorUI.Components;

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
        _navigation.NavigateTo("/profile");
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

    public void GoToInvoice()
    {
        _navigation.NavigateTo("/invoice");
    }

    public void GoToDetailsInvoice(Guid invoiceRefId)
    {
        _navigation.NavigateTo($"/details-invoice/{invoiceRefId}");
    }

    public void GoToDetailsInvoiceAdmin(Guid invoiceRefId)
    {
        _navigation.NavigateTo($"/details-invoice-admin/{invoiceRefId}");
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

    public void GoToListMenu(Guid categoryRefId)
    {
        _navigation.NavigateTo($"/listmenu/{categoryRefId}");
    }

    public void GoToDetail(Guid courseRefId)
    {
        _navigation.NavigateTo($"/detail/{courseRefId}");
    }
}

