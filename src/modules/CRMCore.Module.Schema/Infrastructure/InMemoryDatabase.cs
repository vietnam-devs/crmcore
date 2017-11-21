using CRMCore.Module.Schema.Domain;
using System;
using System.Collections.Generic;

namespace CRMCore.Module.Schema.Infrastructure
{
    public static class InMemoryDatabase
    {
        static InMemoryDatabase()
        {
            // Contact schema
            var contactSchema = new Domain.Schema("Contact");

            contactSchema.Publish();

            contactSchema.Update(new SchemaProperties {
                Label = "Contact",
                Hints = "Contact entity in CRM-Core."
            });

            contactSchema.AddField(new StringField(
                Guid.NewGuid(),
                "Firstname",
                new StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input FirstName.",
                    Placeholder = "Input FirstName...",
                    DefaultValue = "Phuong"
                }));

            contactSchema.AddField(new StringField(
                Guid.NewGuid(), 
                "Lastname",
                new StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input LastName.",
                    Placeholder = "Input LastName...",
                    DefaultValue = "Le"
                }));

            // Customer schema
            var customerSchema = new Domain.Schema("Customer");

            customerSchema.Publish();

            customerSchema.Update(new SchemaProperties
            {
                Label = "Customer",
                Hints = "Customer entity in CRM-Core."
            });

            customerSchema.AddField(new StringField(
                Guid.NewGuid(),
                "Firstname",
                new StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input FirstName.",
                    Placeholder = "Input FirstName...",
                    DefaultValue = "Lena"
                }));

            customerSchema.AddField(new StringField(
                Guid.NewGuid(),
                "Lastname",
                new StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input LastName.",
                    Placeholder = "Input LastName...",
                    DefaultValue = "Cao"
                }));

            SchemaItems.Add(Guid.NewGuid(), contactSchema);
            SchemaItems.Add(Guid.NewGuid(), customerSchema);
        }

        public static readonly Dictionary<Guid, Domain.Schema> SchemaItems = new Dictionary<Guid, Domain.Schema>();
    }
}
