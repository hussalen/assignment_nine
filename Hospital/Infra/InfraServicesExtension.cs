using assignment_nine.Hospital.Application.Repos;
using assignment_nine.Hospital.Infra.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace assignment_nine.Hospital.Infra;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfraServices(this IServiceCollection app)
    {
        app.AddScoped<IAddPrescriptionRepo, AddPrescriptionRepo>();
        app.AddDbContext<HospitaldbContext>();
    }
}
