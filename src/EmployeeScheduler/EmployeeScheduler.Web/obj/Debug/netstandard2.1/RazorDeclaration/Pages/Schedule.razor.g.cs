#pragma checksum "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\Schedule.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2af6685a0700b54541891d12338b85c5d729930d"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace EmployeeScheduler.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using EmployeeScheduler.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\_Imports.razor"
using EmployeeScheduler.Web.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\Schedule.razor"
using Lib.Extensions;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/schedule")]
    public partial class Schedule : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 55 "C:\repos\EmployeeScheduler\src\EmployeeScheduler\EmployeeScheduler.Web\Pages\Schedule.razor"
       
    public Lib.DTO.ScheduleWeek Schedule_ { get; set; }
    public List<Lib.DTO.Employee> Employees { get; set; }
    public List<EmployeeWeek> EmployeeWeeks { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Schedule_ = await Scheduler.GetCurrentScheduleAsync();
        Employees = await Scheduler.GetEmployeesAsync(includeDeleted: false);

        EmployeeWeeks = Employees.Select(e => new EmployeeWeek
        {
            Employee = e,
            Sunday = Schedule_.Sunday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
            Monday = Schedule_.Monday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
            Tuesday = Schedule_.Tuesday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
            Wednesday = Schedule_.Wednesday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
            Thursday = Schedule_.Thursday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
            Friday = Schedule_.Friday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
            Saturday = Schedule_.Saturday?.SingleOrDefault(d => d.EmployeeID == e.ID) ?? new Lib.DTO.EmployeeSchedule(),
        }).ToList();

        // https://stackoverflow.com/questions/58123063/call-method-in-mainlayout-from-a-page-component-in-blazor
        // For how to refresh alerts. Actually, probably need Toast Alerts for this, so this may be moot.
    }

    public class EmployeeWeek
    {
        public Lib.DTO.Employee Employee { get; set; }
        public Lib.DTO.EmployeeSchedule Sunday { get; set; }
        public Lib.DTO.EmployeeSchedule Monday { get; set; }
        public Lib.DTO.EmployeeSchedule Tuesday { get; set; }
        public Lib.DTO.EmployeeSchedule Wednesday { get; set; }
        public Lib.DTO.EmployeeSchedule Thursday { get; set; }
        public Lib.DTO.EmployeeSchedule Friday { get; set; }
        public Lib.DTO.EmployeeSchedule Saturday { get; set; }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Lib.Services.ISchedulingService Scheduler { get; set; }
    }
}
#pragma warning restore 1591
