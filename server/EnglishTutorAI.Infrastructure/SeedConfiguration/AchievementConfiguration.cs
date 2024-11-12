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
                Name = "Novice Translator",
                Description = "Translate sentences",
                IsCompleted = false,
                IconFileName = "novice_translator_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("B4631AAF-F4E1-419F-A073-8DA3B86FB6B5"),
                Name = "Dedicated Translator",
                Description = "Complete translates",
                IsCompleted = false,
                IconFileName = "dedicated_translator_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("34FF3DDA-DCFA-4F35-BB1B-0B13344CBF70"),
                Name = "Flawless Translator",
                Description = "Translate sentences without making any mistakes.",
                IsCompleted = false,
                IconFileName = "flawless_translator_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"),
                Name = "Perfect Passage",
                Description = "Complete entire translate without any mistakes.",
                IsCompleted = false,
                IconFileName = "perfect_passage_icon.png",
            },
            new Achievement
            {
                Id = Guid.Parse("E2665643-F566-4CBF-90B8-E85F0906D8BB"),
                Name = "Grammar Perfectionist",
                Description = "Correct grammar mistakes",
                IsCompleted = false,
                IconFileName = "grammar_perfectionist_icon.png",
            }
        );
    }
}