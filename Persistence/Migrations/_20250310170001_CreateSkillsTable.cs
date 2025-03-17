using System;
using FluentMigrator;

namespace Persistence.Migrations;

[Migration(20250310170001)]
public class _20250310170001_CreateSkillsTable : Migration
{
    public override void Up()
    {
        Create.Table("Skills")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Level").AsString(50).NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);
    }

    public override void Down()
    {
        Delete.Table("Skills");
    }
}
