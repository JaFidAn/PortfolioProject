using FluentMigrator;

namespace Persistence.Migrations;

[Migration(20250306135801)]
public class _20250306135801_CreateProjectsTable : Migration
{
    public override void Up()
    {
        Create.Table("Projects")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Description").AsString(int.MaxValue).NotNullable()
            .WithColumn("Link").AsString(500).Nullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);
    }

    public override void Down()
    {
        Delete.Table("Projects");
    }
}
