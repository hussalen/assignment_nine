using assignment_nine.Hospital.Application.Services;
using assignment_nine.Hospital.Application.Services.Interfaces;

namespace assignment_nine.Hospital.Application;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection app)
    {
        app.AddScoped<IAddPrescriptionService, AddPrescriptionService>();
    }
}
