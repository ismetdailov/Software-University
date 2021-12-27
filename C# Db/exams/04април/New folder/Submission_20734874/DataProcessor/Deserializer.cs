namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var xmlSerializer = new XmlSerializer(typeof(IEnumerable<ImportProjects>), new XmlRootAttribute("Projects"));
            var projects = (IEnumerable<ImportProjects>)xmlSerializer.Deserialize(new StringReader(xmlString));

            foreach (var xmlProject in projects)
            {
                if (!IsValid(xmlProject))
                {
                    return "Invalid Data!";
                }
                var project = new Project
                {
                    Name = xmlProject.Name,
                    OpenDate = xmlProject.OpenDate,
                    DueDate = xmlProject.DueDate,
                };
                foreach (var xmlTask in xmlProject.Tasks)
                {
                    var tag = new Task
                    {
                        Name = xmlTask.Name,
                        OpenDate = xmlTask.OpenDate,
                        DueDate = xmlTask.DueDate,
                        ExecutionType = xmlTask.ExecutionType,
                        LabelType = xmlTask.LabelType

                    };
                    project.Tasks.Add(tag);
                }
                context.Projects.Add(project);
                context.SaveChanges();
                sb.AppendLine($"Successfully imported project - {project.Name} with {project.Tasks.Count} tasks.");
            }
            return "TODO";

        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            return "TODO";

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}