#pragma checksum "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0306a895060796dea84ed885b16932cdb2f71b5e"
// <auto-generated/>
#pragma warning disable 1591
namespace EmployeeScheduler.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using EmployeeScheduler.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using EmployeeScheduler.Web.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/localstorage")]
    public partial class LocalStorage : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Local Storage Test</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<h3>Save Test</h3>\r\n");
            __builder.OpenElement(2, "p");
            __builder.AddMarkupContent(3, "\r\n    Key: ");
            __builder.OpenElement(4, "input");
            __builder.AddAttribute(5, "type", "text");
            __builder.AddAttribute(6, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 7 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          setKey

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => setKey = __value, setKey));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(8, "\r\n    Val: ");
            __builder.OpenElement(9, "input");
            __builder.AddAttribute(10, "type", "text");
            __builder.AddAttribute(11, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 8 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          setValue

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => setValue = __value, setValue));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n");
            __builder.CloseElement();
            __builder.AddMarkupContent(14, "\r\n");
            __builder.OpenElement(15, "button");
            __builder.AddAttribute(16, "class", "btn btn-primary");
            __builder.AddAttribute(17, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 10 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          SaveItem

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(18, "Save Item");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n<hr>\r\n\r\n");
            __builder.AddMarkupContent(20, "<h3>Load Test</h3>\r\n");
            __builder.OpenElement(21, "p");
            __builder.AddMarkupContent(22, "\r\n    Key: ");
            __builder.OpenElement(23, "input");
            __builder.AddAttribute(24, "type", "text");
            __builder.AddAttribute(25, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 15 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          loadKey

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(26, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => loadKey = __value, loadKey));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n");
#nullable restore
#line 16 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
     if (loadError)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(28, "        ");
            __builder.OpenElement(29, "span");
            __builder.AddAttribute(30, "style", "color: red;");
            __builder.AddContent(31, 
#nullable restore
#line 18 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                   loadMessage

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n");
#nullable restore
#line 19 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(33, "        ");
            __builder.OpenElement(34, "span");
            __builder.AddContent(35, 
#nullable restore
#line 22 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
               loadValue

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n");
#nullable restore
#line 23 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n");
            __builder.OpenElement(38, "button");
            __builder.AddAttribute(39, "class", "btn btn-primary");
            __builder.AddAttribute(40, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 25 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          LoadItem

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(41, "Load Item");
            __builder.CloseElement();
            __builder.AddMarkupContent(42, "\r\n<hr>\r\n\r\n");
            __builder.AddMarkupContent(43, "<h3>Remove Test</h3>\r\n");
            __builder.OpenElement(44, "p");
            __builder.AddMarkupContent(45, "\r\n    Key: ");
            __builder.OpenElement(46, "input");
            __builder.AddAttribute(47, "type", "text");
            __builder.AddAttribute(48, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 30 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          removeKey

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(49, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => removeKey = __value, removeKey));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n");
#nullable restore
#line 31 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
     if (removeError)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(51, "        ");
            __builder.AddMarkupContent(52, "<span style=\"color: red;\">Key does not exist!</span>\r\n");
#nullable restore
#line 34 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(53, "        ");
            __builder.OpenElement(54, "span");
            __builder.AddAttribute(55, "style", "color: green;");
            __builder.AddContent(56, 
#nullable restore
#line 37 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                     removeSuccessMessage

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(57, "\r\n");
#nullable restore
#line 38 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(58, "\r\n");
            __builder.OpenElement(59, "button");
            __builder.AddAttribute(60, "class", "btn btn-primary");
            __builder.AddAttribute(61, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 40 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
                                          RemoveItem

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(62, "Remove Item");
            __builder.CloseElement();
            __builder.AddMarkupContent(63, "\r\n<hr>");
        }
        #pragma warning restore 1998
#nullable restore
#line 45 "C:\Local Projects\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\LocalStorage.razor"
       
    private string setKey = "testKey";
    private string setValue = "testVal";

    private bool loadError = false;
    private string loadMessage = "";
    private string loadKey = "testKey";
    private string loadValue = "";

    private bool removeError = false;
    private string removeKey = "testKey";
    private string removeSuccessMessage = "";

    private async Task SaveItem()
    {
        await localStorage.SetItemAsync(setKey, setValue);
    }

    private async Task LoadItem()
    {
        if (!await localStorage.ContainKeyAsync(loadKey))
        {
            loadError = true;
            loadMessage = "Key does not exist!";
        }
        else
        {
            loadError = false;
            try
            {
                loadValue = await localStorage.GetItemAsync<string>(loadKey);
            }
            catch (Exception ex)
            {
                loadError = true;
                loadMessage = $"Error retrieving value:\n{ex.Message}";
            }
        }
    }

    private async Task RemoveItem()
    {
        if (!await localStorage.ContainKeyAsync(removeKey))
        {
            removeError = true;
        }
        else
        {
            removeError = false;
            await localStorage.RemoveItemAsync(removeKey);
            removeSuccessMessage = "Item removed successfully!";
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private EmployeeScheduler.Lib.Services.ISchedulingService schedulingService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
    }
}
#pragma warning restore 1591
