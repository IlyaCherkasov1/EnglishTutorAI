using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishTutorAI.Infrastructure.SeedConfiguration;

public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.HasData(
            new Achievement
            {
                Id = AchievementIds.NoviceTranslatorId,
                Name = "achievements.noviceTranslator.name",
                Description = "achievements.noviceTranslator.description",
                IconFileName = "novice_translator_icon.png",
            },
            new Achievement
            {
                Id = AchievementIds.DedicatedTranslatorId,
                Name = "achievements.dedicatedTranslator.name",
                Description = "achievements.dedicatedTranslator.description",
                IconFileName = "dedicated_translator_icon.png",
            },
            new Achievement
            {
                Id = AchievementIds.FlawlessTranslatorId,
                Name = "achievements.flawlessTranslator.name",
                Description = "achievements.flawlessTranslator.description",
                IconFileName = "flawless_translator_icon.png",
            }
        );
    }
}