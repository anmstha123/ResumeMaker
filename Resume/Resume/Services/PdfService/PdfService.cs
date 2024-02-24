using HarfBuzzSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Resume.Models;
using System.Reflection;

namespace Resume.Services.PdfService
{
    public class PdfService: IPdfService
    {
        public int topicPadding;
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            topicPadding = 10;
        }
        public FileStreamResult GetPDF(ResumeData resumeData)
        {
            Document document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);

                    page.Content().Element((context) => ComposeContent(context, resumeData));
                });
            });

            byte[] pdfBytes = document.GeneratePdf();
            var ms = new MemoryStream(pdfBytes);
            return new FileStreamResult(ms, "application/pdf");
        }

        void ComposeContent(IContainer container, ResumeData resumeData)
        {
            var titleStyle = TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Blue.Medium);
            var contentStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Black);
            var jobExperience = new List<string>
            {
                "Developed and maintained web applications using ASP.NET Core and MVC framework.",
                "Designed and implemented user-friendly interfaces with Angular and Vue.js.",
                "Utilized various technologies such as HTML, CSS, JavaScript, Bootstrap, and jQuery to create responsive web designs.",
                "Successfully worked on multiple projects simultaneously, ensuring timely completion of all deliverables."
            };


            var jobExperienceSecond = new List<string>
            {
                "Integrated third-party APIs and services into web applications using ASP.NET Core to enhance functionality and improve user experience.",
                "Created a dashboard to show different types of savings like time, cost, and fuel savings for trucks using an application that tracked those metrics.",
                "Worked on maintaining and adding features to a WordPress application.",
                "Participated in code reviews, testing, and debugging to ensure high-quality code and optimal performance.",
                "Conducted regular project status meetings and provided regular updates to stakeholders on project progress and milestones."
            };

            var firstProject = new List<string>
            {
                 "Coordinated on a team of three to develop an inventory management application for a convenience store.",
                "Designed and implemented the Login and Sign-up functionality.",
                "Helped with implementing the CRUD functionality and integrating the Firebase database."
            };

            var secondProject = new List<string>
            {
                "Made an application for the School of Social Works at UTA to facilitate student placements.",
                "Designed and implemented the homepage for three different users (Administrators, Students, and Agencies).",
                "Established relationships between Administrators, Students, and Agencies.",
                "Helped with writing the Rest APIs relating to user information."
            };



            container.Column(column =>
            {
                column.Spacing(5);

                //Header Section
                column.Item().Element((context)=>ComposeHeaderTop(context, resumeData));
                column.Item().Element(ComposeHeaderBottom);

                //Content Section
                column.Item().PaddingTop(topicPadding).Text("Professional Experience").Style(titleStyle);
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);
                column.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Column(column =>
                    {
                        column.Item().AlignLeft().Text("Ayoka Systems").Style(TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Black));
                    });
                    row.RelativeItem().AlignRight().Column(column =>
                    {
                        column.Item().AlignRight().Text("May 2022 - August 2023").Style(contentStyle);
                    });
                });
                column.Item().Text("Web Application Developer (Intern)").Style(contentStyle);
                column.Item().Element((context) => ComposeContentExperience(context, jobExperience));
                column.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Column(column =>
                    {
                        column.Item().AlignLeft().Text("Ayoka Systems").Style(TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Black));
                    });
                    row.RelativeItem().AlignRight().Column(column =>
                    {
                        column.Item().AlignRight().Text("August 2023 - Current").Style(contentStyle);
                    });
                });
                column.Item().Text("Web Application Developer").Style(contentStyle);
                column.Item().Element((context) => ComposeContentExperience(context, jobExperienceSecond));

                column.Item().PaddingTop(topicPadding).Text("Education").Style(titleStyle);
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                column.Item().Element((context) => ComposeContentEducation(context, resumeData));

                column.Item().PaddingTop(topicPadding).Text("Key Skills").Style(titleStyle);
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);
                column.Item().Element(ComposeContentSkills);


                column.Item().PaddingTop(topicPadding).Text("Academic Projects").Style(titleStyle);
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);
                column.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Column(column =>
                    {
                        column.Item().AlignLeft().Text("PICASSO").Style(TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Black));
                    });
                });
                column.Item().Text("[HTML, JavaScript, CSS, Firebase]").Style(contentStyle);
                column.Item().Element((context) => ComposeContentExperience(context, firstProject));
                column.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Column(column =>
                    {
                        column.Item().AlignLeft().Text("SMARTPLACEMENT").Style(TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Black));
                    });
                });
                column.Item().Text("[ReactJS, NodeJS, ExpressJS, Bootstrap, Azure SQL Database]").Style(contentStyle);
                column.Item().Element((context) => ComposeContentExperience(context, secondProject));
            });


        }

        void ComposeHeaderTop(IContainer container, ResumeData resumeData)
        {
            var titleStyle = TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Blue.Medium);
            var contentStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Black);
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(resumeData.FullName).Style(titleStyle);
                    column.Item().Text("Software Developer").Style(TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Black));
                    column.Item().Text(resumeData.Address).Style(contentStyle);
                    column.Item().Text(resumeData.Phone).Style(contentStyle);
                });

                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().AlignRight().Text("").Style(contentStyle);
                    column.Item().AlignRight().Text("").Style(contentStyle);
                    column.Item().AlignRight().Text(resumeData.Email).Style(contentStyle);
                    column.Item().AlignRight().Text("Linkedin:").Style(contentStyle);
                    column.Item().AlignRight().Text("http://www.linkedin.com/in/anmstha123/").Style(contentStyle);
                });
            });
        }
        void ComposeHeaderBottom(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Blue.Medium);
            var contentStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Black);
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Versatile Web Application Developer with over 1 years of experience in full stack development. " +
                        "Acquainted\r\nwith dominant programming languages, protocols and platforms needed to launch successful web\r\napplications. " +
                        "Adept at creating software that simultaneously pleases users, marketers and security personnel.").Style(TextStyle.Default.FontSize(10).SemiBold().FontColor(Colors.Black));
                });
            });
        }

        void ComposeContentExperience(IContainer container, List<string> jobExperience)
        {
            var contentStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Black);

            container.Row(row =>
            {
                row.RelativeItem(3).Column(column =>
                {

                    foreach (var i in jobExperience)
                    {
                        column.Item().Text($". {i}").Style(contentStyle);
                    }
                });
            });

        }
        void ComposeContentEducation(IContainer container, ResumeData resumeData)
        {
            var titleStyle = TextStyle.Default.FontSize(11).SemiBold().FontColor(Colors.Blue.Medium);
            var contentStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Black);
            var EducationList = new List<string>();
            EducationList.Add("Programming: C, C++, Java, C#, Python, JavaScript");
            EducationList.Add("Computer Science: Object - Oriented Programming, Datastructureand Algorithm, Programming languages,\r\nArtificial Intelligence, Database Management, Computer Networks, Information Security.");
        
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(resumeData.Education[0].Degree + " in "+ resumeData.Education[0].FieldOfStudy).Style(contentStyle);
                    column.Item().Text(resumeData.Education[0].Institution + " " + resumeData.Education[0].Year).Style(contentStyle);

                    foreach (var i in EducationList)
                    {
                        column.Item().Text($".{i}").Style(contentStyle);

                    }
                });
            });
        }
        void ComposeContentSkills(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(12).SemiBold().FontColor(Colors.Blue.Medium);
            var contentStyle = TextStyle.Default.FontSize(10).FontColor(Colors.Black);
            var techStack = new List<string>
            {
                "ASP.NET",
                "Angular",
                "React",
                "Rest API",
                "C#",
                "JavaScript",
                "TypeScript",
                "SQL",
                "JAVA",
                "Python",
                "AWS",
                "Azure"
            };

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {

                    foreach (var i in techStack)
                    {
                        column.Item().Row(row =>
                        {
                            //row.Spacing(5);
                            row.AutoItem().Text($".{i}");
                        });
                    }
                });
            });
        }


    }
}
