using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using razorweb.Models;

namespace razorweb.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.ID);
                });
                //InsertData
                //fake data : Bogus
                Randomizer.Seed = new Random(8675309);

                var fakeracticle = new Faker<Acticle>();
                fakeracticle.RuleFor(a=>a.Title, f =>f.Lorem.Sentence(5,5));
                fakeracticle.RuleFor(a=>a.PublishDate,f=>f.Date.Between(new DateTime(2021,1,1), new DateTime(2021,7,30)));
                fakeracticle.RuleFor(a=>a.Content,f => f.Lorem.Paragraphs(1,4));

                for(int i=0;i<=15;i++)
                {
                    Acticle acticle = fakeracticle.Generate();
                    migrationBuilder.InsertData(
                    table: "articles",
                    columns: new[]{"Title","PublishDate","Content"},
                    values: new object[]{
                        acticle.Title,
                        acticle.PublishDate,
                        acticle.Content
                    }
                );   
                }
               
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
