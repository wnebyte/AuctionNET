using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AuctionCore.Utils.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class IFormFileExtensionsAttribute : ValidationAttribute
    {
        private string _extensions;

        public IFormFileExtensionsAttribute()
        { }

        public IFormFileExtensionsAttribute(string extensions) =>
            _extensions = extensions;

        public string Extensions
        {
            get
            {
                return string.IsNullOrEmpty(_extensions) ? ".png, .jpg, .jpeg, .gif" : _extensions;
            }
            set
            {
                _extensions = value;
            }
        }

        private string ExtensionsNormalized
        {
            get
            {
                return Extensions.Replace(" ", "", StringComparison.Ordinal).ToUpperInvariant();
            }
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            foreach (var file in value as ICollection<IFormFile>)
            {
                if (!ExtensionsNormalized.Contains(Path.GetExtension(file.FileName).ToUpperInvariant()))
                {
                    return new ValidationResult(FormatErrorMessage(file.FileName));
                }
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(Resources.Culture, ErrorMessageString, name, Extensions);
        }
    }
}
