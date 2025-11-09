using Microsoft.AspNetCore.Mvc;

namespace MyApp.BlazorUI.Services;
public class ErrorService
{
    public ErrorService() { }
    public async Task<string?> GetErrorAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return null;
        }
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.BadRequest: //400
                try
                {
                    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    if (problemDetails == null) throw new ArgumentNullException();
                    var error = problemDetails.Extensions["errorCode"]?.ToString();
                    if (error == "VALIDATION_ERROR") //Error yang dihandle pada endpoint
                    {
                        return problemDetails.Title;
                    }
                    if (problemDetails.Title == null) throw new ArgumentNullException();
                    if (problemDetails.Title == "One or more validation errors occurred.") //Disebabkan dari DTO validation error
                    {
                        return string.Join(",", problemDetails.Extensions["errors"]);
                    }
                    return problemDetails.Title;
                }
                catch (ArgumentNullException)
                {
                    return "Generic Bad Request error";
                }
            case System.Net.HttpStatusCode.Unauthorized: //401
                try
                {
                    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    if (problemDetails == null) throw new ArgumentNullException();
                    var error = problemDetails.Extensions["errorCode"]?.ToString();
                    if (error == "INVALID_TOKEN") //Error yang dihandle pada endpoint
                    {
                        return problemDetails.Title;
                    }
                    if (problemDetails.Title == null) throw new ArgumentNullException(); //Disebabkan dari AuthorizeAttribute

                    return problemDetails.Title;
                }
                catch (ArgumentNullException)
                {
                    return "You are not logged in";
                }
            case System.Net.HttpStatusCode.Forbidden: //403
                try
                {
                    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    if (problemDetails == null) throw new ArgumentNullException();
                    var error = problemDetails.Extensions["errorCode"]?.ToString();
                    if (error == "ACCOUNT_LOCKED") //Error yang dihandle pada endpoint
                    {
                        return problemDetails.Title;
                    }
                    if (error == "ACCOUNT_INACTIVE") //Error yang dihandle pada endpoint
                    {
                        return problemDetails.Title;
                    }
                    if (problemDetails.Title == null) throw new ArgumentNullException(); //Disebabkan dari AuthorizeAttribute yang require admin role
                    return problemDetails.Title;
                }
                catch (ArgumentNullException)
                {
                    return "You are not allowed to access this page";
                }
            case System.Net.HttpStatusCode.NotFound: //404
                try
                {
                    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    if (problemDetails == null) throw new ArgumentNullException();
                    var error = problemDetails.Extensions["errorCode"]?.ToString();
                    if (error == "NOT_FOUND") //Error yang dihandle pada endpoint
                    {
                        return problemDetails.Title;
                    }
                    if (problemDetails.Title == null) throw new ArgumentNullException();
                    return problemDetails.Title;
                }
                catch (ArgumentNullException)
                {
                    return "This resource does not exist";
                }
            case System.Net.HttpStatusCode.InternalServerError: //500
                try
                {
                    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    if (problemDetails == null) throw new ArgumentNullException();
                    if (problemDetails.Title == null) throw new ArgumentNullException();
                    return problemDetails.Title;
                }
                catch (ArgumentNullException)
                {
                    return "Internal server error";
                }
            default:
                return "Error";
        }
    }
}
