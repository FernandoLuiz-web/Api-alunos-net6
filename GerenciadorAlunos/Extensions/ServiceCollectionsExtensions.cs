namespace GerenciadorAlunos.Extensions;

public static class ServiceCollectionsExtensions
{

    public static string CorsCreation(this WebApplicationBuilder builder)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("https://localhost:7173"
                                      ).AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                              });
        });

        return MyAllowSpecificOrigins;
    }

}
