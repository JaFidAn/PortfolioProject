using FluentMigrator;

namespace Persistence.Migrations;

[Migration(20250310140001)]
public class _20250310140001_CreateTechnologiesTable : Migration
{
    public override void Up()
    {
        Create.Table("Technologies")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);
    }

    public override void Down()
    {
        Delete.Table("Technologies");
    }
}
