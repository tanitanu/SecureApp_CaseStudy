// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace TechCo.Components
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using TechCo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using TechCo.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\_Imports.razor"
using TechCo.Models;

#line default
#line hidden
#nullable disable
    public partial class CartView : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 38 "C:\Users\tmajumder2\Downloads\Tanisha_casestudy\source\Group7\TechCo\TechCo\Components\CartView.razor"
       
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