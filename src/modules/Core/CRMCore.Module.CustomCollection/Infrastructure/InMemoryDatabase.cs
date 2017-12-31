using CRMCore.Module.CustomCollection.Entity;
using CRMCore.Module.CustomCollection.Entity.Schema;
using System;
using System.Collections.Generic;
using SchemaModel = CRMCore.Module.CustomCollection.Entity.Schema;

namespace CRMCore.Module.CustomCollection.Infrastructure
{
    public static class InMemoryDatabase
    {
        static InMemoryDatabase()
        {
            // Contact
            var contactSchema = new SchemaModel.Schema("contact");

            contactSchema.Publish();

            contactSchema.Update(new SchemaProperties
            {
                Label = "Contact",
                Hints = "Contact entity in CRM-Core."
            });

            contactSchema.AddField(new StringField(
                Guid.NewGuid(),
                "firstName",
                new StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input first name.",
                    Placeholder = "Input first name...",
                    DefaultValue = "Phuong"
                }));

            contactSchema.AddField(new StringField(
                Guid.NewGuid(), 
                "lastName",
                new StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input last name.",
                    Placeholder = "Input last name...",
                    DefaultValue = "Le"
                }));

            var contact = new Morphism("contact", contactSchema);

            // Customer
            var customerSchema = new SchemaModel.Schema("customer");

            customerSchema.Publish();

            customerSchema.Update(new SchemaProperties
            {
                Label = "Customer",
                Hints = "Customer entity in CRM-Core."
            });

            customerSchema.AddField(new StringField(
                Guid.NewGuid(),
                "firstName",
                new StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input first name.",
                    Placeholder = "Input first name...",
                    DefaultValue = "Lena"
                }));

            customerSchema.AddField(new StringField(
                Guid.NewGuid(),
                "lastName",
                new StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input last name.",
                    Placeholder = "Input last name...",
                    DefaultValue = "Cao"
                }));

            var customer = new Morphism("customer", customerSchema);

            // init
            SchemaItems.Add(Guid.NewGuid(), contact);
            SchemaItems.Add(Guid.NewGuid(), customer);
        }

        public static readonly Dictionary<Guid, Morphism> SchemaItems = new Dictionary<Guid, Morphism>();
    }
}
