using Framework.Core.Enums;
using LookUp.Domain.Entities;

namespace LookUp.Test.MockData
{
    public class AttributeMockData
    {
        public static List<AttributesEntity> GetAttributes()
        {
            return new List<AttributesEntity>
            {
                new AttributesEntity()
                {
                    Id = new Guid("CBB7C8F1-BC5B-47A9-8957-BF08E013447F"),
                    Type = AttributeTypeEnum.Number,
                    Unit = "Pieces",
                    Status = RecordStatusEnum.Active
                },
                new AttributesEntity()
                {
                    Id = new Guid("5CA86A79-56FD-4D7D-ADD4-65E57C185E0E"),
                    Type = AttributeTypeEnum.Number,
                    Unit = "Package",
                    Status = RecordStatusEnum.Inactive
                },
            };
        }
    }
}
