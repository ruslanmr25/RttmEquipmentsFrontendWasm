using System;

namespace BlazorApp4.Services;

public class ToastrService
{
    public event Action<string, string>? OnShow;

    public void ShowSucess(string message)
    {
        OnShow?.Invoke("success", message);
    }

    public void ShowError(string message)
    {
        OnShow?.Invoke("error", message);
    }

    public void ShowInfo(string message)
    {
        OnShow?.Invoke("info", message);
    }

    public void ShowWarning(string message)
    {
        OnShow?.Invoke("warning", message);
    }
}
