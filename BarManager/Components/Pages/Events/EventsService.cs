using Radzen.Blazor;
using Radzen;
using BarManager.Models;


namespace BarManager.Components.Pages.Events
{
    public class EventsService(DialogService dialogService)
    {
        public DialogService _dialogService = dialogService;
        public RadzenScheduler<EventItems> scheduler;

        public Dictionary<DateTime, string> events = [];
        public Month startMonth = Month.January;
        public List<EventItems> Events = [];

        public void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            // Highlight today in month view
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: var(--rz-scheduler-highlight-background-color, rgba(255,220,40,.2));";
            }

            // Draw a line for new year if start month is not January
            if ((args.View.Text == "Planner" || args.View.Text == "Timeline") && args.Start.Month == 12 && startMonth != Month.January)
            {
                args.Attributes["style"] = "border-bottom: thick double var(--rz-base-600);";
            }
        }

        public async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
            if (args.View.Text != "Year")
            {
                var data = await _dialogService.OpenAsync<AddEvents>("Add Appointment", new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End } });

                if (data != null)
                {
                    Events.Add(data);
                    // Either call the Reload method or reassign the Data property of the Scheduler
                    await scheduler.Reload();
                }
            }
        }

        public async Task OnMonthSelect(SchedulerMonthSelectEventArgs args) => await Task.CompletedTask;

        public async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<EventItems> args)
        {
            await _dialogService.OpenAsync<EditEvents>("Edit Appointment", new Dictionary<string, object> { { "EventItems", args.Data } });

            await scheduler.Reload();
        }

        public void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<EventItems> args)
        {
            // Never call StateHasChanged in AppointmentRender - would lead to infinite loop

            if (args.Data.Text == "Birthday")
            {
                args.Attributes["style"] = "background: red";
            }
        }
    }
}
