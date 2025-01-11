using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishTutorAI.Infrastructure.SeedConfiguration;

public class AchievementLevelConfiguration : IEntityTypeConfiguration<AchievementLevel>
{
    public void Configure(EntityTypeBuilder<AchievementLevel> builder)
    {
        builder.HasData(
            new AchievementLevel { Id = Guid.Parse("EE5E587D-1050-445D-BEE1-C0C74419C273"), AchievementId = AchievementIds.NoviceTranslatorId, Goal = 10 },
            new AchievementLevel { Id = Guid.Parse("4300546D-7EB5-461B-B800-DEF078028AE4"), AchievementId = AchievementIds.NoviceTranslatorId, Goal = 30},
            new AchievementLevel { Id = Guid.Parse("A4C81451-3AEC-43A4-990C-A70A0D1E2522"), AchievementId = AchievementIds.NoviceTranslatorId, Goal = 50},
            new AchievementLevel { Id = Guid.Parse("99645F7F-CEFE-4931-8170-81BB0322C667"), AchievementId = AchievementIds.NoviceTranslatorId, Goal = 100},
            new AchievementLevel { Id = Guid.Parse("87621B97-BD01-4DB1-B308-0547C9A09559"), AchievementId = AchievementIds.DedicatedTranslatorId, Goal = 5},
            new AchievementLevel { Id = Guid.Parse("C32F0B45-1C31-420C-AC47-A9C2692838C0"), AchievementId = AchievementIds.DedicatedTranslatorId, Goal = 10},
            new AchievementLevel { Id = Guid.Parse("9EBFD7F8-5309-4838-8DFD-64DC7F5741CA"), AchievementId = AchievementIds.DedicatedTranslatorId, Goal = 30},
            new AchievementLevel { Id = Guid.Parse("214D3C6F-2E12-47CB-923F-92B33F27F2C8"), AchievementId = AchievementIds.FlawlessTranslatorId, Goal = 10},
            new AchievementLevel { Id = Guid.Parse("AEB666EB-5192-4DD7-9D55-2E00DBAEA370"), AchievementId = AchievementIds.FlawlessTranslatorId, Goal = 30},
            new AchievementLevel { Id = Guid.Parse("72A62120-25E8-4A23-B639-694C73A1010C"), AchievementId = AchievementIds.FlawlessTranslatorId, Goal = 50});
    }
}