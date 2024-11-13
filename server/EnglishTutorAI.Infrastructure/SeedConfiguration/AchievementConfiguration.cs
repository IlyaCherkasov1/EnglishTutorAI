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
                Id = Guid.Parse("684F3EFB-BF12-42AA-8FF4-17705F81D447"),
                Name = "achievements.flawlessTranslator.name",
                Description = "achievements.flawlessTranslator.description",
                IsCompleted = false,
                IconFileName = "novice_translator_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("B4631AAF-F4E1-419F-A073-8DA3B86FB6B5"),
                Name = "achievements.dedicatedTranslator.name",
                Description = "achievements.dedicatedTranslator.description",
                IsCompleted = false,
                IconFileName = "dedicated_translator_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("34FF3DDA-DCFA-4F35-BB1B-0B13344CBF70"),
                Name = "achievements.flawlessTranslator.name",
                Description = "achievements.flawlessTranslator.description",
                IsCompleted = false,
                IconFileName = "flawless_translator_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"),
                Name = "achievements.perfectPassage.name",
                Description = "achievements.perfectPassage.description",
                IsCompleted = false,
                IconFileName = "perfect_passage_icon.png",
            }
        );
    }
}