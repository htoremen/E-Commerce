using Domain.Entities;
using Persistence.Persistence;

namespace Migration.App.Seed
{
    public static class ParamaterSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context == null)
                return;

            if (!context.ParameterTypes.Any())
            {
                var parameterTypeId = "4d4b3802-2128-44c8-ad98-47fe3000c100";
                var parameterType = context.ParameterTypes.Add(new ParameterType { Id = parameterTypeId, Name = "Email İzni"}).Entity;


                parameterType.Parameters.Add(new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c101", ParameterTypeId = parameterTypeId, Name = "Randevu Oluşturma Bildirimi", IsActive = true });
                parameterType.Parameters.Add(new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c102", ParameterTypeId = parameterTypeId, Name = "Randevu Hatırlatma Bildirimi", IsActive = true });
                parameterType.Parameters.Add(new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c103", ParameterTypeId = parameterTypeId, Name = "Randevu Tarihi Güncelleme Bildirimi", IsActive = true });
                parameterType.Parameters.Add(new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c104", ParameterTypeId = parameterTypeId, Name = "Randevu Durumu Güncelleme Bildirimi", IsActive = true });
                parameterType.Parameters.Add(new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c105", ParameterTypeId = parameterTypeId, Name = "Günlük Randevu Hatırlatma Bildirimi", IsActive = true });
                parameterType.Parameters.Add(new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c106", ParameterTypeId = parameterTypeId, Name = "Randevu Yorumu Bildirimi", IsActive = true });

                await context.SaveChangesAsync();
            }
        }
    }
}