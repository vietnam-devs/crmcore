using CRMCore.Module.Schema.Entity;
using System;
using System.Collections.Generic;
using SchemaModel = CRMCore.Framework.Entities.Schema;

namespace CRMCore.Module.Schema.Infrastructure
{
    public static class InMemoryDatabase
    {
        static InMemoryDatabase()
        {
            // Contact
            var contactSchema = new SchemaModel.Schema("Contact");

            contactSchema.Publish();

            contactSchema.Update(new SchemaModel.SchemaProperties
            {
                Label = "Contact",
                Hints = "Contact entity in CRM-Core."
            });

            contactSchema.AddField(new SchemaModel.StringField(
                Guid.NewGuid(),
                "Firstname",
                new SchemaModel.StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = SchemaModel.StringFieldType.Input,
                    Hints = "Please input FirstName.",
                    Placeholder = "Input FirstName...",
                    DefaultValue = "Phuong"
                }));

            contactSchema.AddField(new SchemaModel.StringField(
                Guid.NewGuid(), 
                "Lastname",
                new SchemaModel.StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = SchemaModel.StringFieldType.Input,
                    Hints = "Please input LastName.",
                    Placeholder = "Input LastName...",
                    DefaultValue = "Le"
                }));

            var contact = new Morphism("Contact", contactSchema);

            // Customer
            var customerSchema = new SchemaModel.Schema("Customer");

            customerSchema.Publish();

            customerSchema.Update(new SchemaModel.SchemaProperties
            {
                Label = "Customer",
                Hints = "Customer entity in CRM-Core."
            });

            customerSchema.AddField(new SchemaModel.StringField(
                Guid.NewGuid(),
                "Firstname",
                new SchemaModel.StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = SchemaModel.StringFieldType.Input,
                    Hints = "Please input FirstName.",
                    Placeholder = "Input FirstName...",
                    DefaultValue = "Lena"
                }));

            customerSchema.AddField(new SchemaModel.StringField(
                Guid.NewGuid(),
                "Lastname",
                new SchemaModel.StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = SchemaModel.StringFieldType.Input,
                    Hints = "Please input LastName.",
                    Placeholder = "Input LastName...",
                    DefaultValue = "Cao"
                }));

            var customer = new Morphism("Customer", customerSchema);

            // init
            SchemaItems.Add(Guid.NewGuid(), contact);
            SchemaItems.Add(Guid.NewGuid(), customer);
        }

        public static readonly Dictionary<Guid, Morphism> SchemaItems = new Dictionary<Guid, Morphism>();
    }
}
