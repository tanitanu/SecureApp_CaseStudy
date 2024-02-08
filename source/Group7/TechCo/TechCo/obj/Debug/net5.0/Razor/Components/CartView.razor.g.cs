#pragma checksum "C:\RJCode\TechCo\TechCo\Components\CartView.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6f29e8e35991d4d16346ca612f5f5aeb51f42806"
// <auto-generated/>
#pragma warning disable 1591
namespace TechCo.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using TechCo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using TechCo.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\RJCode\TechCo\TechCo\_Imports.razor"
using TechCo.Models;

#line default
#line hidden
#nullable disable
    public partial class CartView : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "p-3");
            __builder.AddMarkupContent(2, "<h3>Your Cart</h3>\n    <hr class=\"m-2\">");
#nullable restore
#line 8 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
     if (cartitems is not null)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "p-2");
#nullable restore
#line 11 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
             foreach (var item in cartitems)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "row no-gutters mb-3");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "col-2");
            __builder.OpenElement(9, "img");
            __builder.AddAttribute(10, "src", "/image/" + (
#nullable restore
#line 15 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
                                          item.Thumbnail

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "class", "w-100");
            __builder.AddAttribute(12, "alt", 
#nullable restore
#line 15 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
                                                                              item.ProductName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\n                    ");
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "class", "col-9");
            __builder.OpenElement(16, "div");
            __builder.AddAttribute(17, "class", "card-body");
            __builder.OpenElement(18, "h5");
            __builder.AddAttribute(19, "class", "card-title");
            __builder.AddContent(20, 
#nullable restore
#line 19 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
                                                    item.ProductName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\n                    ");
            __builder.OpenElement(22, "div");
            __builder.AddAttribute(23, "class", "col-1");
            __builder.OpenElement(24, "button");
            __builder.AddAttribute(25, "class", "btn btn-danger my-auto");
            __builder.AddAttribute(26, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 23 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
                                                                         ()=>Deleteitem(item.ID)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(27, "<i class=\"fas fa-times\"></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 26 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 28 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
     if (isorderd)
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(28, "<div class=\"alert alert-primary\" role=\"alert\">\n            Your Order is Sent Go To The Orders Page To Pay For It And Get It Delivered To You\n        </div>");
#nullable restore
#line 34 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(29, "<hr class=\"m-2\">\n    ");
            __builder.OpenElement(30, "button");
            __builder.AddAttribute(31, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 36 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
                      ()=>CreateAnOrder()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(32, "class", "btn btn-primary");
            __builder.AddContent(33, "Order");
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 38 "C:\RJCode\TechCo\TechCo\Components\CartView.razor"
       
    [Parameter]
    public string UserName { get; set; }

    [Parameter]
    public string RedirectURL { get; set; }

    List<Guid> items;
    public List<Product> cartitems = new List<Product>();
    bool isorderd = false;

    protected override async Task OnInitializedAsync()
    {
        var itemsarr = await storage.GetAsync<Guid[]>("cartitems");
        if (itemsarr.Success && itemsarr.Value is not null)
        {
            items = itemsarr.Value.ToList();
            foreach (var item in items)
            {
                if (cartitems.Where(p => p.ID == item).Count() == 0)
                    cartitems.Add(context.Product.Find(item));
            }
        }
        else
        {
            items = new List<Guid>();
        }
    }

    private async void Deleteitem(Guid itemid)
    {
        items.Remove(itemid);
        cartitems.Remove(cartitems.Where(p => p.ID == itemid).FirstOrDefault());
        await storage.SetAsync("cartitems", items);
        await OnInitializedAsync();
    }

    private async void CreateAnOrder()
    {
        if (items.Count() > 0)
        {
            var a = context.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            var order = new Order()
            {
                ApplicationUserID = a.Id,
                OrderDate = DateTime.Now,
                OrderProducts = cartitems,
            };
            context.Add(order);
            await storage.SetAsync("cartitems", null);
            items = new List<Guid>() { };
            cartitems = new List<Product>() { };
            isorderd = true;
            await context.SaveChangesAsync();
            NavigationManager.NavigateTo(RedirectURL,forceLoad:true);
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ProtectedLocalStorage storage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ApplicationDbContext context { get; set; }
    }
}
#pragma warning restore 1591
