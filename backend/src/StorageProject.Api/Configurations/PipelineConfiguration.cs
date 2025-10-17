namespace StorageProject.Api.Configurations
{
    public static class PipelineConfiguration
    {
        public static void AddPipelineConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("AllowSpecificOrigins");
                
            }

            app.UseCustomMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
