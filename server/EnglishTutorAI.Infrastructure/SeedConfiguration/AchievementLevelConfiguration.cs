using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishTutorAI.Infrastructure.SeedConfiguration;

public class AchievementLevelConfiguration : IEntityTypeConfiguration<AchievementLevel>
{
    public void Configure(EntityTypeBuilder<AchievementLevel> builder)
    {
        builder.HasData(
            new AchievementLevel { Id = Guid.Parse("EE5E587D-1050-445D-BEE1-C0C74419C273"), AchievementId = Guid.Parse("684F3EFB-BF12-42AA-8FF4-17705F81D447"), Goal = 5 },
            new AchievementLevel { Id = Guid.Parse("4300546D-7EB5-461B-B800-DEF078028AE4"), AchievementId = Guid.Parse("684F3EFB-BF12-42AA-8FF4-17705F81D447"), Goal = 10},
            new AchievementLevel { Id = Guid.Parse("A4C81451-3AEC-43A4-990C-A70A0D1E2522"), AchievementId = Guid.Parse("684F3EFB-BF12-42AA-8FF4-17705F81D447"), Goal = 30},
            new AchievementLevel { Id = Guid.Parse("87621B97-BD01-4DB1-B308-0547C9A09559"), AchievementId = Guid.Parse("B4631AAF-F4E1-419F-A073-8DA3B86FB6B5"), Goal = 7},
            new AchievementLevel { Id = Guid.Parse("C32F0B45-1C31-420C-AC47-A9C2692838C0"), AchievementId = Guid.Parse("B4631AAF-F4E1-419F-A073-8DA3B86FB6B5"), Goal = 30},
            new AchievementLevel { Id = Guid.Parse("9EBFD7F8-5309-4838-8DFD-64DC7F5741CA"), AchievementId = Guid.Parse("B4631AAF-F4E1-419F-A073-8DA3B86FB6B5"), Goal = 100},
            new AchievementLevel { Id = Guid.Parse("214D3C6F-2E12-47CB-923F-92B33F27F2C8"), AchievementId = Guid.Parse("34FF3DDA-DCFA-4F35-BB1B-0B13344CBF70"), Goal = 10},
            new AchievementLevel { Id = Guid.Parse("AEB666EB-5192-4DD7-9D55-2E00DBAEA370"), AchievementId = Guid.Parse("34FF3DDA-DCFA-4F35-BB1B-0B13344CBF70"), Goal = 30},
            new AchievementLevel { Id = Guid.Parse("72A62120-25E8-4A23-B639-694C73A1010C"), AchievementId = Guid.Parse("34FF3DDA-DCFA-4F35-BB1B-0B13344CBF70"), Goal = 100},
            new AchievementLevel { Id = Guid.Parse("321AB4E0-B331-4560-906E-46873E66DBAD"), AchievementId = Guid.Parse("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), Goal = 3},
            new AchievementLevel { Id = Guid.Parse("A56C2EB0-F02D-46DA-A1CB-892097B673B2"), AchievementId = Guid.Parse("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), Goal = 10},
            new AchievementLevel { Id = Guid.Parse("2443BA90-2D79-4645-8329-5E68D07DEA12"), AchievementId = Guid.Parse("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), Goal = 20});
    }
}