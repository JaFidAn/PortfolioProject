using FluentMigrator;

namespace Persistence.Migrations;

[Migration(20250310150001)]
public class _20250310150001_CreateProjectTechnologyTable : Migration
{
    public override void Up()
    {
        Create.Table("ProjectTechnology")
            .WithColumn("ProjectId").AsGuid().NotNullable()
            .WithColumn("TechnologyId").AsGuid().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);

        // Foreign Keys
        Create.ForeignKey("FK_ProjectTechnology_Project")
            .FromTable("ProjectTechnology").ForeignColumn("ProjectId")
            .ToTable("Projects").PrimaryColumn("Id");

        Create.ForeignKey("FK_ProjectTechnology_Technology")
            .FromTable("ProjectTechnology").ForeignColumn("TechnologyId")
            .ToTable("Technologies").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("ProjectTechnology");
    }
}
